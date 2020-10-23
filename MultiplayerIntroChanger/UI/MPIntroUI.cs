
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;

namespace MultiplayerIntroChanger.UI
{
    internal class MPIntroUI
    {
        private static readonly MenuButton menuButton = new MenuButton("MP Intro", "Change Custom Multiplayer Intro Stuff Here!", IntroMenuButtonPressed, true);

        public static MPIntroFlowCoordinator introFlowCoordinator;
        public static bool created = false;

        public static void CreateMenu() {
            if (!created) {
                MenuButtons.instance.RegisterButton(menuButton);
                created = true;
            }
        }

        public static void RemoveMenu() {
            if (created) {
                MenuButtons.instance.UnregisterButton(menuButton);
                created = false;
            }
        }

        public static void ShowIntroFlow() {
            if (introFlowCoordinator == null) {
                introFlowCoordinator = BeatSaberUI.CreateFlowCoordinator<MPIntroFlowCoordinator>();
            }

            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(introFlowCoordinator, null, HMUI.ViewController.AnimationDirection.Vertical, false, false);
        }

        private static void IntroMenuButtonPressed() => ShowIntroFlow();
    }
}

