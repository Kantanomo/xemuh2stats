using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.CompilerServices;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;
using OBSWebsocketDotNet.Types.Events;
using xemuh2stats.classes;
using xemuh2stats.enums;
using xemuh2stats.objects;
using System.IO;
using DocumentFormat.OpenXml.Vml;
using Path = System.IO.Path;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using WhatTheFuck.classes;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.Numerics;


namespace xemuh2stats
{
    public partial class Form1 : Form
    {
        public static bool is_valid = false;
        public static bool real_time_lock = false;
        public static bool dump_lock = false;

        public static bool time_lock = false;
        public static DateTime StartTime;
        public static s_variant_details variant_details_cache;


        public static List<real_time_player_stats> real_time_cache = new List<real_time_player_stats>();
        public static QmpProxy qmp;
        
        public Form1()
        {
            InitializeComponent();

            //obs_communicator.connect("ws://localhost:4455", "HTMwlVgpQ7jyQpJl");

            for (int i = 0; i < 16; i++)
            {
                players_table.Rows.Add();
                debug_table.Rows.Add();
            }

            for (int i = 0; i < weapon_stat.weapon_list.Count; i++)
            {
                weapon_stat_table.Rows.Add();
                weapon_stat_table.Rows[i].Cells[0].Value = weapon_stat.weapon_list[i];
            }

            foreach (configuration configuration in Program.configurations.AsList)
            {
                configuration_combo_box.Items.Add(configuration.name);
            }

            main_tab_control.TabPages.RemoveAt(1);
            main_tab_control.TabPages.RemoveAt(1);

            weapon_player_select.Tag = 0;
        }

        private void main_timer_Tick(object sender, EventArgs e)
        {
            if (is_valid)
            {
                var game_ending = Program.memory.ReadBool(Program.game_state_resolver["game_ending"].address);

                if (game_ending)
                {
                    if (!real_time_lock)
                    {
                        variant_details_cache = variant_details.get();
                        variant_details_cache.start_time = StartTime;
                        variant_details_cache.end_time = DateTime.Now;
                        
                        int player_count =
                            Program.memory.ReadInt(Program.game_state_resolver["game_state_players"].address + 0x3C);

                        real_time_cache.Clear();
                        for (int i = 0; i < player_count; i++)
                        {
                            real_time_cache.Add(objects.real_time_player_stats.get(i));
                        }


                        real_time_lock = true;
                    }
                }
                else
                {
                    real_time_lock = false;
                }

                var cycle = (life_cycle)Program.memory.ReadInt(Program.exec_resolver["life_cycle"].address);
                
                life_cycle_status_label.Text = $@"Life Cycle: {cycle.ToString()} |";

                if (cycle == life_cycle.in_lobby)
                {
                    Program.memory.WriteBool(Program.exec_resolver["profile_enabled"].address,!profile_disabled_check_box.Checked, false);
                }

                if (cycle == life_cycle.in_game)
                {
                    var variant = variant_details.get();
                    variant_status_label.Text = $@"Variant: {variant.name} |";
                    game_type_status_label.Text = $@"Game Type: {variant.game_type.ToString()} |";
                    map_status_label.Text = $@"Map: {variant.map_name} |";
                    if (!time_lock)
                    {
                        StartTime = DateTime.Now;
                        time_lock = true;
                    }
                }
                else
                {
                    time_lock = false;
                }
                
                if (cycle == life_cycle.post_game)
                {
                    if (!dump_lock)
                    {
                        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        List<s_post_game_player> post_game_ = new List<s_post_game_player>();

                        for (int i = 0; i < real_time_cache.Count; i++)
                        {
                            post_game_.Add(post_game_report.get(i));
                        }

                        xls_generator.dump_game_to_sheet($"{timestamp}", real_time_cache, post_game_, variant_details_cache);
                        dump_lock = true;
                    }
                }
                else
                {
                    dump_lock = false;
                }

                switch (main_tab_control.SelectedTab.Text)
                {
                    case "Players":
                    {
                        render_players_tab();
                        break;
                    }
                    case "Weapon Stats":
                    {
                        render_weapon_stat_tab();
                        break;
                    }
                    case "debug":
                    {
                        render_debug_tab();
                        break;
                    }
                }

            }
        }


        private void render_weapon_stat_tab()
        {
            int test_player_count =
                Program.memory.ReadInt(Program.game_state_resolver["game_state_players"].address + 0x3C);

            if ((int) weapon_player_select.Tag != test_player_count)
            {
                weapon_player_select.Items.Clear();
                weapon_player_select.Tag = test_player_count;
                var cur = weapon_player_select.SelectedText;
                var ncur = -1;
                for (int i = 0; i < test_player_count; i++)
                {
                    var name = game_state_player.name(i);
                    if (name == String.Empty && i == 0)
                    {
                        weapon_player_select.Tag = 0;
                        break;
                    }

                    weapon_player_select.Items.Add(name);
                        if (cur == name)
                            ncur = i;
                }

                weapon_player_select.SelectedIndex = ncur;

            }
            if (weapon_player_select.SelectedIndex != -1)
            {
                for (int i = 0; i < weapon_stat.weapon_list.Count; i++)
                {
                    weapon_stat.s_weapon_stat stat = weapon_stat.get_weapon_stats(weapon_player_select.SelectedIndex, i);
                    weapon_stat_table.Rows[i].Cells[1].Value = stat.kills;
                    weapon_stat_table.Rows[i].Cells[2].Value = stat.head_shots;
                    weapon_stat_table.Rows[i].Cells[3].Value = stat.deaths;
                    weapon_stat_table.Rows[i].Cells[4].Value = stat.suicide;
                    weapon_stat_table.Rows[i].Cells[5].Value = stat.shots_fired;
                    weapon_stat_table.Rows[i].Cells[6].Value = stat.shots_hit;
                }
                //weapon_stat.s_weapon_stat astat = weapon_stat.get_weapon_stats(weapon_player_select.SelectedIndex, 5);

                //if (astat.shots_fired != 0 && astat.shots_hit != 0)
                //{
                //    double accuracy = (double)astat.shots_hit / astat.shots_fired;
                //    accuracy = Math.Round(accuracy * 100, 2);
                //    obs_communicator.update_text($"Accuracy: {accuracy}%");
                //}
                //else
                //{
                //    obs_communicator.update_text($"Accuracy: 0%");
                //}
            }
        }

        private void render_players_tab()
        {
            int test_player_count =
                Program.memory.ReadInt(Program.game_state_resolver["game_state_players"].address + 0x3C);
            
            var variant = variant_details.get();
            for (int i = 0; i < test_player_count; i++)
            {
                var player = real_time_player_stats.get(i);
                players_table.Rows[i].Cells[0].Value = player.name;

                switch (variant.game_type)
                {
                    case game_type.capture_the_flag:
                        players_table.Rows[i].Cells[1].Value = player.game_stats.ctf_scores;
                        break;
                    case game_type.slayer:
                        players_table.Rows[i].Cells[1].Value = player.game_stats.kills;
                        break;
                    case game_type.oddball:
                        if (player.game_stats.oddball_score > 0)
                        {
                            try
                            {
                                players_table.Rows[i].Cells[1].Value =
                                    TimeSpan.FromSeconds(player.game_stats.oddball_score).ToString("mm:ss");
                            }
                            catch (Exception)
                            {
                                players_table.Rows[i].Cells[1].Value = player.game_stats.oddball_score;
                            }
                        }

                        break;
                    case game_type.king_of_the_hill:
                    {
                        try
                        {
                            players_table.Rows[i].Cells[1].Value =
                                TimeSpan.FromSeconds(player.game_stats.oddball_score).ToString("mm:ss");
                        }
                        catch (Exception)
                        {
                            players_table.Rows[i].Cells[1].Value = player.game_stats.oddball_score;
                        }
                    }
                        break;
                    case game_type.juggeraut:
                        players_table.Rows[i].Cells[1].Value = player.game_stats.kills_as_juggernaut;
                        break;
                    case game_type.territories:
                    {
                        try
                        {
                            players_table.Rows[i].Cells[1].Value =
                                TimeSpan.FromSeconds(player.game_stats.oddball_score).ToString("mm:ss");
                        }
                        catch (Exception)
                        {
                            players_table.Rows[i].Cells[1].Value = player.game_stats.oddball_score;
                        }
                    }
                        break;
                    case game_type.assault:
                        players_table.Rows[i].Cells[1].Value = player.game_stats.assault_score;
                        break;
                    default:
                        players_table.Rows[i].Cells[1].Value = player.game_stats.kills;
                        break;
                }

                players_table.Rows[i].Cells[2].Value = player.game_stats.kills;
                players_table.Rows[i].Cells[3].Value = player.game_stats.deaths;
                players_table.Rows[i].Cells[4].Value = player.game_stats.assists;
                if (player.game_stats.deaths > 0)
                {
                    float kda = (float)(player.game_stats.kills + player.game_stats.assists) / player.game_stats.deaths;
                    players_table.Rows[i].Cells[5].Value = Math.Round(kda, 3);
                }
                else
                {
                    players_table.Rows[i].Cells[5].Value = player.game_stats.kills + player.game_stats.assists;
                }
            }
        }

        private void render_debug_tab()
        {
            int test_player_count =
                Program.memory.ReadInt(Program.game_state_resolver["game_state_players"].address + 0x3C);
            for (int i = 0; i < test_player_count; i++)
            {
                var player = real_time_player_stats.get(i);
                debug_table.Rows[i].Cells[0].Value = player.name;

                debug_table.Rows[i].Cells[1].Value = player.player_index;
                debug_table.Rows[i].Cells[2].Value = player.game_addr.ToString("X");
                debug_table.Rows[i].Cells[3].Value = player.medal_addr.ToString("X");
                debug_table.Rows[i].Cells[4].Value = player.weapon_addr.ToString("X");
            }
        }

        private void xemu_browse_button_Click(object sender, EventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    xemu_path_text_box.Text = dialog.SelectedPath;
                }
            }
        }
        private string FindFileInDirectory(string folderPath, string fileNameToFind)
        {
            try
            {
                // Search in the current directory
                foreach (var file in Directory.GetFiles(folderPath))
                {
                    if (Path.GetFileName(file).Equals(fileNameToFind, StringComparison.OrdinalIgnoreCase))
                    {
                        return file;  // Return the full path of the file if found
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }

            return null;  // Return null if the file is not found
        }

        private void resolve_addresses()
        {
            // offsets are host (host virtual address - host_base_executable_address)
            Program.exec_resolver.Add(new offset_resolver_item("game_stats", 0x35ADF02, ""));
            Program.exec_resolver.Add(new offset_resolver_item("weapon_stats", 0x35ADFE0, ""));
            Program.exec_resolver.Add(new offset_resolver_item("medal_stats", 0x35ADF4E, ""));
            Program.exec_resolver.Add(new offset_resolver_item("life_cycle", 0x35E4F04, ""));
            Program.exec_resolver.Add(new offset_resolver_item("post_game_report", 0x363A990, ""));
            Program.exec_resolver.Add(new offset_resolver_item("session_players", 0x35AD344, ""));
            Program.exec_resolver.Add(new offset_resolver_item("variant_info", 0x35AD0EC, ""));
            Program.exec_resolver.Add(new offset_resolver_item("profile_enabled", 0x3569128, ""));
            Program.exec_resolver.Add(new offset_resolver_item("players", 0x35A44F4, ""));
            Program.exec_resolver.Add(new offset_resolver_item("game_results_globals", 0x35ACFB0, ""));
            Program.exec_resolver.Add(new offset_resolver_item("game_results_globals_extra", 0x35CF014, ""));
            Program.exec_resolver.Add(new offset_resolver_item("disable_rendering", 0x3520E22, ""));

            Program.game_state_resolver.Add(new offset_resolver_item("game_state_players", 0, "players"));
            Program.game_state_resolver.Add(new offset_resolver_item("game_ending", 0, ""));

            // xemu base_address + xbe base_address
            var host_base_executable_address = (long)qmp.Translate(0x80000000) + 0x5C000;
            
            foreach (offset_resolver_item offsetResolverItem in Program.exec_resolver)
            {
                offsetResolverItem.address = host_base_executable_address + offsetResolverItem.offset;
            }

            ulong test = Program.memory.ReadUInt(Program.exec_resolver["players"].address);

            var taddr = qmp.Translate(test);

            Program.game_state_resolver["game_state_players"].address = (long)taddr;
            Program.game_state_resolver["game_ending"].address = (long) taddr - 0x1E8;

            main_tab_control.TabPages.Add(players_tab_page);
            main_tab_control.TabPages.Add(weapon_stats_tab);
        }

        private void xemu_launch_button_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(xemu_path_text_box.Text))
            {
                var xemu_path = FindFileInDirectory(xemu_path_text_box.Text, "xemu.exe");
                if (!string.IsNullOrEmpty(xemu_path))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = xemu_path,
                        Arguments = "-qmp tcp:localhost:4444,server,nowait",
                    };
                    Process p = Process.Start(startInfo);
                    System.Threading.Thread.Sleep(5000);
                    qmp = new QmpProxy();

                    Program.memory = new MemoryHandler(p);
                    resolve_addresses();
                    is_valid = true;
                }
            }
        }

        private void dump_stats_to_binary_button_Click(object sender, EventArgs e)
        {
            var mem = Program.memory.ReadMemory(false, Program.exec_resolver["game_results_globals"].address, 0xDD8C);
            var mem2 = Program.memory.ReadMemory(false, Program.exec_resolver["game_results_globals_extra"].address, 0x1980);

            IEnumerable<byte> rv = mem.Concat(mem2);
            File.WriteAllBytes("dump.bin", rv.ToArray());
            
        }

        private void disable_rendering_check_box_CheckedChanged(object sender, EventArgs e)
        {
            Program.memory.WriteBool(Program.exec_resolver["disable_rendering"].address, !disable_rendering_check_box.Checked, false);
        }

        private void configuration_save_button_Click(object sender, EventArgs e)
        {
            if (configuration_combo_box.SelectedIndex == -1)
            {
                configuration config = Program.configurations.add(instance_name_text_box.Text);
                config.set("instance_name", instance_name_text_box.Text);
                config.set("xemu_path", xemu_path_text_box.Text);
                config.set("dedi_mode", profile_disabled_check_box.Checked.ToString());
                config.save();
            }
            else
            {
                configuration config = Program.configurations[(string)configuration_combo_box.SelectedItem];
                config.set("instance_name", instance_name_text_box.Text);
                config.set("xemu_path", xemu_path_text_box.Text);
                config.set("dedi_mode", profile_disabled_check_box.Checked.ToString());
                config.save();
            }
        }

        private void configuration_combo_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (configuration_combo_box.SelectedIndex != -1)
            {
                configuration config = Program.configurations[(string)configuration_combo_box.SelectedItem];

                instance_name_text_box.Text = config.get("instance_name", "");
                xemu_path_text_box.Text = config.get("xemu_path", "");
                profile_disabled_check_box.Checked = config.get("dedi_mode", "False") == "True";
            }
        }
    }

    public class offset_resolver_item
    {
        public string name;
        public long offset;
        public string value;
        public long address;

        public offset_resolver_item(string name, long offset, string value)
        {
            this.name = name;
            this.offset = offset;
            this.value = value;
        }
    }
}
