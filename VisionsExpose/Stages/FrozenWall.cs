using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;

namespace StageAesthetic.Stages
{
    class FrozenWall
    {
        public static void OceanWall(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(47, 52, 62, 80);
            fog.fogColorMid.value = new Color32(72, 80, 98, 212);
            fog.fogColorEnd.value = new Color32(90, 101, 119, 255);
            fog.skyboxStrength.value = 0.15f;
            fog.fogZero.value = -0.05f;
            fog.fogOne.value = 0.4f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(177, 184, 200, 255);
            sunLight.intensity = 1.2f;
        }
        public static void NightWall(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(18, 30, 43, 164);
            fog.fogColorMid.value = new Color32(38, 46, 56, 218);
            fog.fogColorEnd.value = new Color32(25, 37, 47, 255);
            fog.skyboxStrength.value = 0.7f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(127, 168, 217, 255);
            sunLight.intensity = 0.9f;
            sunLight.shadowStrength = 0.4f;
            GameObject.Find("Directional Light (SUN)").transform.eulerAngles = new Vector3(50, 275, 2);
            GameObject.Find("HOLDER: Skybox").transform.Find("Water").localPosition = new Vector3 (-1260,-66,0);
        }
    }
}
