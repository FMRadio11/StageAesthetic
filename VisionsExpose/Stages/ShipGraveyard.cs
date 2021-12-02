using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace StageAesthetic.Stages
{
    class ShipGraveyard
    {
        public static void ShipNight(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(10, 47, 39, 59);
            fog.fogColorMid.value = new Color32(8, 37, 30, 176);
            fog.fogColorEnd.value = new Color32(4, 25, 22, 255);
            fog.skyboxStrength.value = 0.8f;
            var lightBase = GameObject.Find("Weather, Shipgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(155, 163, 227, 255);
            sunLight.intensity = 0.8f;
            sunLight.shadowStrength = 0.4f;
        }
        public static void ShipSkies(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(53, 66, 82, 18);
            fog.fogColorMid.value = new Color32(64, 67, 103, 154);
            fog.fogColorEnd.value = new Color32(126, 156, 166, 255);
            var lightBase = GameObject.Find("Weather, Shipgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 239, 223, 255);
            sunLight.intensity = 2f;
            sunLight.shadowStrength = 0.7f;
            sunTransform.localEulerAngles = new Vector3(33, 0, 0);
        }
    }
}
