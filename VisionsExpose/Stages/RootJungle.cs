using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace StageAesthetic.Stages
{
    class RootJungle
    {
        public static void GreenJungle(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(52, 57, 39, 18);
            fog.fogColorMid.value = new Color32(44, 49, 34, 147);
            fog.fogColorEnd.value = new Color32(69, 75, 53, 255);
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(242, 239, 202, 255);
            sunLight.intensity = 3f;
            sunTransform.localEulerAngles = new Vector3(30, 175, 180);
        }
        public static void SunJungle(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(46, 85, 98, 0);
            fog.fogColorMid.value = new Color32(51, 70, 84, 143);
            fog.fogColorEnd.value = new Color32(92, 127, 131, 255);
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(242, 239, 202, 255);
            sunLight.intensity = 4f;
            sunTransform.localEulerAngles = new Vector3(60, 15, -4);
        }
    }
}
