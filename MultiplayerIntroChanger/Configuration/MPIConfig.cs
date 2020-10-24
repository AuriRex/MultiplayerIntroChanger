using IPA.Config.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerIntroChanger.Configuration
{
    public class MPIConfig
    {
        public static string ReadyText { get; internal set; } = "";
        public static string SetText { get; internal set; } = "";
        public static string GoText { get; internal set; } = "";
        public static string LastSound { get; internal set; } = "";

        internal static void Init(IPA.Config.Config config) => PluginConfig.Instance = config.Generated<PluginConfig>();

        internal static void Load() {
            PluginConfig ins = PluginConfig.Instance;
            ReadyText = ins.ReadyText;
            SetText = ins.SetText;
            GoText = ins.GoText;
            LastSound = ins.LastSound;
        }

        internal static void Save() {
            PluginConfig ins = PluginConfig.Instance;
            ins.ReadyText = ReadyText;
            ins.SetText = SetText;
            ins.GoText = GoText;
            ins.LastSound = LastSound;
        }


    }
}
