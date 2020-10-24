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

        [Init]
        public void Init(IPALogger logger, IPA.Config.Config config, PluginMetadata metadata) {
            Instance = this;
            MPIConfig.Init(config);
            Logger.log = logger;
        }

        [OnStart]
        public void OnApplicationStart() {
            MPIConfig.Load();
            Patches.ApplyHarmonyPatches();
            LoadTexts();
            MPIAudioLoader.Init();
            SharedCoroutineStarter.instance.StartCoroutine(LoadAudio());
            MPIntroUI.CreateMenu();
        }

        [OnExit]
        public void OnApplicationQuit() {
            MPIConfig.Save();
            Patches.RemoveHarmonyPatches();
            MPIntroUI.RemoveMenu();
        }

        internal void LoadTexts() {
            ReadyText = MPIConfig.ReadyText;
            SetText = MPIConfig.SetText;
            GoText = MPIConfig.GoText;
        }

        private IEnumerator LoadAudio() {
            Logger.log.Info("Attempting to load Audio files ...");
            yield return MPIAudioLoader.Instance.Load();
        }

        public IEnumerator ReloadAudioCoroutine(Action callback) {
            Logger.log.Info("Attempting to reload Audio files ...");
            yield return MPIAudioLoader.Instance.Reload();
            callback();
        }

        public void ReloadAudio(Action callback) {
            SharedCoroutineStarter.instance.StartCoroutine(ReloadAudioCoroutine(callback));
        }

        

    }
}
