using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xemuh2stats.objects
{
    public class real_time_player_stats
    {
        public string name;
        public s_game_stats game_stats;
        public s_medal_stats medal_stats;
        public Dictionary<string, weapon_stat.s_weapon_stat> weapon_stats;
        public long game_addr;
        public long medal_addr;
        public long weapon_addr;
        public int player_index;

        public static real_time_player_stats get(int player_index)
        {
            real_time_player_stats result = new real_time_player_stats();

            result.player_index = player_index;

            result.name =
                Program.memory.ReadStringUnicode(Program.exec_resolver["session_players"].address + player_index * 0xA4,
                    16);
            //result.name = game_state_player.name(player_index);
            Dictionary<string, weapon_stat.s_weapon_stat> t_stats = new Dictionary<string, weapon_stat.s_weapon_stat>();
            
            for (int i = 0; i < weapon_stat.weapon_list.Count; i++)
                t_stats.Add(weapon_stat.weapon_list[i], weapon_stat.get_weapon_stats(player_index, i));
            
            result.weapon_stats = t_stats;
            result.weapon_addr = weapon_stat.get_addr(player_index);

            result.game_stats = objects.game_stats.get(player_index);
            result.game_addr = objects.game_stats.get_addr(player_index);

            result.medal_stats = objects.medal_stats.get(player_index);
            result.medal_addr = objects.medal_stats.get_addr(player_index);

            return result;
        }
    }
}
