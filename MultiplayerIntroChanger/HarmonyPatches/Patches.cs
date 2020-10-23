using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerIntroChanger.HarmonyPatches
{
    class Patches
    {

        private static Harmony instance;

        public static bool IsPatched { get; private set; }
        public const string InstanceId = "com.AuriRex.BeatSaber.MultiplayerIntroChanger";

        internal static void ApplyHarmonyPatches() {
            if (!IsPatched) {
                if (instance == null) {
                    instance = new Harmony(InstanceId);
                }
                instance.PatchAll(Assembly.GetExecutingAssembly());
                IsPatched = true;
            }
        }

        internal static void RemoveHarmonyPatches() {
            if (instance != null && IsPatched) {
                instance.UnpatchAll(InstanceId);
                IsPatched = false;
            }
        }

    }
}
