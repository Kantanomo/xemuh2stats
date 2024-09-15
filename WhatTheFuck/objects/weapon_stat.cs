using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace xemuh2stats.objects
{
    public static class weapon_stat
    {
        public static List<string> weapon_list = new List<string>()
        {
            "magnum",
            "plasma pistol",
            "needler",
            "SMG",
            "plasma rifle",
            "battle rifle",
            "carbine",
            "shotgun",
            "sniper rifle",
            "beam rifle",
            "brute plasma rifle",
            "rocket launcher",
            "fuel rod",
            "brute shot",
            "unused 1",
            "sentinal beam",
            "unused 2",
            "energy sword",
            "frag grenade",
            "plasma grenade",
            "flag",
            "bomb",
            "oddball",
        };

        [StructLayout(layoutKind: LayoutKind.Sequential, Pack = 1)]
        public struct s_weapon_stat
        {
            public ushort kills;
            public ushort deaths;
            public ushort suicide;
            public ushort shots_fired;
            public ushort shots_hit;
            public ushort head_shots;
        }


        public static s_weapon_stat get_weapon_stats(int player_index, int weapon_index)
        {
            var addr = Program.exec_resolver["weapon_stats"].address + (player_index * 0x36a) + (weapon_index * 0x10);

            if (player_index > 4)
                addr = Program.exec_resolver["game_results_globals_extra"].address + 0xDE + ((player_index - 5) * 0x36A) + (weapon_index * 0x10);

            return new s_weapon_stat()
            {
                kills = Program.memory.ReadUShort(addr),
                deaths = Program.memory.ReadUShort(addr + 2),
                suicide = Program.memory.ReadUShort(addr + 6),
                shots_fired = Program.memory.ReadUShort(addr + 8),
                shots_hit = Program.memory.ReadUShort(addr + 10),
                head_shots = Program.memory.ReadUShort(addr + 12)
            } ;
            //var data = Program.memory.ReadMemory(false, Program.game_state_resolver["weapon_stats"].address + (player_index * 0x36a) + (weapon_index * 0x10), 0x10);

            // return CastBytesTo<s_weapon_stat>(data, 0, 0x10);
        }

        public static long get_addr(int player_index)
        {
            return Program.exec_resolver["weapon_stats"].address + (player_index * 0x36a);
        }
    }
}
