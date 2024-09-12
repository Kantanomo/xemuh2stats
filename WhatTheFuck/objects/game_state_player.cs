using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace xemuh2stats.objects
{
    public static class game_state_player
    {
        public static string name(int index)
        {
            var addr = (Program.game_state_resolver["game_state_players"].address + 0x90) + index * 540;
            return Program.memory.ReadStringUnicode(addr, 16);
        }
    }
}
