
using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace MultiplayerIntroChanger.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }
        // Must be 'virtual' if you want BSIPA to detect a value change and save the config automatically.
        public virtual string ReadyText { get; set; } = "READY!"; 
        public virtual string SetText { get; set; } = "SET!"; 
        public virtual string GoText { get; set; } = "GO!";

        public virtual string LastSound { get; set; } = "";

        /// <summary>
        /// This is called whenever BSIPA reads the config from disk (including when file changes are detected).
        /// </summary>
        public virtual void OnReload() {
            // Do stuff after config is read from disk.
        }

        /// <summary>
        /// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
        /// </summary>
        public virtual void Changed() {
            // Do stuff when the config is changed.
        }

        /// <summary>
        /// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
        /// </summary>
        public virtual void CopyFrom(PluginConfig other) {
            // This instance's members populated from other
        }
    }
}
