using System;
using System.Collections.Generic;
using System.Linq;

using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;


namespace MultiplayerIntroChanger.UI
{
    internal class MPIntroListViewController : BSMLResourceViewController
    {
        // For this method of setting the ResourceName, this class must be the first class in the file.
        public override string ResourceName => "MultiplayerIntroChanger.UI.Views.MPIntroListView.bsml";
    }
}
