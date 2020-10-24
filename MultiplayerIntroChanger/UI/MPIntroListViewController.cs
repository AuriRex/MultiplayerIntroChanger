using System;
using System.Collections.Generic;
using System.Linq;

using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

namespace MultiplayerIntroChanger.UI
{
    internal class MPIntroListViewController : BSMLResourceViewController
    {
        // For this method of setting the ResourceName, this class must be the first class in the file.
        public override string ResourceName => "MultiplayerIntroChanger.UI.Views.MPIntroListView.bsml";


        [UIComponent("soundList")]
        public CustomListTableData customListTableData;

        [UIAction("soundSelected")]
        public void Select(TableView _, int row) {
            MPIAudioLoader.Instance.SelectAudio(row);
        }

        [UIComponent("reload-button")]
        Button reloadButton;

        [UIAction("reloadSounds")]
        public void ReloadSounds() {
            reloadButton.interactable = false;
            Plugin.Instance.ReloadAudio(OnReloaded);
        }

        public void OnReloaded() {
            SetupList();
            reloadButton.interactable = true;
        }

        Sprite defaultSprite;
        Sprite defaultCustomSprite;

        [UIAction("#post-parse")]
        public void SetupList() {
            customListTableData.data.Clear();

            foreach (MPIAudioContainer audio in MPIAudioLoader.Instance.IntroAudioList) {

                Sprite icon;

                if(audio.Icon != null) {
                    icon = Sprite.Create(audio.Icon, new Rect(0.0f, 0.0f, audio.Icon.width, audio.Icon.height), new Vector2(0.5f, 0.5f), 100.0f);
                } else {
                    if(!defaultSprite) {
                        defaultSprite = Sprite.Create(Util.GetDefaultIcon(), new Rect(0.0f, 0.0f, Util.GetDefaultIcon().width, Util.GetDefaultIcon().height), new Vector2(0.5f, 0.5f), 100.0f);
                    }
                    if(!defaultCustomSprite) {
                        defaultCustomSprite = Sprite.Create(Util.GetCustomIcon(), new Rect(0.0f, 0.0f, Util.GetCustomIcon().width, Util.GetCustomIcon().height), new Vector2(0.5f, 0.5f), 100.0f);
                    }
                    if(MPIAudioLoader.DefaultAC == audio) {
                        icon = defaultSprite;
                    } else {
                        icon = defaultCustomSprite;
                    }
                    
                }

                CustomListTableData.CustomCellInfo customCellInfo = new CustomListTableData.CustomCellInfo(audio.Name, audio.ReplacesText, icon);
                customListTableData.data.Add(customCellInfo);
            }

            customListTableData.tableView.ReloadData();
            int selectedAudio = MPIAudioLoader.Instance.GetIndexOfSelected();

            customListTableData.tableView.ScrollToCellWithIdx(selectedAudio, TableViewScroller.ScrollPositionType.Beginning, false);
            customListTableData.tableView.SelectCellWithIdx(selectedAudio);
        }


    }
}
