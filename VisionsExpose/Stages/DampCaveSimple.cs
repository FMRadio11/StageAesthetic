using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Stages
{
    class DampCaveSimple
    {
        public static void VanillaChanges()
        {
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.intensity = 2f;
            GameObject.Find("Directional Light (SUN)").transform.localEulerAngles = new Vector3(35, 15, 351);
        }
        public static void DarkCave(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(33, 10, 4, 176);
            fog.fogColorMid.value = new Color32(74, 8, 8, 222);
            fog.fogColorEnd.value = new Color32(62, 4, 5, 255);
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(215, 73, 69, 255);
            sunLight.intensity = 1.6f;
            GameObject.Find("Directional Light (SUN)").transform.localEulerAngles = new Vector3(43, 78, 351);
            RampFog caveFog = GameObject.Find("HOLDER: Lighting, PP, Wind, Misc").transform.Find("DCPPInTunnels").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveFog.fogColorStart.value = new Color32(50, 21, 13, 176);
            caveFog.fogColorMid.value = new Color32(80, 19, 19, 222);
            caveFog.fogColorEnd.value = new Color32(75, 13, 15, 255);
        }
        public static void MeadowCave(RampFog fog, GameObject rain)
        {
            fog.fogColorStart.value = new Color32(96, 67, 103, 33);
            fog.fogColorMid.value = new Color32(102, 66, 109, 148);
            fog.fogColorEnd.value = new Color32(148, 85, 166, 255);
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(205, 129, 255, 255);
            sunLight.intensity = 1f;
            if (AestheticConfig.WeatherEffects.Value) UnityEngine.Object.Instantiate<GameObject>(rain, Vector3.zero, Quaternion.identity);
            RampFog caveFog = GameObject.Find("HOLDER: Lighting, PP, Wind, Misc").transform.Find("DCPPInTunnels").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveFog.fogColorStart.value = new Color32(85, 57, 91, 33);
            caveFog.fogColorMid.value = new Color32(90, 55, 97, 148);
            caveFog.fogColorEnd.value = new Color32(135, 76, 149, 255);
        }
    }
}
