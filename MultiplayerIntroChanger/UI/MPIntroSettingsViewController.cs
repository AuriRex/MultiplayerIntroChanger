using System;
using System.Collections.Generic;
using System.Linq;

using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.ViewControllers;
using MultiplayerIntroChanger.Configuration;

namespace MultiplayerIntroChanger.UI
{
    internal class MPIntroSettingsViewController : BSMLResourceViewController
    {

        [UIParams]
        BSMLParserParams parserParams;

        // For this method of setting the ResourceName, this class must be the first class in the file.
        public override string ResourceName => "MultiplayerIntroChanger.UI.Views.MPIntroSettingsView.bsml";

        [UIValue("ready-text")]
        private string readyText = "Ready?";
        [UIValue("set-text")]
        private string setText = "Set";
        [UIValue("go-text")]
        private string goText = "Go!";

        [UIAction("#post-parse")]
        public void SetupText() {

            readyText = Plugin.ReadyText;
            setText = Plugin.SetText;
            goText = Plugin.GoText;

            parserParams.EmitEvent("ready-get-event");
            parserParams.EmitEvent("set-get-event");
            parserParams.EmitEvent("go-get-event");

        }

        [UIAction("ready-change")]
        public void ReadyChange(string change) {
            MPIConfig.ReadyText = change;
            Plugin.Instance.LoadTexts();
        }

        [UIAction("set-change")]
        public void SetChange(string change) {
            MPIConfig.SetText = change;
            Plugin.Instance.LoadTexts();
        }

        [UIAction("go-change")]
        public void GoChange(string change) {
            MPIConfig.GoText = change;
            Plugin.Instance.LoadTexts();
        }

        [UIAction("preview-btn-action")]
        private void PreviewButtonAction() {
            /*readyText = "TEST";
            MultiplayerIntroCountdown mic = new MultiplayerIntroCountdown();

            mic.StartCountdown(3,1,1);*/
        }

    }
}
