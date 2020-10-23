using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MultiplayerIntroChanger.HarmonyPatches
{


    [HarmonyPatch(typeof(MultiplayerIntroCountdown))]
    [HarmonyPatch(nameof(MultiplayerIntroCountdown.Awake), MethodType.Normal)]
    internal class MultiplayerIntroCountdownAudioPatch
    {

        private static void Postfix(MultiplayerIntroCountdown __instance, ref AudioClip ____readyClip, ref AudioClip ____setClip, ref AudioClip ____goClip, ref AudioClip ____buildUpClip) {

            if (Plugin.Instance.OriginalReadyClip == null) {

                Plugin.Instance.OriginalReadyClip = ____readyClip;
                Plugin.Instance.OriginalSetClip = ____setClip;
                Plugin.Instance.OriginalGoClip = ____goClip;
                Plugin.Instance.OriginalBuildUpClip = ____buildUpClip;

            }

            MultiplayerIntroAudioContainer miac = MultiplayerIntroAudioLoader.Instance.CurrentAC;

            if (miac == MultiplayerIntroAudioLoader.DefaultAC || miac == null) {

                ____readyClip = Plugin.Instance.OriginalReadyClip;
                ____setClip = Plugin.Instance.OriginalSetClip;
                ____goClip = Plugin.Instance.OriginalGoClip;
                ____buildUpClip = Plugin.Instance.OriginalBuildUpClip;

            } else {

                if (miac.ReadyClip != null) {
                    ____readyClip = miac.ReadyClip;
                } else {
                    ____readyClip = Plugin.Instance.OriginalReadyClip;
                }

                if (miac.SetClip != null) {
                    ____setClip = miac.SetClip;
                } else {
                    ____setClip = Plugin.Instance.OriginalSetClip;
                }

                if (miac.GoClip != null) {
                    ____goClip = miac.GoClip;
                } else {
                    ____goClip = Plugin.Instance.OriginalGoClip;
                }

                if (miac.BuildUpClip != null) {
                    ____buildUpClip = miac.BuildUpClip;
                } else {
                    ____buildUpClip = Plugin.Instance.OriginalBuildUpClip;
                }

            }

        }

    }

    [HarmonyPatch(typeof(MultiplayerIntroCountdown))]
    [HarmonyPatch(nameof(MultiplayerIntroCountdown.PhaseRoutine), MethodType.Normal)]
    internal class MultiplayerIntroCountdownTextPatch
    {
        private static int count = -1;
        private static void Prefix(ref string text) {

            count++;

            switch(count) {
                case 0:
                    if(!Plugin.ReadyText.Equals(""))
                        text = Plugin.ReadyText;
                    break;
                case 1:
                    if (!Plugin.SetText.Equals(""))
                        text = Plugin.SetText;
                    break;
                case 2:
                default:
                    if (!Plugin.GoText.Equals(""))
                        text = Plugin.GoText;
                    count = -1;
                    break;
            }

        }
    }

}
