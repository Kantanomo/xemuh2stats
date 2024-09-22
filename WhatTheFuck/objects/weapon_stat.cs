using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WhatTheFuck.enums;

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
                addr = Program.exec_resolver["game_results_globals_extra"].address + 0xDE +
                       ((player_index - 5) * 0x36A) + (weapon_index * 0x10);

            return new s_weapon_stat()
            {
                kills = Program.memory.ReadUShort(addr),
                deaths = Program.memory.ReadUShort(addr + 2),
                suicide = Program.memory.ReadUShort(addr + 6),
                shots_fired = Program.memory.ReadUShort(addr + 8),
                shots_hit = Program.memory.ReadUShort(addr + 10),
                head_shots = Program.memory.ReadUShort(addr + 12)
            };
            //var data = Program.memory.ReadMemory(false, Program.game_state_resolver["weapon_stats"].address + (player_index * 0x36a) + (weapon_index * 0x10), 0x10);

            // return CastBytesTo<s_weapon_stat>(data, 0, 0x10);
        }

        public static long get_addr(int player_index)
        {
            return Program.exec_resolver["weapon_stats"].address + (player_index * 0x36a);
        }

        public static int damage_reporting_type_to_results_index(DamageReportingType type)
        {
            switch (type)
            {
                case DamageReportingType.MagnumPistol:
                    return 0;
                case DamageReportingType.PlasmaPistol:
                    return 1;
                case DamageReportingType.Needler:
                    return 2;
                case DamageReportingType.SMG:
                    return 3;
                case DamageReportingType.PlasmaRifle:
                    return 4;
                case DamageReportingType.BattleRifle:
                    return 5;
                case DamageReportingType.Carbine:
                    return 6;
                case DamageReportingType.Shotgun:
                    return 7;
                case DamageReportingType.SniperRifle:
                    return 8;
                case DamageReportingType.BeamRifle:
                    return 9;
                case DamageReportingType.RocketLauncher:
                    return 11;
                case DamageReportingType.FlakCannon:
                    return 12;
                case DamageReportingType.BruteShot:
                    return 13;
                case DamageReportingType.Disintegrator:
                    return 14;
                case DamageReportingType.BrutePlasmaRifle:
                    return 10;
                case DamageReportingType.EnergySword:
                    return 17;
                case DamageReportingType.FragGrenade:
                    return 18;
                case DamageReportingType.PlasmaGrenade:
                    return 19;
                case DamageReportingType.FlagMeleeDamage:
                    return 20;
                case DamageReportingType.BombMeleeDamage:
                    return 21;
                case DamageReportingType.BombExplosionDamage:
                    return 35;
                case DamageReportingType.BallMeleeDamage:
                    return 22;
                case DamageReportingType.HumanTurret:
                    return 23;
                case DamageReportingType.PlasmaTurret:
                    return 24;
                case DamageReportingType.Banshee:
                    return 25;
                case DamageReportingType.Ghost:
                    return 26;
                case DamageReportingType.Mongoose:
                    return 27;
                case DamageReportingType.Scorpion:
                    return 28;
                case DamageReportingType.SpectreDriver:
                    return 29;
                case DamageReportingType.SpectreGunner:
                    return 30;
                case DamageReportingType.WarthogDriver:
                    return 31;
                case DamageReportingType.WarthogGunner:
                    return 32;
                case DamageReportingType.Wraith:
                    return 33;
                case DamageReportingType.Tank:
                    return 34;
                case DamageReportingType.SentinelBeam:
                    return 15;
                case DamageReportingType.SentinelRPG:
                    return 16;
                default:
                    return 0;
            }
        }


    }
}
