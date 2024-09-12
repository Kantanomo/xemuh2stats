using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace xemuh2stats.objects
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct s_medal_stats
    {
        public ushort double_kill;
        public ushort triple_kill;
        public ushort killtacular;
        public ushort kill_frenzy;
        public ushort killtrocity;
        public ushort killamanjaro;
        public ushort sniper_kill;
        public ushort road_kill;
        public ushort bone_cracker;
        public ushort assassin;
        public ushort vehicle_destroyed;
        public ushort car_jacking;
        public ushort stick_it;
        public ushort killing_spree;
        public ushort running_riot;
        public ushort rampage;
        public ushort beserker;
        public ushort over_kill;
        public ushort flag_taken;
        public ushort flag_carrier_kill;
        public ushort flag_returned;
        public ushort bomb_planted;
        public ushort bomb_carrier_kill;
        public ushort bomb_returned;
    }
    public static class medal_stats
    {
        public static s_medal_stats get(int player_index)
        {
            var addr = Program.exec_resolver["medal_stats"].address + player_index * 0x36A;
            return Program.CastBytesTo<s_medal_stats>(Program.memory.ReadMemory(false, addr, 48), 0, 48);
        }
    }
}
