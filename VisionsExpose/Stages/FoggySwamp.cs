using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Stages
{
    class FoggySwamp
    {
        public static void PinkSwamp(RampFog fog, ColorGrading cgrade, GameObject purple)
        {
            fog.fogColorStart.value = new Color32(90, 69, 105, 13);
            fog.fogColorMid.value = new Color32(130, 105, 154, 161);
            fog.fogColorEnd.value = new Color32(169, 119, 227, 255);
            cgrade.colorFilter.value = new Color32(233, 189, 245, 255);
            cgrade.colorFilter.overrideState = true;
            fog.skyboxStrength.value = 0;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(198, 152, 223, 255);
            sunLight.intensity = 0.9f;
            var caveOuter = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("Blended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            var caveInner = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("NonBlended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveOuter.fogColorStart.value = new Color32(124, 86, 109, 0);
            caveOuter.fogColorMid.value = new Color32(154, 89, 127, 89);
            caveOuter.fogColorEnd.value = new Color32(227, 118, 219, 255);
            caveInner.fogColorStart.value = new Color32(131, 86, 94, 0);
            caveInner.fogColorMid.value = new Color32(137, 22, 24, 89);
            caveInner.fogColorEnd.value = new Color32(152, 8, 6, 255);
            if (Aesthetic.WeatherEffects.Value) UnityEngine.Object.Instantiate<GameObject>(purple, Vector3.zero, Quaternion.identity);
        }
        public static void GoldSwamp(RampFog fog, ColorGrading cgrade, GameObject ember)
        {
            fog.fogColorStart.value = new Color32(129, 94, 43, 9);
            fog.fogColorMid.value = new Color32(131, 96, 37, 135);
            fog.fogColorEnd.value = new Color32(129, 90, 34, 255);
            cgrade.colorFilter.value = new Color32(251, 199, 180, 255);
            cgrade.colorFilter.overrideState = true;
            fog.skyboxStrength.value = 0;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(209, 119, 47, 255);
            var caveOuter = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("Blended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            var caveInner = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("NonBlended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveOuter.fogColorStart.value = new Color32(127, 124, 84, 0);
            caveOuter.fogColorMid.value = new Color32(188, 163, 47, 88);
            caveOuter.fogColorEnd.value = new Color32(162, 123, 46, 255);
            caveInner.fogColorStart.value = new Color32(162, 192, 5, 0);
            caveInner.fogColorMid.value = new Color32(149, 154, 89, 89);
            caveInner.fogColorEnd.value = new Color32(217, 201, 11, 255);
            if (Aesthetic.WeatherEffects.Value) UnityEngine.Object.Instantiate<GameObject>(ember, Vector3.zero, Quaternion.identity);
        }
        public static void MoreSwamp(RampFog fog, GameObject rain)
        {
            fog.fogColorStart.value = new Color32(33, 43, 41, 87);
            fog.fogColorMid.value = new Color32(45, 60, 51, 173);
            fog.fogColorEnd.value = new Color32(47, 60, 48, 255);
            fog.fogOne.value = 0.355f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(128, 205, 170, 255);
            sunLight.intensity = 0.32f;
            sunLight.shadowStrength = 0.477f;
            if (AestheticConfig.WeatherEffects.Value)
            {
                var rainParticle = rain.GetComponent<ParticleSystem>();
                var epic = rainParticle.emission;
                var epic2 = epic.rateOverTime;
                epic.rateOverTime = new ParticleSystem.MinMaxCurve()
                {
                    constant = 700,
                    constantMax = 700,
                    constantMin = 220,
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
                rain.transform.eulerAngles = new Vector3(87, 110, 0);
                rain.transform.localScale = new Vector3(14, 14, 1);
                UnityEngine.Object.Instantiate<GameObject>(rain, Vector3.zero, Quaternion.identity);
            }
            var caveOuter = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("Blended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveOuter.fogColorStart.value = new Color32(14, 111, 160, 0);
            caveOuter.fogColorMid.value = new Color32(66, 76, 43, 89);
            caveOuter.fogColorEnd.value = new Color32(75, 84, 51, 255);
        }
    }
}
