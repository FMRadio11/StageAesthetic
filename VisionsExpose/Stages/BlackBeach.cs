using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Stages
{
    class BlackBeach
    {
        public static void VanillaBeach(GameObject rain, String scenename)
        {
            if (AestheticConfig.WeatherEffects.Value && scenename == "blackbeach2") UnityEngine.Object.Instantiate<GameObject>(rain, Vector3.zero, Quaternion.identity);
        }
        public static void LightBeach(RampFog fog, String scenename, ColorGrading cgrade)
        {
            // Much more sun than vanilla roost - almost enough to give it a cel-shaded look on lower texture settings. The whiter lighting allows the green/brown elements to stand out more.
            fog.fogColorStart.value = new Color32(107, 125, 123, 25);
            fog.fogColorMid.value = new Color32(129, 154, 152, 69);
            fog.fogColorEnd.value = new Color32(156, 194, 189, 114);
            fog.skyboxStrength.value = 0.13f;
            fog.fogZero.value = 0;
            fog.fogOne.value = 0.142f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(255, 246, 229, 255);
            sunLight.intensity = 1.8f;
            sunLight.shadowStrength = 1;
            cgrade.colorFilter.value = new Color32(255, 234, 194, 255);
            cgrade.colorFilter.overrideState = true;
            if (scenename == "blackbeach")
            {
                // There's unused fog assets here too, enabling those
                GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("SKYBOX").transform.GetChild(4).gameObject.SetActive(true);
                // Removing rain
                GameObject.Find("HOLDER: Weather Particles").transform.Find("BBSkybox").Find("CameraRelative").Find("Rain").gameObject.SetActive(false);
            }
        }
        public static void DarkBeach(RampFog fog, String scenename, GameObject rain, ColorGrading cgrade)
        {
            // Dark and purple, not much else to say here really
            fog.fogColorStart.value = new Color32(24, 20, 43, 48);
            fog.fogColorMid.value = new Color32(33, 25, 49, 150);
            fog.fogColorEnd.value = new Color32(43, 35, 62, 255);
            fog.skyboxStrength.value = 0.03f;
            fog.fogPower.value = 0.6f;
            cgrade.colorFilter.value = new Color32(179, 162, 249, 255);
            cgrade.colorFilter.overrideState = true;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(106, 69, 160, 255);
            sunLight.intensity = 1f;
            sunLight.shadowStrength = 0.45f;
            if (scenename == "blackbeach")
            {
                // Enabling some unused fog
                GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("SKYBOX").transform.GetChild(4).gameObject.SetActive(true);
            }
            var lightList = UnityEngine.Object.FindObjectsOfType(typeof(Light)) as Light[];
            // Aiding visibility by increasing the lighting effect of the crystal pillars in both stages
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null)
                {
                    var lightParent = lightBase.transform.parent;
                    if (lightParent != null)
                    {
                        if (lightParent.gameObject.name.Equals("BbRuinBowl") || lightParent.gameObject.name.Equals("BbRuinBowl (1)") || lightParent.gameObject.name.Equals("BbRuinBowl (2)"))
                        {

                            light.intensity = 15;
                            light.range = 50;
                        }
                    }
                }
            }
        }
        public static void FoggyBeach(RampFog fog, String scenename, GameObject rain)
        {
            // Stormy weather. Might be a bit too murky
            fog.fogColorStart.value = new Color32(31, 46, 63, 50);
            fog.fogColorMid.value = new Color(0.205f, 0.269f, 0.288f, 0.76f);
            fog.fogColorEnd.value = new Color32(71, 82, 88, 255);
            fog.skyboxStrength.value = 0.02f;
            fog.fogPower.value = 0.35f;
            fog.fogIntensity.value = 0.99f;
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.05f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(77, 188, 175, 255);
            sunLight.intensity = 1.7f;
            sunLight.shadowStrength = 0.6f;
            // Strong rain
            if (AestheticConfig.WeatherEffects.Value)
            {
                if (scenename == "blackbeach") GameObject.Find("HOLDER: Weather Particles").transform.Find("BBSkybox").Find("CameraRelative").Find("Rain").gameObject.SetActive(false);
                var rainParticle = rain.GetComponent<ParticleSystem>();
                var epic = rainParticle.emission;
                var epic2 = epic.rateOverTime;
                epic.rateOverTime = new ParticleSystem.MinMaxCurve()
                {
                    constant = 3000,
                    constantMax = 3000,
                    constantMin = 600,
                    curve = epic2.curve,
                    curveMax = epic2.curveMax,
                    curveMin = epic2.curveMax,
                    curveMultiplier = epic2.curveMultiplier,
                    mode = epic2.mode
                };
                var epic3 = rainParticle.colorOverLifetime;
                epic3.enabled = false;
                var epic4 = rainParticle.main;
                epic4.scalingMode = ParticleSystemScalingMode.Shape;
                rain.transform.eulerAngles = new Vector3(75, 20, 0);
                rain.transform.localScale = new Vector3(14, 14, 1);
                UnityEngine.Object.Instantiate<GameObject>(rain, Vector3.zero, Quaternion.identity);
                GameObject wind = GameObject.Find("WindZone");
                wind.transform.eulerAngles = new Vector3(30, 20, 0);
                var windZone = wind.GetComponent<WindZone>();
                windZone.windMain = 1;
                windZone.windTurbulence = 1;
                windZone.windPulseFrequency = 0.5f;
                windZone.windPulseMagnitude = 0.5f;
                windZone.mode = WindZoneMode.Directional;
                windZone.radius = 100;
            }
            // Enabling some unused fog
            if (scenename == "blackbeach") GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);
            // Aiding visibility by increasing the lighting effect of the crystal pillars in both stages
            var lightList = UnityEngine.Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null)
                {
                    var lightParent = lightBase.transform.parent;
                    if (lightParent != null)
                    {
                        if (lightParent.gameObject.name.Equals("BbRuinBowl") || lightParent.gameObject.name.Equals("BbRuinBowl (1)") || lightParent.gameObject.name.Equals("BbRuinBowl (2)"))
                        {

                            light.intensity = 10;
                            light.range = 30;
                        }
                    }
                }
            }
        }
    }
}
