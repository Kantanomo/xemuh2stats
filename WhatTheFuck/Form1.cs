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


namespace xemuh2stats
{
    public partial class Form1 : Form
    {
        public static bool is_valid = false;
        public static bool real_time_lock = false;
        public static bool dump_lock = false;
        public static List<real_time_player_stats> real_time_cache = new List<real_time_player_stats>();
        
        public Form1()
        {
            //127300
            //Program.game_state_resolver.Add(new offset_resolver_item("game_state_players", 0x2CE89D4, "players"));
            ////Program.game_state_resolver.Add(new offset_resolver_item("weapon_stats", 0x34A1CC4 + 0x128BD0, ""));
            //Program.game_state_resolver.Add(new offset_resolver_item("weapon_stats", 0x35C8FC4, ""));
            ////Program.game_state_resolver.Add(new offset_resolver_item("game_stats", 0x34A1BE6, ""));
            //Program.game_state_resolver.Add(new offset_resolver_item("game_stats", 0x35C8EE6, ""));
            ////Program.game_state_resolver.Add(new offset_resolver_item("game_stats", 0x34A1BE6, ""));
            //Program.game_state_resolver.Add(new offset_resolver_item("medal_stats", 0x35C8F32, ""));
            ////Program.game_state_resolver.Add(new offset_resolver_item("medal_stats", 0x34A1C32, ""));
            //Program.game_state_resolver.Add(new offset_resolver_item("life_cycle", 0x361FF34, ""));
            ////Program.game_state_resolver.Add(new offset_resolver_item("life_cycle", 0x34F7364, ""));
            //Program.game_state_resolver.Add(new offset_resolver_item("game_ending", 0x2CE87EC, ""));
            //Program.game_state_resolver.Add(new offset_resolver_item("post_game_report", 0x3655974, ""));
            ////Program.game_state_resolver.Add(new offset_resolver_item("post_game_report", 0x350CAD4, ""));
            InitializeComponent();

            iso_select.SelectedIndex = 0;

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

            main_tab_control.TabPages.RemoveAt(1);
            main_tab_control.TabPages.RemoveAt(1);

            weapon_player_select.Tag = 0;
        }

        private void SetResolver()
        {
            Program.game_state_resolver.Clear();
            switch (iso_select.SelectedIndex)
            { 
                case 0: // 1.5
                    
                    Program.game_state_resolver.Add(new offset_resolver_item("game_state_players", 0x2CE89D4, "players"));
                    Program.game_state_resolver.Add(new offset_resolver_item("game_ending", 0x2CE87EC, ""));


                    Program.exec_resolver.Add(new offset_resolver_item("game_stats", 0x35ADF02, ""));
                    Program.exec_resolver.Add(new offset_resolver_item("weapon_stats", 0x35ADFE0, ""));
                    Program.exec_resolver.Add(new offset_resolver_item("medal_stats", 0x35ADF4E, ""));
                    Program.exec_resolver.Add(new offset_resolver_item("life_cycle", 0x35E4F04, ""));
                    Program.exec_resolver.Add(new offset_resolver_item("post_game_report", 0x363A990, ""));
                    Program.exec_resolver.Add(new offset_resolver_item("session_players", 0x35AD344, ""));
                    Program.exec_resolver.Add(new offset_resolver_item("variant_info", 0x35AD0EC, ""));
                    Program.exec_resolver.Add(new offset_resolver_item("profile_enabled", 0x3569128, ""));
                    break;
                case 1: // r1

                    break;
                case 2: // 1.0
                    Program.game_state_resolver.Add(new offset_resolver_item("game_state_players", 0x2CE89D4, "players"));
                    Program.game_state_resolver.Add(new offset_resolver_item("weapon_stats", 0x34A1CC4, ""));
                    Program.game_state_resolver.Add(new offset_resolver_item("game_stats", 0x34A1BE6, ""));
                    Program.game_state_resolver.Add(new offset_resolver_item("medal_stats", 0x34A1C32, ""));
                    Program.game_state_resolver.Add(new offset_resolver_item("life_cycle", 0x34F7364, ""));
                    Program.game_state_resolver.Add(new offset_resolver_item("game_ending", 0x2CE87EC, ""));
                    Program.game_state_resolver.Add(new offset_resolver_item("post_game_report", 0x350CAD4, ""));
                    break;
                default:

                    break;
            }
        }

        static bool IsHexString(string input)
        {
            // Regular expression to match hexadecimal format
            Regex hexPattern = new Regex("^[0-9a-fA-F]+$");

            // Check if the input matches the pattern
            return hexPattern.IsMatch(input);
        }
        private async void go_button_Click(object sender, EventArgs e)
        {
            Program.memory = new MemoryHandler(Process.GetProcessesByName("xemu")[0]);
            if (exec_text_box.Text == "")
            {
                var exec_list = Program.memory.ScanProcessAsync(
                    new byte[] {0x53, 0x33, 0xDB, 0xE8, 0x80, 0xDD, 0x2C, 0x00},
                    new byte[] {1, 1, 1, 1, 1, 1, 1, 1});

                int working_count = 0;
                while (!exec_list.IsCompleted)
                {
                    valid_label.Text = "Working" + new string('.', working_count);
                    working_count++;
                    if (working_count == 5)
                        working_count = 0;
                    await Task.Delay(100);
                }
                exec_text_box.Text = exec_list.Result[0].ToString("X");
            }
            
            if (go_text_box.Text == "")
            {
                var tags_list = Program.memory.ScanProcessAsync(
                new byte[] {0x73, 0x67, 0x61, 0x74, 0x2B, 0x21, 0x23, 0x24},
                new byte[] {1, 1, 1, 1, 1, 1, 1, 1});

                var working_count = 0;
                while (!tags_list.IsCompleted)
                {
                    valid_label.Text = "Working" + new string('.', working_count);
                    working_count++;
                    if (working_count == 5)
                        working_count = 0;
                    await Task.Delay(100);
                }
                go_text_box.Text = tags_list.Result[0].ToString("X");
            }

            var b = 123123;

            SetResolver();

            long address = Convert.ToInt64(go_text_box.Text, 16);
            //Program.memory = new MemoryHandler(Process.GetProcessesByName("xemu")[0]);
            ////5333DBE8023D2C00
            ////byte[] targetBytes = new byte[] { 0x53, 0x33, 0xDB, 0xE8, 0x02, 0x3D, 0x2C, 0x00 };
            ////byte[] t_bytes = memory.ReadMemory(false, address, targetBytes.Length);
            var t_string = Program.memory.ReadStringAscii(address, 8, false);

            if (t_string == "sgat+!#$")
            {
                int valid_count = 0;
                foreach (offset_resolver_item offsetResolverItem in Program.game_state_resolver)
                {
                    offsetResolverItem.address = address + offsetResolverItem.offset;
                    t_string = Program.memory.ReadStringAscii(offsetResolverItem.address, offsetResolverItem.value.Length);
                    if (t_string == offsetResolverItem.value)
                    {
                        valid_count++;
                    }
                }

                if (valid_count == Program.game_state_resolver.Count)
                {
                    valid_label.Text = "Valid!";
                    is_valid = true;
                }
                else
                {
                    valid_label.Text = "Invalid!";
                    is_valid = false;
                }
            }

            address = Convert.ToInt64(exec_text_box.Text, 16);
            byte[] targetBytes = new byte[] { 0x53, 0x33, 0xDB, 0xE8, 0x80, 0xDD, 0x2C, 0x00 };
            byte[] t_bytes = Program.memory.ReadMemory(false, address, targetBytes.Length);

            if (targetBytes.SequenceEqual(t_bytes))
            {
                int valid_count = 0;
                foreach (offset_resolver_item offsetResolverItem in Program.exec_resolver)
                {
                    offsetResolverItem.address = address + offsetResolverItem.offset;
                    t_string = Program.memory.ReadStringAscii(offsetResolverItem.address, offsetResolverItem.value.Length);
                    if (t_string == offsetResolverItem.value)
                        valid_count++;
                }
                if (valid_count == Program.exec_resolver.Count)
                {
                    valid_label.Text = "Valid!";
                    is_valid = true;
                }
                else
                {
                    valid_label.Text = "Invalid!";
                    is_valid = false;
                }
            }

            if (is_valid)
            {
                main_tab_control.TabPages.Add(players_tab_page);
                main_tab_control.TabPages.Add(weapon_stats_tab);
            }
        }

        private void main_timer_Tick(object sender, EventArgs e)
        {
            if (is_valid)
            {
                var game_ending = Program.memory.ReadBool(Program.game_state_resolver["game_ending"].address);
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

                if (game_ending)
                {
                    if (!real_time_lock)
                    {
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

                        xls_generator.dump_game_to_sheet($"{timestamp}", real_time_cache, post_game_);
                        dump_lock = true;
                    }
                }
                else
                {
                    dump_lock = false;
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

                debug_table.Rows[i].Cells[1].Value = player.game_stats.oddball_score;
                debug_table.Rows[i].Cells[2].Value = player.game_stats.oddball_ball_kills;
                debug_table.Rows[i].Cells[3].Value = player.game_stats.assault_bomb_grabbed;
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
