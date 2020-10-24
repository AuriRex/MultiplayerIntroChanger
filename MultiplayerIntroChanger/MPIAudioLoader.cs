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
    public class MPIAudioLoader
    {

        public static MPIAudioLoader Instance;

        public static void Init() {
            if(Instance == null) {
                Instance = new MPIAudioLoader();
            }

            MutedAC = new MPIAudioContainer("Mute", Util.GetSilentAudioClip(), Util.GetSilentAudioClip(), Util.GetSilentAudioClip(), Util.GetSilentAudioClip(), Util.GetMutedIcon()) { ReplacesText = "Silence those pesky intro sounds!" };

        }

        public List<MPIAudioContainer> IntroAudioList { get; private set; } = new List<MPIAudioContainer>();

        public MPIAudioContainer CurrentAC { get; private set; }

        public static MPIAudioContainer DefaultAC { get; private set; } = new MPIAudioContainer("Default") { ReplacesText = "The default multiplayer intro sounds!" };
        public static MPIAudioContainer MutedAC { get; private set; }

        internal MPIAudioContainer SelectAudio(int idx) {

            MPIAudioContainer[] ial = IntroAudioList.ToArray();

            if (idx < ial.Length && idx >= 0) {
                CurrentAC = ial[idx];
            } else {
                CurrentAC = DefaultAC;
            }

            MPIConfig.LastSound = CurrentAC.Name;

            return CurrentAC;
        }

        internal int GetIndexOfSelected() {

            foreach(MPIAudioContainer miac in IntroAudioList) {
                if(miac == CurrentAC) {
                    return IntroAudioList.IndexOf(miac);
                }
            }
            return 0;
        }

        internal IEnumerator Load() {

            var folderPath = Environment.CurrentDirectory + Plugin.AudioPath;
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            var directories = Directory.GetDirectories(folderPath);
            IntroAudioList.Add(DefaultAC);
            IntroAudioList.Add(MutedAC);
            foreach (var folder in directories) {
                //Logger.log.Info(folder);
                MPIAudioContainer miac = new MPIAudioContainer("");//FromFolder(folder);
                yield return miac.LoadFromFolder(folder + "/");
                if(miac != null) {
                    IntroAudioList.Add(miac);
                    //Logger.log.Info("Added: " + miac.Name + " | " + (miac.BuildUpClip != null) + " | " + (miac.ReadyClip != null) + " | " + (miac.SetClip != null) + " | " + (miac.GoClip != null));
                    if(miac.Name.Equals(MPIConfig.LastSound)) {
                        //Logger.log.Info("Last used Sounds found!");
                        CurrentAC = miac;
                    }
                }
            }

            Logger.log.Info((IntroAudioList.Count - 2) + " custom multiplayer intro sound preset(s) loaded!");

            if(CurrentAC == null) {
                CurrentAC = DefaultAC;
            }

        }

        internal IEnumerator Reload() {

            Clear();

            yield return Load();

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
