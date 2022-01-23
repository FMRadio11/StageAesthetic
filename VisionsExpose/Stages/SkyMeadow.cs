using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Stages
{
    class SkyMeadow
    {
        public static void VanillaChanges(GameObject purple)
        {
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(239, 231, 211, 255);
            sunLight.intensity = 2f;
            sunLight.shadowStrength = 1f;
            if (Aesthetic.WeatherEffects.Value) UnityEngine.Object.Instantiate<GameObject>(purple, Vector3.zero, Quaternion.identity);
        }
        public static void NightMeadow(RampFog fog, GameObject purple)
        {
            fog.fogColorStart.value = new Color32(38, 59, 69, 33);
            fog.fogColorMid.value = new Color32(12, 15, 59, 131);
            fog.fogColorEnd.value = new Color32(18, 3, 45, 255);
            fog.fogZero.value = -0.08f;
            fog.skyboxStrength.value = 0.25f;
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(165, 156, 198, 255);
            sunLight.intensity = 2f;
            sunLight.shadowStrength = 0.4f;
            lightBase.Find("CameraRelative").Find("SmallStars").gameObject.SetActive(true);
            GameObject.Find("SMSkyboxPrefab").transform.Find("MoonHolder").Find("ShatteredMoonMesh").gameObject.SetActive(false);
            GameObject.Find("SMSkyboxPrefab").transform.Find("MoonHolder").Find("MoonMesh").gameObject.SetActive(true);
            if (Aesthetic.WeatherEffects.Value) UnityEngine.Object.Instantiate<GameObject>(purple, Vector3.zero, Quaternion.identity);
        }
        public static void StormyMeadow(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(76, 86, 98, 0);
            fog.fogColorMid.value = new Color32(67, 62, 88, 159);
            fog.fogColorEnd.value = new Color32(75, 73, 96, 255);
            fog.fogZero.value = -0.02f;
            fog.skyboxStrength.value = 0.1f;
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(142, 156, 202, 255);
            sunLight.intensity = 0.6f;
            sunLight.shadowStrength = 0.3f;
            lightBase.Find("CameraRelative").Find("Rain").gameObject.SetActive(true);
            GameObject.Find("SMSkyboxPrefab").transform.Find("SmallStars").gameObject.SetActive(false);
        }
        public static void EpicMeadow(RampFog fog, ColorGrading cgrade, GameObject ember)
        {
            fog.fogColorStart.value = new Color(0.176f, 0.019f, 0.013f, 0.3569f);
            fog.fogColorMid.value = new Color(0.151f, 0.061f, 0, 0.775f);
            fog.fogColorEnd.value = new Color(0.059f, 0f, 0f, 1);
            fog.fogZero.value = -0.04f;
            fog.fogOne.value = 0.27f;
            fog.skyboxStrength.value = 0.05f;
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color(0.817f, 0.181f, 0, 0.367f);
            sunLight.intensity = 2;
            sunLight.shadowStrength = 0.55f;
            lightBase.Find("CameraRelative").Find("SmallStars").gameObject.SetActive(true);
            GameObject.Find("SMSkyboxPrefab").transform.Find("MoonHolder").Find("ShatteredMoonMesh").gameObject.SetActive(false);
            GameObject.Find("SMSkyboxPrefab").transform.Find("MoonHolder").Find("MoonMesh").gameObject.SetActive(true);
            if (Aesthetic.WeatherEffects.Value) UnityEngine.Object.Instantiate<GameObject>(ember, Vector3.zero, Quaternion.identity);
            cgrade.colorFilter.value = new Color(1, 0.632f, 0.471f);
        }
    }
}
