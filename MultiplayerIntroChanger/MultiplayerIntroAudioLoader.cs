using MultiplayerIntroChanger.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace MultiplayerIntroChanger
{
    public class MultiplayerIntroAudioLoader
    {

        public static MultiplayerIntroAudioLoader Instance;

        public static void Init() {
            if(Instance == null) {
                Instance = new MultiplayerIntroAudioLoader();
            }
        }

        public List<MultiplayerIntroAudioContainer> IntroAudioList { get; private set; } = new List<MultiplayerIntroAudioContainer>();

        public MultiplayerIntroAudioContainer CurrentAC { get; private set; }

        public static MultiplayerIntroAudioContainer DefaultAC { get; private set; } = new MultiplayerIntroAudioContainer("Default");

        internal IEnumerator Load() {

            Clear();

            var folderPath = Environment.CurrentDirectory + Plugin.AudioPath;
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            var directories = Directory.GetDirectories(folderPath);
            IntroAudioList.Add(DefaultAC);
            foreach (var folder in directories) {
                Logger.log.Info(folder);
                MultiplayerIntroAudioContainer miac = new MultiplayerIntroAudioContainer("");//FromFolder(folder);
                yield return miac.LoadFromFolder(folder + "/");
                if(miac != null) {
                    IntroAudioList.Add(miac);
                    Logger.log.Info("Added: " + miac.Name + " | " + (miac.BuildUpClip != null) + " | " + (miac.ReadyClip != null) + " | " + (miac.SetClip != null) + " | " + (miac.GoClip != null));
                    if(miac.Name.Equals(PluginConfig.Instance.LastSound)) {
                        Logger.log.Info("Last used Sounds found!");
                        CurrentAC = miac;
                    }
                }
            }
            
            if(CurrentAC == null) {
                CurrentAC = DefaultAC;
            }

        }

        internal void Clear() {
            IntroAudioList.Clear();
        }

        /*public MultiplayerIntroAudioContainer FromFolder(string path) {

            string name = new FileInfo(path).Directory.Name;

            if (name.Equals("Default")) return null;

            AudioClip ready = GetFromPath(path, "Ready.ogg");
            AudioClip set = GetFromPath(path, "Set.ogg");
            AudioClip go = GetFromPath(path, "Go.ogg");
            AudioClip build = GetFromPath(path, "BuildUp.ogg");

            return new MultiplayerIntroAudioContainer(name, ready, set, go, build);
        }

        public IEnumerator GetFromPath(string path, string sound) {

            if (File.Exists(path + "/" + sound)) {
                string url1 = "file:///" + path + "/" + sound;
                
                UnityWebRequest www1 = UnityWebRequestMultimedia.GetAudioClip(url1, AudioType.OGGVORBIS);
                clip = null;
                yield return www1.SendWebRequest();

                if (www1.isNetworkError)
                    Logger.log.Notice("Failed to load "+sound+" audio: " + www1.error);
                else
                    clip = DownloadHandlerAudioClip.GetContent(www1);
            }
            clip = null;
        }*/

    }
}
