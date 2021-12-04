using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using UnityEngine;

namespace StageAesthetic.Stages
{
    class BlackBeach
    {
        public static void LightBeach(RampFog fog, String scenename)
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
            if (scenename == "blackbeach")
            {
                // There's unused fog assets here too, enabling those
                GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("SKYBOX").transform.GetChild(4).gameObject.SetActive(true);
            }
        }
        public static void DarkBeach(RampFog fog, String scenename)
        {
            // Dark and purple, not much else to say here really
            fog.fogColorStart.value = new Color32(24, 20, 43, 176);
            fog.fogColorMid.value = new Color32(33, 25, 49, 193);
            fog.fogColorEnd.value = new Color32(43, 35, 62, 255);
            fog.skyboxStrength.value = 0.03f;
            fog.fogPower.value = 0.6f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(106, 69, 160, 255);
            sunLight.intensity = 1f;
            sunLight.shadowStrength = 0.45f;
            Transform[] lightArray = new Transform[] { };
            if (scenename == "blackbeach")
            {
                // Picking up default roost lights
                Transform base1 = GameObject.Find("GAMEPLAY SPACE").transform;
                Transform base2 = base1.Find("GATED STUFF").Find("DownstairsGate, Open");
                lightArray = new Transform[] { base1.Find("BbRuinBowl"), base1.Find("BbRuinBowl (2)"), base1.Find("BbRuinMarker_LOD0 (2)").Find("BbRuinBowl (1)"), base1.Find("BbRuinMarker_LOD0").Find("BbRuinBowl"), base1.Find("BbRuinMarker_LOD0 (16)").Find("BbRuinBowl"), base1.Find("BbRuinMarker_LOD0 (17)").Find("BbRuinBowl"), base1.Find("BbRuinMarker_LOD0 (18)").Find("BbRuinBowl (1)"), base1.Find("BbRuinMarker_LOD0 (19)").Find("BbRuinBowl (1)"), base1.Find("BbRuinMarker_LOD0 (20)").Find("BbRuinBowl (1)"), base1.Find("BbRuinMarker_LOD0 (21)").Find("BbRuinBowl (1)"), base2.Find("BbRuinMarker_LOD0 (12)").Find("BbRuinBowl"), base2.Find("BbRuinMarker_LOD0 (13)").Find("BbRuinBowl") };
                // There's unused fog assets here too, enabling those
                GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("SKYBOX").transform.GetChild(4).gameObject.SetActive(true);
            }
            else if (scenename == "blackbeach2")
            {
                // Picking up alt roost lights
                Transform base1 = GameObject.Find("HOLDER: Ruins").transform;
                Transform base2 = base1.Find("GROUP: Markers");
                Transform base3 = base1.Find("GROUP: Gates").Find("DownstairsGate, Open");
                lightArray = new Transform[] { base2.Find("BbRuinMarker_LOD0 (16)").Find("BbRuinBowl"), base2.Find("BbRuinMarker_LOD0 (17)").Find("BbRuinBowl"), base2.Find("BbRuinMarker_LOD0 (18)").Find("BbRuinBowl"), base2.Find("BbRuinMarker_LOD0 (20)").Find("BbRuinBowl"), base2.Find("BbRuinMarker_LOD0 (21)").Find("BbRuinBowl"), base2.Find("BbRuinMarker_LOD0 (22)").Find("BbRuinBowl"), base2.Find("BbRuinMarker_LOD0 (23)").Find("BbRuinBowl"), base2.Find("BbRuinMarker_LOD0 (24)").Find("BbRuinBowl"), base2.Find("BbRuinMarker_LOD0 (27)").Find("BbRuinBowl"), base2.Find("BbRuinMarker_LOD0 (2)").Find("BbRuinBowl (1)"), base3.Find("BbRuinMarker_LOD0 (12)").Find("BbRuinBowl"), base3.Find("BbRuinMarker_LOD0 (13)").Find("BbRuinBowl") };
            }
            foreach (Transform bowl in lightArray)
            {
                Light bowlLight = bowl.Find("Point light").gameObject.GetComponent<Light>();
                bowlLight.intensity = 15;
                bowlLight.range = 50;
            }
        }
    }
}
