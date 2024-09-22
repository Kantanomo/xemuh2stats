using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xemuh2stats;

namespace WhatTheFuck.objects
{
    public static class cache_file_tags
    {
        private static Dictionary<uint, long> tag_cache = new Dictionary<uint, long>();
        private static uint tag_instance_qemu_address = 0; 
        private static long tag_instance_address;
        private static uint tag_instance_count;

        private static void check_tag_instance_addr()
        {
            var addr = Program.memory.ReadUInt(Program.game_state_resolver["game_state_tags"].address + 8);
            if (tag_instance_qemu_address != addr)
            {
                tag_cache.Clear();
                tag_instance_qemu_address = addr;
                tag_instance_address = (long)Program.qmp.Translate(tag_instance_qemu_address);
                tag_instance_count =
                    Program.memory.ReadUInt(Program.game_state_resolver["game_state_tags"].address + 24);
            }
        }

        public static void clear_cache()
        { 
            tag_cache.Clear();
        }


        public static long get_tag_address(uint tag_index)
        {
            check_tag_instance_addr();

            if(tag_cache.ContainsKey(tag_index)) return tag_cache[tag_index];

            short abs_index = (short)(tag_index & 0xFFFF);
            var addr = tag_instance_address + (16 * abs_index) + 8;
            var offset = Program.memory.ReadUInt(addr);
            var r_offset = (long)Program.qmp.Translate(offset);

            tag_cache.Add(tag_index, r_offset);

            return r_offset;
        }
    }
}
