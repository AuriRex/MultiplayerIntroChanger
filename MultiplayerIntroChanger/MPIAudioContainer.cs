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
    public class MPIAudioContainer
    {

        public string Name { get; private set; }
        public AudioClip BuildUpClip { get; private set; }
        public AudioClip ReadyClip { get; private set; }
        public AudioClip SetClip { get; private set; }
        public AudioClip GoClip { get; private set; }
        public Texture2D Icon { get; private set; }
        public string ReplacesText { get; internal set; }

        public MPIAudioContainer(string name, AudioClip ready = null, AudioClip set = null, AudioClip go = null, AudioClip build = null, Texture2D icon = null) {

            Name = name;
            BuildUpClip = build;
            ReadyClip = ready;
            SetClip = set;
            GoClip = go;
            Icon = icon;

        }

        internal IEnumerator LoadFromFolder(string path) {

            string name = new FileInfo(path).Directory.Name;

            Name = name;

            if (!name.Equals("Default")) {
                yield return GetFromPath(path, "Ready.ogg");
                ReadyClip = clip;
                clip = null;
                yield return GetFromPath(path, "Set.ogg");
                SetClip = clip;
                clip = null;
                yield return GetFromPath(path, "Go.ogg");
                GoClip = clip;
                clip = null;
                yield return GetFromPath(path, "Buildup.ogg");
                BuildUpClip = clip;
                clip = null;
                yield return GetIconFromPath(path, "Icon.png");
            }

            SetReplacesText();
            
        }

        private void SetReplacesText() {

            ReplacesText = string.Empty;

            if(ReadyClip) {
                ReplacesText += "Ready";
            }

            if (SetClip) {
                if (ReplacesText != string.Empty) ReplacesText += ", ";
                ReplacesText += "Set";
            }

            if (GoClip) {
                if (ReplacesText != string.Empty) ReplacesText += ", ";
                ReplacesText += "Go";
            }

            if (BuildUpClip) {
                if (ReplacesText != string.Empty) ReplacesText += ", ";
                ReplacesText += "Buildup";
            }

            if(ReplacesText == string.Empty) {
                ReplacesText = "None, delete this Folder!";
            }

        }

        private AudioClip clip;

        public IEnumerator GetFromPath(string path, string sound) {

            if (File.Exists(path + sound)) {
                string url1 = "file:///" + path + sound;

                UnityWebRequest www1 = UnityWebRequestMultimedia.GetAudioClip(url1, AudioType.OGGVORBIS);
                clip = null;
                yield return www1.SendWebRequest();

                if (www1.isNetworkError)
                    Logger.log.Notice("Failed to load " + sound + " audio: " + www1.error);
                else
                    clip = DownloadHandlerAudioClip.GetContent(www1);
            }
        }

        public IEnumerator GetIconFromPath(string path, string iconName) {

            if (File.Exists(path + iconName)) {
                string url1 = "file:///" + path + iconName;

                UnityWebRequest www1 = UnityWebRequestTexture.GetTexture(url1);
                yield return www1.SendWebRequest();

                if (www1.isNetworkError)
                    Logger.log.Notice("Failed to load " + iconName + " texture: " + www1.error);
                else
                    Icon = DownloadHandlerTexture.GetContent(www1);
            }
        }
    }
}
