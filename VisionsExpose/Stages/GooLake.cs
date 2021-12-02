using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;

namespace StageAesthetic.Stages
{
    class GooLake
    {
        public static void DarkAqueduct(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(43, 23, 12, 144);
            fog.fogColorMid.value = new Color32(56, 30, 19, 195);
            fog.fogColorEnd.value = new Color32(66, 41, 29, 255);
            fog.skyboxStrength.value = 0.06f;
            fog.fogOne.value = 0.152f;
            Transform base1 = GameObject.Find("HOLDER: Misc Props").transform;
            GameObject.Find("HOLDER: Warning Flags").SetActive(false);
            base1.Find("Warning Signs").gameObject.SetActive(true);
            var lightBase = GameObject.Find("Weather, Goolake").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(190, 162, 154, 255);
            sunLight.intensity = 0.8f;
            sunLight.shadowStrength = 0.6f;
        }
        public static void BlueAqueduct(RampFog fog, GameObject rain)
        {
            fog.fogColorStart.value = new Color32(57, 63, 76, 73);
            fog.fogColorMid.value = new Color32(62, 71, 83, 179);
            fog.fogColorEnd.value = new Color32(68, 77, 90, 255);
            fog.skyboxStrength.value = 0.055f;
            Transform base1 = GameObject.Find("HOLDER: Misc Props").transform;
            base1.Find("Props").GetChild(4).gameObject.SetActive(true);
            var lightBase = GameObject.Find("Weather, Goolake").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(166, 221, 253, 255);
            sunLight.intensity = 1.2f;
            sunLight.shadowStrength = 0.1f;
            sunTransform.localEulerAngles = new Vector3(42, 12, 180);
            if (AestheticConfig.WeatherEffects.Value) UnityEngine.Object.Instantiate<GameObject>(rain, Vector3.zero, Quaternion.identity);
        }
        public static void VanillaChanges()
        {
            var lightBase = GameObject.Find("Weather, Goolake").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 246, 229, 255);
            sunLight.intensity = 1.4f;
            sunTransform.localEulerAngles = new Vector3(42, 12, 180);
        }
        public static void IgnoreThis(string warning)
        {
            int stagenum = Math.Min(Mathf.FloorToInt((Run.instance.stageClearCount - 3) / 2), 50);
            if (UnityEngine.Random.Range(stagenum, 70) <= stagenum)
            {
                StageAesthetic.Aesthetic.AesLog.LogFatal(warning);
                StageAesthetic.Aesthetic.epic = true;
            }
        }
    }
}
