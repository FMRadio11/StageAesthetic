﻿using System;
using R2API;
using UnityEngine;
using RoR2;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using StageAesthetic.Stages;
using BepInEx.Configuration;
using System.Collections.Generic;

namespace StageAesthetic
{

    public class Aesthetic
    {
        public static void Nice()
        {
            AestheticConfig.SetConfig();
            On.RoR2.SceneDirector.Start += new On.RoR2.SceneDirector.hook_Start(SceneDirector_Start);
            SceneManager.sceneLoaded += TitlePicker;
            if (WeatherEffects.Value) SceneCamera.onSceneCameraPreRender += RainCamera;
            AesLog.LogMessage("Welcome to the latest update of StageAesthetics!");
            AesLog.LogMessage("Note that most of the code is run during the game itself, so just because no errors popped up here doesn't mean the mod will work.");
            AesLog.LogMessage("This has NOT been tested for cross-mod compatibility - if you're experiencing bugs that are disruptive enough to warrant a fix, sending BepInEx logs along with the bug report will make things easier. Also, note that Starstorm 2's weather and void effects will likely not mix well with this mod.");
        }
        private static void TitlePicker(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "title")
            {
                rainCheck = false;
                var graphicBase = GameObject.Find("HOLDER: Title Background").transform;
                graphicBase.Find("Terrain").gameObject.SetActive(true);
                graphicBase.Find("CU2 Props").gameObject.SetActive(true);
                graphicBase.Find("Misc Props").Find("DeadCommando").localPosition = new Vector3(16, -2f, 27);
                var menuBase = GameObject.Find("MainMenu").transform;
                if (!rainEffect && WeatherEffects.Value)
                {
                    rainEffect = PrefabAPI.InstantiateClone(menuBase.Find("MENU: Title").Find("World Position").Find("CameraPositionMarker").Find("Rain").gameObject, "rainClone", true);
                    rainEffect.transform.eulerAngles = new Vector3(90, 0, 0);
                    AesLog.LogInfo("Rain object stored in memory.");
                }
            }
        }
        private static void RainCamera(SceneCamera sceneCamera)
        {
            if (sceneCamera.cameraRigController)
            {
                SetRain(sceneCamera.cameraRigController, true, false);
            }
        }
        private static void SetRain(CameraRigController cameraRigController, bool lockPosition, bool lockRotation)
        {
            if (rainCheck)
            {
                Transform transform = cameraRigController.transform;
                rainObj = GameObject.Find("rainClone(Clone)");
                if (rainObj) rainObj.transform.SetPositionAndRotation(lockPosition ? transform.position : rainEffect.transform.position, lockRotation ? transform.rotation : rainEffect.transform.rotation);
            }
        }
        private static void SceneDirector_Start(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
        {
            ChangeProfile(SceneManager.GetActiveScene().name);
            orig(self);
        }
        private static void ChangeProfile(string scenename)
        {
            // Disabling weather checks
            rainCheck = false;
            // Loading in the current PostProcessVolume from SceneInfo
            SceneInfo currentScene = SceneInfo.instance;
            if (currentScene) volume = currentScene.GetComponent<PostProcessVolume>();
            // Some stages keep post-processing in a dedicated "Weather" folder because ??? inconsistent code moment
            // The following checks for three options used by various stages, which should leave Commencement as the only null value
            if (!volume)
            {
                GameObject alt = GameObject.Find("PP + Amb");
                if (!alt) alt = GameObject.Find("PP, Global");
                if (!alt) alt = GameObject.Find("GlobalPostProcessVolume, Base");
                if (alt) volume = alt.GetComponent<PostProcessVolume>();
                else volume = null;
            }
            if (volume && scenename != "moon2")
            {
                // Pretty much every variant uses RampFog, and it always shows up in the post-processing volume so it's put into an easy variable for transferring to other files.
                RampFog fog = volume.profile.GetSetting<RampFog>();
                // Commencement does not natively have a profile, so I'm borrowing it from the first stage in memory.
                if (Run.instance.stageClearCount == 0 && CommencementAlt.Value)
                {
                    commencementVolume = volume.profile;
                    AesLog.LogInfo("Commencement volume set: " + commencementVolume);
                }
                // Moving onto the big list of stage changes. I'll comment the Titanic Plains one for context, but the rest won't be.
                // Due to Titanic Plains and Distant Roost having two different variations, the if statement for them is an OR check for both.
                if (scenename == "golemplains" || scenename == "golemplains2")
                {
                    // Setting up a random number between 0 and the total number of strings in the stage's list. This sets the maximum number to 
                    int counter = UnityEngine.Random.Range(0, plainsList.Count);
                    // Implementing a do-while loop to force a new variant whenever a stage is reloaded:
                    if (plainsList.Count > 1) do counter = UnityEngine.Random.Range(0, plainsList.Count); while (counter == plainsVariant);
                    // Converting the list to a string array so I can pull values based off of index. There's probably a better way to do this, but...
                    String[] plainsArray = plainsList.ToArray();
                    if (counter == plainsList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                    else
                    {
                        // Checking the currently active value against the array's available variants.
                        if (plainsArray[counter] == "vanilla")
                        {
                            if (PlainsChanges.Value) GolemPlains.VanillaChanges();
                            AesLog.LogInfo("Titanic Plains loaded.");
                        }
                        else if (plainsArray[counter] == "sunset")
                        {
                            GolemPlains.SunsetPlains(fog);
                            AesLog.LogInfo("Sunset Plains loaded.");
                        }
                        else if (plainsArray[counter] == "rain")
                        {
                            rainCheck = true;
                            GolemPlains.RainyPlains(fog, rainEffect);
                            AesLog.LogInfo("Rainy Plains loaded.");
                        }
                        else AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                    }
                    // Finally, the active variant is stored for the next time this stage is loaded.
                    plainsVariant = counter;
                }
                if (scenename == "blackbeach" || scenename == "blackbeach2")
                {
                    int counter = UnityEngine.Random.Range(0, roostList.Count);
                    if (roostList.Count > 1) do counter = UnityEngine.Random.Range(0, roostList.Count); while (counter == roostVariant);
                    String[] roostArray = roostList.ToArray();
                    if (counter == roostList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                    else
                    {
                        if (roostArray[counter] == "vanilla") AesLog.LogInfo("Distant Roost loaded.");
                        else if (roostArray[counter] == "night")
                        {
                            BlackBeach.DarkBeach(fog, scenename);
                            AesLog.LogInfo("Midnight Roost loaded.");
                        }
                        else if (roostArray[counter] == "sunny")
                        {
                            BlackBeach.LightBeach(fog, scenename);
                            AesLog.LogInfo("Sunny Roost loaded.");
                        }
                        else AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                    }
                    roostVariant = counter;
                }
                if (scenename == "foggyswamp")
                {
                    int counter = UnityEngine.Random.Range(0, wetlandList.Count);
                    if (wetlandList.Count > 1) do counter = UnityEngine.Random.Range(0, wetlandList.Count); while (counter == wetlandVariant);
                    String[] wetlandArray = wetlandList.ToArray();
                    if (counter == wetlandList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                    else
                    {
                        if (wetlandArray[counter] == "vanilla") AesLog.LogInfo("Wetland Aspect loaded.");
                        else if (wetlandArray[counter] == "sunset")
                        {
                            FoggySwamp.GoldSwamp(fog);
                            AesLog.LogInfo("Sunset Aspect loaded.");
                        }
                        else if (wetlandArray[counter] == "sky")
                        {
                            FoggySwamp.PinkSwamp(fog);
                            AesLog.LogInfo("Sky Meadow Aspect loaded.");
                        }
                        else AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                    }
                    wetlandVariant = counter;
                }
                if (scenename == "goolake")
                {
                    int counter = UnityEngine.Random.Range(0, aqueductList.Count);
                    if (aqueductList.Count > 1) do counter = UnityEngine.Random.Range(0, aqueductList.Count); while (counter == aqueductVariant);
                    String[] aqueductArray = aqueductList.ToArray();
                    if (counter == aqueductList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                    else
                    {
                        if (aqueductArray[counter] == "vanilla")
                        {
                            GooLake.VanillaChanges();
                            AesLog.LogInfo("Abandoned Aqueduct loaded.");
                        }
                        else if (aqueductArray[counter] == "night")
                        {
                            GooLake.DarkAqueduct(fog);
                            AesLog.LogInfo("Evening Aqueduct loaded.");
                        }
                        else if (aqueductArray[counter] == "rain")
                        {
                            rainCheck = true;
                            GooLake.BlueAqueduct(fog, rainEffect);
                            AesLog.LogInfo("Rainy Aqueduct loaded.");
                        }
                        else AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                    }
                    aqueductVariant = counter;
                }
                if (scenename == "frozenwall")
                {
                    int counter = UnityEngine.Random.Range(0, deltaList.Count);
                    if (deltaList.Count > 1) do counter = UnityEngine.Random.Range(0, deltaList.Count); while (counter == deltaVariant);
                    String[] deltaArray = deltaList.ToArray();
                    if (counter == deltaList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                    else
                    {
                        if (deltaArray[counter] == "vanilla") AesLog.LogInfo("Rallypoint Delta loaded.");
                        else if (deltaArray[counter] == "night")
                        {
                            FrozenWall.NightWall(fog);
                            AesLog.LogInfo("Rallypoint Night loaded.");
                        }
                        else if (deltaArray[counter] == "foggy")
                        {
                            FrozenWall.OceanWall(fog);
                            AesLog.LogInfo("Rallypoint Fog loaded.");
                        }
                        else AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                    }
                    deltaVariant = counter;
                }
                if (scenename == "wispgraveyard")
                {
                    int counter = UnityEngine.Random.Range(0, acresList.Count);
                    if (acresList.Count > 1) do counter = UnityEngine.Random.Range(0, acresList.Count); while (counter == acresVariant);
                    String[] acresArray = acresList.ToArray();
                    if (counter == acresList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                    else
                    {
                        if (acresArray[counter] == "vanilla")
                        {
                            WispGraveyard.VanillaChanges();
                            AesLog.LogInfo("Scorched Acres loaded.");
                        }
                        else if (acresArray[counter] == "sunset")
                        {
                            WispGraveyard.SunsetAcres(fog);
                            AesLog.LogInfo("Sunset Acres loaded.");
                        }
                        else if (acresArray[counter] == "night")
                        {
                            rainCheck = true;
                            WispGraveyard.MoonAcres(fog, rainEffect);
                            AesLog.LogInfo("Midnight Acres loaded.");
                        }
                        else AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                    }
                    acresVariant = counter;
                }
                if (scenename == "dampcavesimple")
                {
                    int counter = UnityEngine.Random.Range(0, depthsList.Count);
                    if (depthsList.Count > 1) do counter = UnityEngine.Random.Range(0, depthsList.Count); while (counter == depthsVariant);
                    String[] depthsArray = depthsList.ToArray();
                    if (counter == depthsList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                    else
                    {
                        if (depthsArray[counter] == "vanilla")
                        {
                            DampCaveSimple.VanillaChanges();
                            AesLog.LogInfo("Abyssal Depths loaded.");
                        }
                        else if (depthsArray[counter] == "dark")
                        {
                            DampCaveSimple.DarkCave(fog);
                            AesLog.LogInfo("Dark Depths loaded.");
                        }
                        else if (depthsArray[counter] == "sky")
                        {
                            rainCheck = true;
                            DampCaveSimple.MeadowCave(fog, rainEffect);
                            AesLog.LogInfo("Sky Meadow Depths loaded.");
                        }
                        else AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                    }
                    depthsVariant = counter;
                }
                if (scenename == "shipgraveyard")
                {
                    int counter = UnityEngine.Random.Range(0, sirenList.Count);
                    if (sirenList.Count > 1) do counter = UnityEngine.Random.Range(0, sirenList.Count); while (counter == sirenVariant);
                    String[] sirenArray = sirenList.ToArray();
                    if (counter == sirenList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                    else
                    {
                        if (sirenArray[counter] == "vanilla") AesLog.LogInfo("Siren's Call loaded.");
                        else if (sirenArray[counter] == "night")
                        {
                            ShipGraveyard.ShipNight(fog);
                            AesLog.LogInfo("Night's Call loaded.");
                        }
                        else if (sirenArray[counter] == "sunny")
                        {
                            ShipGraveyard.ShipSkies(fog);
                            AesLog.LogInfo("Siren's Sun loaded.");
                        }
                        else AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                    }
                    sirenVariant = counter;
                }
                if (scenename == "rootjungle")
                {
                    int counter = UnityEngine.Random.Range(0, groveList.Count);
                    if (groveList.Count > 1) do counter = UnityEngine.Random.Range(0, groveList.Count); while (counter == groveVariant);
                    String[] groveArray = groveList.ToArray();
                    if (counter == groveList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                    else
                    {
                        if (groveArray[counter] == "vanilla") AesLog.LogInfo("Sundered Grove loaded.");
                        else if (groveArray[counter] == "green")
                        {
                            RootJungle.GreenJungle(fog);
                            AesLog.LogInfo("Olive Grove loaded.");
                        }
                        else if (groveArray[counter] == "sunny")
                        {
                            RootJungle.SunJungle(fog);
                            AesLog.LogInfo("Sunny Grove loaded.");
                        }
                        else AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                    }
                    groveVariant = counter;
                }
                if (scenename == "skymeadow")
                {
                    int counter = UnityEngine.Random.Range(0, meadowList.Count);
                    if (meadowList.Count > 1) do counter = UnityEngine.Random.Range(0, meadowList.Count); while (counter == meadowVariant);
                    String[] meadowArray = meadowList.ToArray();
                    if (counter == meadowList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                    else
                    {
                        if (meadowArray[counter] == "vanilla")
                        {
                            SkyMeadow.VanillaChanges();
                            AesLog.LogInfo("Sky Meadow loaded.");
                        }
                        else if (meadowArray[counter] == "night")
                        {
                            SkyMeadow.NightMeadow(fog);
                            AesLog.LogInfo("Night Meadow loaded.");
                        }
                        else if (meadowArray[counter] == "storm")
                        {
                            SkyMeadow.StormyMeadow(fog);
                            AesLog.LogInfo("Stormy Meadow loaded.");
                        }
                        else AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                    }
                    meadowVariant = counter;
                }
            }
            else if (scenename == "moon2" && CommencementAlt.Value)
            {
                volume = currentScene.gameObject.AddComponent<PostProcessVolume>();
                volume.enabled = true;
                volume.isGlobal = true;
                volume.priority = 9999f;
                volume.profile = commencementVolume;
                RampFog fog = volume.profile.GetSetting<RampFog>();
                fog.fogColorStart.value = new Color(0.06f, 0.07f, 0.12f, 0.4f);
                fog.fogColorMid.value = new Color(0.13f, 0.14f, 0.19f, 0.625f);
                fog.fogColorEnd.value = new Color(0.2f, 0.21f, 0.27f, 1f);
                fog.skyboxStrength.value = 0f;
            }
            else AesLog.LogWarning("Post process volume could not be found.");
            if (!epic && Run.instance.stageClearCount >= 5) GooLake.IgnoreThis(AestheticConfig.warning2);
        }
        public static int networkCounter;
        public static PostProcessVolume volume;
        public static int plainsVariant = -1;
        public static int roostVariant = -1;
        public static int wetlandVariant = -1;
        public static int aqueductVariant = -1;
        public static int deltaVariant = -1;
        public static int acresVariant = -1;
        public static int depthsVariant = -1;
        public static int sirenVariant = -1;
        public static int groveVariant = -1;
        public static int meadowVariant = -1;
        public static int currentVariant;
        public static PostProcessProfile commencementVolume;
        public static GameObject rainEffect;
        public static GameObject rainObj;
        public static bool rainCheck;
        internal static BepInEx.Logging.ManualLogSource AesLog;
        public static ConfigEntry<bool> VanillaPlains;
        public static ConfigEntry<bool> PlainsChanges;
        public static ConfigEntry<bool> SunsetPlains;
        public static ConfigEntry<bool> RainyPlains;
        public static ConfigEntry<bool> VanillaRoost;
        public static ConfigEntry<bool> SunnyRoost;
        public static ConfigEntry<bool> NightRoost;
        public static ConfigEntry<bool> VanillaWetland;
        public static ConfigEntry<bool> SunsetWetland;
        public static ConfigEntry<bool> SkyWetland;
        public static ConfigEntry<bool> VanillaAqueduct;
        public static ConfigEntry<bool> AqueductChanges;
        public static ConfigEntry<bool> NightAqueduct;
        public static ConfigEntry<bool> RainyAqueduct;
        public static ConfigEntry<bool> VanillaDelta;
        public static ConfigEntry<bool> NightDelta;
        public static ConfigEntry<bool> FoggyDelta;
        public static ConfigEntry<bool> VanillaAcres;
        public static ConfigEntry<bool> AcresChanges;
        public static ConfigEntry<bool> SunsetAcres;
        public static ConfigEntry<bool> NightAcres;
        public static ConfigEntry<bool> VanillaDepths;
        public static ConfigEntry<bool> DepthsChanges;
        public static ConfigEntry<bool> DarkDepths;
        public static ConfigEntry<bool> SkyDepths;
        public static ConfigEntry<bool> VanillaGrove;
        public static ConfigEntry<bool> GreenGrove;
        public static ConfigEntry<bool> SunnyGrove;
        public static ConfigEntry<bool> VanillaSiren;
        public static ConfigEntry<bool> NightSiren;
        public static ConfigEntry<bool> SunnySiren;
        public static ConfigEntry<bool> VanillaMeadow;
        public static ConfigEntry<bool> MeadowChanges;
        public static ConfigEntry<bool> NightMeadow;
        public static ConfigEntry<bool> StormyMeadow;
        public static ConfigFile AesConfig { get; set; }
        public static ConfigEntry<bool> CommencementAlt { get; set; }
        public static ConfigEntry<bool> VoidAlt;
        public static ConfigEntry<bool> GildedAlt;
        public static ConfigEntry<bool> WeatherEffects { get; set; }
        public static List<String> plainsList = new List<string>();
        public static List<String> roostList = new List<string>();
        public static List<String> wetlandList = new List<string>();
        public static List<String> aqueductList = new List<string>();
        public static List<String> deltaList = new List<string>();
        public static List<String> acresList = new List<string>();
        public static List<String> depthsList = new List<string>();
        public static List<String> groveList = new List<string>();
        public static List<String> sirenList = new List<string>();
        public static List<String> meadowList = new List<string>();
        public static bool epic = false;
    }
}
