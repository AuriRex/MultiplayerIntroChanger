using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MultiplayerIntroChanger
{
    internal class Util
    {

        /// <summary>
        /// Loads an embedded resource from the calling assembly
        /// </summary>
        /// <param name="resourcePath">Path to resource</param>
        public static byte[] LoadFromResource(string resourcePath) => GetResource(Assembly.GetCallingAssembly(), resourcePath);

        /// <summary>
        /// Loads an embedded resource from an assembly
        /// </summary>
        /// <param name="assembly">Assembly to load from</param>
        /// <param name="resourcePath">Path to resource</param>
        public static byte[] GetResource(Assembly assembly, string resourcePath) {
            Stream stream = assembly.GetManifestResourceStream(resourcePath);
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, (int) stream.Length);
            return data;
        }

        private static Texture2D defaultIcon = null;

        public static Texture2D GetDefaultIcon() {
            if (!defaultIcon) {
                try {
                    byte[] resource = LoadFromResource($"MultiplayerIntroChanger.Resources.Icons.IconAudioBlue.png");
                    defaultIcon = LoadTextureRaw(resource);
                } catch { }
            }

            return defaultIcon;
        }

        private static Texture2D customIcon = null;

        public static Texture2D GetCustomIcon() {
            if (!customIcon) {
                try {
                    byte[] resource = LoadFromResource($"MultiplayerIntroChanger.Resources.Icons.IconAudio.png");
                    customIcon = LoadTextureRaw(resource);
                } catch { }
            }

            return customIcon;
        }

        private static Texture2D mutedIcon = null;

        public static Texture2D GetMutedIcon() {
            if (!mutedIcon) {
                try {
                    byte[] resource = LoadFromResource($"MultiplayerIntroChanger.Resources.Icons.IconMuted.png");
                    mutedIcon = LoadTextureRaw(resource);
                } catch { }
            }

            return mutedIcon;
        }

        private static AudioClip muted = null;
        public static AudioClip GetSilentAudioClip() {
            if(!muted) {
                byte[] resource = LoadFromResource($"MultiplayerIntroChanger.Resources.Audio.NoSound.wav");
                float[] samples = new float[resource.Length / 4];
                muted = AudioClip.Create("Silence", samples.Length, 1, 44100, false);
                muted.SetData(samples, 0);
            }

            return muted;
        }

        /// <summary>
        /// Loads an Texture2D from byte[]
        /// </summary>
        /// <param name="file"></param>
        public static Texture2D LoadTextureRaw(byte[] file) {
            if (file.Length > 0) {
                Texture2D texture = new Texture2D(2, 2);
                if (texture.LoadImage(file)) {
                    return texture;
                }
            }

            return null;
        }
    }
}
