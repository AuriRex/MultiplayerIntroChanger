using BeatSaberMarkupLanguage;
using HMUI;
using System;
namespace MultiplayerIntroChanger.UI
{
    internal class MPIntroFlowCoordinator : FlowCoordinator
    {
        private MPIntroListViewController introListView;
        private MPIntroSettingsViewController settingsView;

        public void Awake() {
            if (!settingsView) {
                settingsView = BeatSaberUI.CreateViewController<MPIntroSettingsViewController>();
            }

            if (!introListView) {
                introListView = BeatSaberUI.CreateViewController<MPIntroListViewController>();
            }
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling) {
            try {
                if (firstActivation) {
                    SetTitle("Custom Multiplayer Intro Audio");
                    showBackButton = true;
                    ProvideInitialViewControllers(introListView, settingsView, null);
                }
            } catch (Exception ex) {
                Logger.log.Error(ex);
            }
        }

        protected override void BackButtonWasPressed(ViewController topViewController) =>
            // Dismiss ourselves
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this, null, ViewController.AnimationDirection.Vertical, false);
    }
}

