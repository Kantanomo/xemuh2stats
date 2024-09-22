using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WhatTheFuck.classes.websocket;
using WhatTheFuck.extensions;
using WhatTheFuck.objects;
using xemuh2stats;
using xemuh2stats.enums;
using xemuh2stats.objects;

namespace WhatTheFuck.classes
{
    internal class websocket_message
    {
        public string message_type { get; set; }
        public Dictionary<string, string> arguments { get; set; }
    }

    internal class websocket_response<T>
    {
        public string message_type;
        public string response_type;
        public T response;

        public websocket_response(string message_type, string response_type, T response)
        {
            this.message_type = message_type;
            this.response = response;
        }
    }

    internal class websocket_response_error
    {
        public string message_type;
        public string error_message;

        public websocket_response_error(string message_type, string error_message)
        {
            this.message_type = message_type;
            this.error_message = error_message;
        }
    }

    internal static class websocket_message_handlers
    {

        public static string websocket_message_get_player_weapon(Dictionary<string, string> arguments)
        {
            if (!arguments.ContainsKey("player"))
                return JsonConvert.SerializeObject(new websocket_response_error("get_player", "A player name was not provided for the request"));

            int player_count = Program.memory.ReadInt(Program.game_state_resolver["game_state_players"].address + 0x3C);
            
            s_game_state_player player = new s_game_state_player();
            int player_index = -1;

            for (int i = 0; i < player_count; i++)
            {
                real_time_player_stats _player = real_time_player_stats.get(i);
                if (_player.GetPlayerName() == arguments["player"])
                {
                    player_index = i;
                    break;
                }
            }
            for (int i = 0; i < player_count; i++)
            {
                s_game_state_player _player = game_state_player.get(i);
                if (game_state_player.name(i) == arguments["player"])
                {
                    player = _player;
                    break;
                }
            }

            if (player.Equals(new s_game_state_player()))
                return JsonConvert.SerializeObject(new websocket_response_error("get_player", $"player: {arguments["player"]} could not be found"));

            Dictionary<string, weapon_stat.s_weapon_stat> result = new Dictionary<string, weapon_stat.s_weapon_stat>();

            var weapon = game_state_object.unit_object_get_weapon_type(player.unit_index);
            var stat = weapon_stat.get_weapon_stats(player_index, weapon_stat.damage_reporting_type_to_results_index(weapon));

            result.Add(weapon.GetDisplayName(), stat);

            return JsonConvert.SerializeObject(new websocket_response<Dictionary<string, weapon_stat.s_weapon_stat>>("get_player_weapon", "", result));
        }

        public static string websocket_message_get_life_cycle(Dictionary<string, string> arguments)
        {
            var cycle = (life_cycle)Program.memory.ReadInt(Program.exec_resolver["life_cycle"].address);
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("life_cycle", cycle.ToString());
            return JsonConvert.SerializeObject(new websocket_response<Dictionary<string, string>>("get_life_cycle", "", result));
        }
        
        public static string websocket_message_get_player(Dictionary<string, string> arguments)
        {
            if (!arguments.ContainsKey("type"))
                return JsonConvert.SerializeObject(new websocket_response_error("get_player", "A type was not provided for the request"));

            if (!arguments.ContainsKey("player"))
                return JsonConvert.SerializeObject(new websocket_response_error("get_player", "A player name was not provided for the request"));

            int player_count = Program.memory.ReadInt(Program.game_state_resolver["game_state_players"].address + 0x3C);
            real_time_player_stats player = null;

            for (int i = 0; i < player_count; i++)
            {
                real_time_player_stats _player = real_time_player_stats.get(i);
                if (_player.GetPlayerName() == arguments["player"])
                {
                    player = _player;
                    break;
                }
            }

            if (player == null)
                return JsonConvert.SerializeObject(new websocket_response_error("get_player", $"player: {arguments["player"]} could not be found"));

            switch (arguments["type"])
            {
                case "full":
                {
                    Dictionary<string, real_time_player_stats> result = new Dictionary<string, real_time_player_stats>();
                    result.Add(player.GetPlayerName(), player);
                    return JsonConvert.SerializeObject(new websocket_response<Dictionary<string, real_time_player_stats>>("get_player", arguments["type"], result));
                }
                case "game_stats":
                {
                    Dictionary<string, s_game_stats> result = new Dictionary<string, s_game_stats>();
                    result.Add(player.GetPlayerName(), player.game_stats);
                    return JsonConvert.SerializeObject(new websocket_response<Dictionary<string, s_game_stats>>("get_player", arguments["type"], result));
                }
                case "medals":
                {
                    Dictionary<string, s_medal_stats> result = new Dictionary<string, s_medal_stats>();
                    result.Add(player.GetPlayerName(), player.medal_stats);
                    return JsonConvert.SerializeObject(new websocket_response<Dictionary<string, s_medal_stats>>("get_player", arguments["type"], result));
                }
                case "weapons":
                {
                    Dictionary<string, Dictionary<string, weapon_stat.s_weapon_stat>> result = new Dictionary<string, Dictionary<string, weapon_stat.s_weapon_stat>>();
                    result.Add(player.GetPlayerName(), player.weapon_stats);
                    return JsonConvert.SerializeObject(new websocket_response<Dictionary<string, Dictionary<string, weapon_stat.s_weapon_stat>>>("get_player", arguments["type"], result));
                }
                case "location":
                {
                    for (int i = 0; i < player_count; i++)
                    {
                        s_game_state_player _player = game_state_player.get(i);
                        if (game_state_player.name(i) == arguments["player"])
                        {
                            Dictionary<string, Vector3> result = new Dictionary<string, Vector3>();
                            result.Add(arguments["player"], game_state_object.get_object_position(_player.unit_index));
                            return JsonConvert.SerializeObject(
                                new websocket_response<Dictionary<string, Vector3>>("get_player", arguments["type"],
                                    result));
                        }
                    }
                    return JsonConvert.SerializeObject(new websocket_response_error("get_player", "Player position couldn't be found"));
                }
                default:
                    return JsonConvert.SerializeObject(new websocket_response_error("get_player", "A invalid type was provided for the request"));
            }
        }

        public static string websocket_message_get_players(Dictionary<string, string> arguments)
        {
            if (!arguments.ContainsKey("type"))
                return JsonConvert.SerializeObject(new websocket_response_error("get_players", "A type was not provided for the request"));

            int player_count = Program.memory.ReadInt(Program.game_state_resolver["game_state_players"].address + 0x3C);

            switch (arguments["type"])
            {
                case "full":
                {
                    Dictionary<string, real_time_player_stats> players =
                        new Dictionary<string, real_time_player_stats>();

                    for (int i = 0; i < player_count; i++)
                    {
                        real_time_player_stats player_ = real_time_player_stats.get(i);
                        players.Add(player_.GetPlayerName(), player_);
                    }

                    return JsonConvert.SerializeObject(new websocket_response<Dictionary<string, real_time_player_stats>>("get_players", arguments["type"], players));
                    }

                case "game_stats":
                {
                    Dictionary<string, s_game_stats> players = new Dictionary<string, s_game_stats>();
                    for (int i = 0; i < player_count; i++)
                    {
                        real_time_player_stats player_ = real_time_player_stats.get(i);
                        players.Add(player_.GetPlayerName(), player_.game_stats);
                    }

                    return JsonConvert.SerializeObject(new websocket_response<Dictionary<string, s_game_stats>>("get_players", arguments["type"], players));
                }

                case "properties":
                {
                    Dictionary<string, s_player_properties> players = new Dictionary<string, s_player_properties>();

                    for (int i = 0; i < player_count; i++)
                    {
                        real_time_player_stats player_ = real_time_player_stats.get(i);
                        players.Add(player_.GetPlayerName(), player_.player);
                    }

                    return JsonConvert.SerializeObject(new websocket_response<Dictionary<string, s_player_properties>>("get_players", arguments["type"], players));
                }

                case "medals":
                {
                    Dictionary<string, s_medal_stats> players = new Dictionary<string, s_medal_stats>();

                    for (int i = 0; i < player_count; i++)
                    {
                        real_time_player_stats player_ = real_time_player_stats.get(i);
                        players.Add(player_.GetPlayerName(), player_.medal_stats);
                    }

                    return JsonConvert.SerializeObject(new websocket_response<Dictionary<string, s_medal_stats>>("get_players", arguments["type"], players));
                }
                case "weapons":
                {
                    Dictionary<string, Dictionary<string, weapon_stat.s_weapon_stat>> players = new Dictionary<string, Dictionary<string, weapon_stat.s_weapon_stat>>();

                    for (int i = 0; i < player_count; i++)
                    {
                        real_time_player_stats player_ = real_time_player_stats.get(i);
                        players.Add(player_.GetPlayerName(), player_.weapon_stats);
                    }

                    return JsonConvert.SerializeObject(new websocket_response<Dictionary<string, Dictionary<string, weapon_stat.s_weapon_stat>>>("get_players", arguments["type"], players));
                }
                case "location":
                {
                    Dictionary<string, Vector3> result = new Dictionary<string, Vector3>();
                    for (int i = 0; i < player_count; i++)
                    {
                        s_game_state_player _player = game_state_player.get(i);
                        result.Add(game_state_player.name(i), game_state_object.get_object_position(_player.unit_index));
                    }
                    return JsonConvert.SerializeObject(new websocket_response<Dictionary<string, Vector3>>("get_players", arguments["type"], result));
                }
                default:
                    return JsonConvert.SerializeObject(new websocket_response_error("get_players", "A invalid type was provided for the request"));
            }
        }
    }


    public static class websocket_communicator
    {
        private static websocket_server _server;

        private static void server_message_received(object sender, OnMessageReceivedHandler args)
        {
            string message_json = args.GetMessage();
            websocket_client client = args.GetClient();
            if (!string.IsNullOrEmpty(message_json))
            {
                websocket_message message;
                try
                {
                    message = JsonConvert.DeserializeObject<websocket_message>(message_json);
                }
                catch
                {
                    _server.SendMessage(client, JsonConvert.SerializeObject(new websocket_response_error("error", "the sent json message was malformed")));
                    return;
                }

                switch (message.message_type)
                {
                    case "get_players":
                        _server.SendMessage(client, websocket_message_handlers.websocket_message_get_players(message.arguments));
                        break;
                    case "get_player":
                        _server.SendMessage(client, websocket_message_handlers.websocket_message_get_player(message.arguments));
                        break;
                    case "get_player_weapon":
                        _server.SendMessage(client, websocket_message_handlers.websocket_message_get_player_weapon(message.arguments));
                        break;
                    case "get_life_cycle":
                        _server.SendMessage(client, websocket_message_handlers.websocket_message_get_life_cycle(message.arguments));
                        break;
                }
            }
        }

        public static void start(string ip, string port)
        {
            _server = new websocket_server(new IPEndPoint(IPAddress.Parse(ip), int.Parse(port)));
            _server.OnMessageReceived += server_message_received;
            _server.OnClientConnected += _server_OnClientConnected;
            _server.OnClientDisconnected += _server_OnClientDisconnected;
            _server.OnSendMessage += _server_OnSendMessage;
        }

        private static void _server_OnSendMessage(object sender, OnSendMessageHandler e)
        {
        }

        private static void _server_OnClientDisconnected(object sender, OnClientDisconnectedHandler e)
        {
        }

        private static void _server_OnClientConnected(object sender, OnClientConnectedHandler e)
        {
        }

        public static void stop()
        {
            _server.Stop();
        }
    }
}
