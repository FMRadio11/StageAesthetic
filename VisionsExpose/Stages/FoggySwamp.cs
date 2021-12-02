using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace StageAesthetic.Stages
{
    class FoggySwamp
    {
        public static void PinkSwamp(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(108, 83, 125, 13);
            fog.fogColorMid.value = new Color32(150, 124, 174, 195);
            fog.fogColorEnd.value = new Color32(178, 126, 237, 255);
            fog.skyboxStrength.value = 0;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(198, 152, 223, 255);
            sunLight.intensity = 0.9f;
        }
        public static void GoldSwamp(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(129, 94, 43, 30);
            fog.fogColorMid.value = new Color32(131, 96, 37, 150);
            fog.fogColorEnd.value = new Color32(129, 90, 34, 255);
            fog.skyboxStrength.value = 0;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(209, 119, 47, 255);
        }
    }
}
