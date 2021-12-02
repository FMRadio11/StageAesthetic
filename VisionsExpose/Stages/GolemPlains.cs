using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;

namespace StageAesthetic.Stages
{
    class GolemPlains
    {
        public static void SunsetPlains(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(45, 49, 75, 0);
            fog.fogColorMid.value = new Color32(113, 75, 58, 130);
            fog.fogColorEnd.value = new Color32(178, 93, 82, 255);
            fog.skyboxStrength.value = 0.156f;
            fog.fogZero.value = -0.049f;
            fog.fogOne.value = 0.211f;
            fog.fogIntensity.value = 0.769f;
            fog.fogPower.value = 0.5f;
            var lightBase = GameObject.Find("Weather, Golemplains").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 135, 0, 255);
            sunLight.intensity = 1.14f;
            sunLight.shadowStrength = 0.877f;
            sunTransform.localEulerAngles = new Vector3(27, 268, 88);
        }
        public static void RainyPlains(RampFog fog, GameObject rain)
        {
            fog.fogColorStart.value = new Color32(34, 45, 62, 18);
            fog.fogColorMid.value = new Color32(72, 84, 103, 165);
            fog.fogColorEnd.value = new Color32(97, 109, 129, 255);
            fog.skyboxStrength.value = 0.075f;
            fog.fogPower.value = 0.35f;
            fog.fogOne.value = 0.108f;
            var lightBase = GameObject.Find("Weather, Golemplains").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(64, 144, 219, 255);
            sunLight.intensity = 0.9f;
            sunLight.shadowStrength = 0.7f;
            sunTransform.localEulerAngles = new Vector3(50, 17, 270);
            if (AestheticConfig.WeatherEffects.Value) UnityEngine.Object.Instantiate<GameObject>(rain, Vector3.zero, Quaternion.identity);
        }
        public static void VanillaChanges()
        { 
            var lightBase = GameObject.Find("Weather, Golemplains").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(178, 218, 255, 255);
            sunLight.intensity = 1.5f;
            sunTransform.localEulerAngles = new Vector3(33, 267, 277);
        }
    }
}
