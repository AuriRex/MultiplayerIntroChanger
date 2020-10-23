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
    public class MultiplayerIntroAudioContainer
    {

        public string Name { get; private set; }
        public AudioClip BuildUpClip { get; private set; }
        public AudioClip ReadyClip { get; private set; }
        public AudioClip SetClip { get; private set; }
        public AudioClip GoClip { get; private set; }

        public MultiplayerIntroAudioContainer(string name, AudioClip ready = null, AudioClip set = null, AudioClip go = null, AudioClip build = null) {

            Name = name;
            BuildUpClip = build;
            ReadyClip = ready;
            SetClip = set;
            GoClip = go;

        }

        internal IEnumerator LoadFromFolder(string path) {

            string name = new FileInfo(path).Directory.Name;

            Name = name;

            if (!name.Equals("Default")) {
                yield return GetFromPath(path, "Ready.ogg");
                ReadyClip = clip;
                yield return GetFromPath(path, "Set.ogg");
                SetClip = clip;
                yield return GetFromPath(path, "Go.ogg");
                GoClip = clip;
                yield return GetFromPath(path, "Buildup.ogg");
                BuildUpClip = clip;
                clip = null;
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
    }
}
