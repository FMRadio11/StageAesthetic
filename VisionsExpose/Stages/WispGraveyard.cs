using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;

namespace StageAesthetic.Stages
{
    class WispGraveyard
    {
        public static void SunsetAcres(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(84, 51, 36, 9);
            fog.fogColorMid.value = new Color32(97, 47, 35, 132);
            fog.fogColorEnd.value = new Color32(107, 53, 32, 243);
            fog.skyboxStrength.value = 0.126f;
            fog.fogZero.value = -0.019f;
            fog.fogOne.value = 0.211f;
            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 98, 0, 255);
            sunLight.intensity = 1f;
            sunTransform.localEulerAngles = new Vector3(30, 198.5f, 218.841f);
            var sunBase = lightBase.Find("CameraRelative").Find("SunHolder").Find("Sphere");
            Transform quad = sunBase.GetChild(1);
            quad.localScale = new Vector3(14, 14, 14);
            quad.localEulerAngles = new Vector3(270, 30, 0);
            quad.localPosition = new Vector3(0, 0, 0);
            sunBase.GetChild(0).gameObject.SetActive(false);
        }
        public static void MoonAcres(RampFog fog, GameObject rain)
        {
            fog.fogColorStart.value = new Color32(45, 49, 75, 30);
            fog.fogColorMid.value = new Color32(26, 25, 62, 130);
            fog.fogColorEnd.value = new Color32(39, 32, 56, 255);
            fog.skyboxStrength.value = 0.03f;
            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(173, 175, 245, 255);
            sunLight.intensity = 0.9f;
            sunLight.shadowStrength = 0.4f;
            sunLight.shadowBias = 0.05f;
            lightBase.Find("CameraRelative").Find("SunHolder").gameObject.SetActive(false);
            if (AestheticConfig.WeatherEffects.Value) UnityEngine.Object.Instantiate<GameObject>(rain, Vector3.zero, Quaternion.identity);
            var dummylist = UnityEngine.Object.FindObjectsOfType(typeof(WeatherParticles)) as WeatherParticles[];
            for (var i = 0; i < dummylist.Length; i++)
            {
                Debug.Log(dummylist[i].name);
                Debug.Log(dummylist[i].gameObject.name);
                if (dummylist[i].gameObject.name.Equals("Skybox Assets"))
                {
                    Debug.Log("test");
                    Transform eclipseBase = dummylist[i].gameObject.transform.parent;
                    eclipseBase.gameObject.SetActive(true);
                    eclipseBase.Find("PP + Amb").gameObject.SetActive(false);
                    eclipseBase.Find("Directional Light (SUN)").gameObject.SetActive(false);
                }
            }
            dummylist = null;
        }
        public static void VanillaChanges()
        {
            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 135, 0, 255);
            sunLight.intensity = 3f;
            sunTransform.localEulerAngles = new Vector3(36, 0, 0);
            var sunBase = lightBase.Find("CameraRelative").Find("SunHolder").Find("Sphere");
            sunBase.position = new Vector3(-30, 2267, -3200);
            ;
            Transform[] quadCount = new Transform[]{ sunBase.GetChild(0), sunBase.GetChild(1) };
            foreach (Transform quad in quadCount)
            {
                quad.localPosition = new Vector3(0, -1, 1);
                quad.localEulerAngles = new Vector3(270, 30, 0);
            }
        }
    }
}
