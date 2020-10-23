using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using UnityEngine.SceneManagement;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;
using MultiplayerIntroChanger.HarmonyPatches;
using System.IO;
using MultiplayerIntroChanger.UI;
using IPA.Loader;
using MultiplayerIntroChanger.Configuration;

namespace MultiplayerIntroChanger
{

    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static string Name => "MultiplayerIntroChanger";

        internal static string ReadyText = "";
        internal static string SetText = "";
        internal static string GoText = "";

        public AudioClip OriginalReadyClip { get; internal set; }
        public AudioClip OriginalSetClip { get; internal set; }
        public AudioClip OriginalGoClip { get; internal set; }
        public AudioClip OriginalBuildUpClip { get; internal set; }

        internal const string AudioPath = "/UserData/MultiplayerIntroSounds";


        //Uncomment to use BSIPA's config

        [Init]
        public void Init(IPALogger logger, Config config, PluginMetadata metadata) {
            Instance = this;
            Configuration.PluginConfig.Instance = config.Generated<Configuration.PluginConfig>();
            Logger.log = logger;
            Logger.log.Debug("Config loaded");
        }

        [OnStart]
        public void OnApplicationStart() {
            Patches.ApplyHarmonyPatches();
            LoadTexts();
            MultiplayerIntroAudioLoader.Init();
            SharedCoroutineStarter.instance.StartCoroutine(LoadAudio());
            MPIntroUI.CreateMenu();
        }

        [OnExit]
        public void OnApplicationQuit() {
            Patches.RemoveHarmonyPatches();
            MPIntroUI.RemoveMenu();
        }

        internal void LoadTexts() {
            ReadyText = PluginConfig.Instance.ReadyText;
            SetText = PluginConfig.Instance.SetText;
            GoText = PluginConfig.Instance.GoText;
        }

        public IEnumerator LoadAudio() {
            Logger.log.Notice("Attempting to load Audio files");
            yield return MultiplayerIntroAudioLoader.Instance.Load();
        }

    }
}
