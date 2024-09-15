using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace xemuh2stats.objects
{
    public static class obs_communicator
    {
        public static OBSWebsocket obs;

        public static void connect(string url, string password)
        {
            obs = new OBSWebsocket();
            obs.Connected += onConnect;
            obs.Disconnected += onDisconnect;
            obs.CurrentProgramSceneChanged += onCurrentProgramSceneChanged;
            obs.ConnectAsync(url, password);
            
        }

        private static void onConnect(object sender, EventArgs e)
        {

        }

        private static void onDisconnect(object sender, OBSWebsocketDotNet.Communication.ObsDisconnectionInfo e)
        {

        }

        private static void onCurrentProgramSceneChanged(object sender, ProgramSceneChangedEventArgs args)
        {

        }

        public static void update_text(string text)
        {
            obs.SetInputSettings("banana", new JObject(){{"text", text}});
        }
    }
}
