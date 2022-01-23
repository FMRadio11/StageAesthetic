using System;
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
            // Setting up config and hooks before the game is actually loaded
            AestheticConfig.SetConfig();
            On.RoR2.SceneDirector.Start += new On.RoR2.SceneDirector.hook_Start(SceneDirector_Start);
            SceneManager.sceneLoaded += TitlePicker;
            SceneCamera.onSceneCameraPreRender += RainCamera;
            Run.onRunStartGlobal += AestheticConfig.ApplyConfig;
            AesLog.LogMessage("Welcome to the latest update of StageAesthetics!");
            AesLog.LogMessage("Note that most of the code is run during the game itself, so just because no errors popped up here doesn't mean the mod will work.");
            AesLog.LogMessage("This has NOT been tested for cross-mod compatibility - if you're experiencing bugs that are disruptive enough to warrant a fix, sending BepInEx logs along with the bug report will make things easier. Also, note that Starstorm 2's weather and void effects will likely not mix well with this mod.");
        }
        private static void TitlePicker(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "title")
            {
                // Doing this since the title sequence isn't covered by SceneDirector.Start
                rainCheck = false;
                var menuBase = GameObject.Find("MainMenu").transform;
                // Pulling weather effects if they're enabled (this used to be part of the Weather Effects bool, but with InLobbyConfig being a thing now it'd NRE if this was disabled on load and then enabled in lobby without returning to the title screen)
                if (!rainEffect)
                {
                    rainEffect = PrefabAPI.InstantiateClone(menuBase.Find("MENU: Title").Find("World Position").Find("CameraPositionMarker").Find("Rain").gameObject, "rainT", true);
                    rainEffect.transform.eulerAngles = new Vector3(90, 0, 0);
                    AesLog.LogMessage("Rain stored in memory.");
                }
                if (!ember)
                {
                    var emberBase = GameObject.Find("HOLDER: Title Background").transform.Find("CU1 Props");
                    emberBase.gameObject.SetActive(true);
                    ember = PrefabAPI.InstantiateClone(emberBase.transform.GetChild(1).GetChild(0).gameObject, "emberT", true);
                    quad = emberBase.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;
                    emberBase.gameObject.SetActive(false);
                    AesLog.LogMessage("Embers stored in memory.");
                }
                if (!purple)
                {
                    var purpleBase = GameObject.Find("HOLDER: Title Background").transform.Find("CU3 Props");
                    purpleBase.gameObject.SetActive(true);
                    purple = PrefabAPI.InstantiateClone(purpleBase.transform.GetChild(3).Find("Embers").gameObject, "purpleT", true);
                    purpleBase.gameObject.SetActive(false);
                    AesLog.LogMessage("Purple embers stored in memory.");
                }
                // Title screen changes
                if (TitleScene.Value)
                {
                    var graphicBase = GameObject.Find("HOLDER: Title Background").transform;
                    graphicBase.Find("Terrain").gameObject.SetActive(true);
                    graphicBase.Find("CU2 Props").gameObject.SetActive(true);
                    graphicBase.Find("Misc Props").Find("DeadCommando").localPosition = new Vector3(16, -2f, 27);
                    ParticleSystem menuRain = menuBase.Find("MENU: Title").Find("World Position").Find("CameraPositionMarker").Find("Rain").gameObject.GetComponent<ParticleSystem>();
                    var epic = menuRain.emission;
                    Debug.Log("testing");
                    var epic2 = epic.rateOverTime; // 30 constant, 30 constantmax, 0 constantmin, 0 curvemultiplier
                    epic.rateOverTime = new ParticleSystem.MinMaxCurve()
                    {
                        constant = 100,
                        constantMax = 100,
                        constantMin = 60,
                        curve = epic2.curve,
                        curveMax = epic2.curveMax,
                        curveMin = epic2.curveMax,
                        curveMultiplier = epic2.curveMultiplier,
                        mode = epic2.mode
                    };
                    var epic3 = menuRain.colorOverLifetime;
                    epic3.enabled = false;
                    menuBase.Find("MENU: Title").Find("World Position").Find("CameraPositionMarker").Find("Rain").eulerAngles = new Vector3(80, 90, 0);
                    WindZone menuWind = GameObject.Find("HOLDER: Title Background").transform.Find("FX").Find("WindZone").gameObject.GetComponent<WindZone>();
                    menuWind.windMain = 0.5f;
                    menuWind.windTurbulence = 1;
                }
            }
        }
        private static void RainCamera(SceneCamera sceneCamera)
        {
            if (sceneCamera.cameraRigController && WeatherEffects.Value)
            {
                // Grabbing the scene camera's controller
                SetRain(sceneCamera.cameraRigController, true, false);
            }
        }
        private static void SetRain(CameraRigController cameraRigController, bool lockPosition, bool lockRotation)
        {
            if (rainCheck || emberCheck || purpleCheck)
            {
                // Getting the two needed objects set up
                Transform transform = cameraRigController.transform;
                if (rainCheck)
                {
                    rainObj = GameObject.Find("rainT(Clone)");
                    // Using the camera's xyz position to set the rain effect
                    if (rainObj) rainObj.transform.SetPositionAndRotation(lockPosition ? transform.position : rain.transform.position, lockRotation ? transform.rotation : rain.transform.rotation);
                }
                if (emberCheck)
                {
                    emberObj = GameObject.Find("emberT(Clone)");
                    if (emberObj) emberObj.transform.SetPositionAndRotation(lockPosition ? transform.position : ember.transform.position, lockRotation ? transform.rotation : ember.transform.rotation);
                }
                if (purpleCheck)
                {
                    purpleObj = GameObject.Find("purpleT(Clone)");
                    // Using the camera's xyz position to set the rain effect
                    if (purpleObj) purpleObj.transform.SetPositionAndRotation(lockPosition ? transform.position : purple.transform.position, lockRotation ? transform.rotation : purple.transform.rotation);
                }
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
            emberCheck = false;
            purpleCheck = false;
            // Resetting rain object between stages
            rain = rainEffect;
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
                // As of 0.1.2, I'm reintroducing color grading to help make some alts look better
                ColorGrading cgrade = volume.profile.GetSetting<ColorGrading>();
                if (cgrade == null) cgrade = cgrade = volume.profile.AddSettings<ColorGrading>();
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
                            GolemPlains.RainyPlains(fog, rain, scenename);
                            AesLog.LogInfo("Rainy Plains loaded.");
                        }
                        else if (plainsArray[counter] == "night")
                        {
                            rainCheck = true;
                            GolemPlains.NightPlains(fog, rain, cgrade);
                            AesLog.LogInfo("Night Plains loaded.");
                        }
                        else AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                    }
                    if (PlainsBridge.Value > 0)
                    {
                        int bridgeValue = Math.Min(PlainsBridge.Value - 1, 99);
                        if (UnityEngine.Random.Range(bridgeValue, 100) <= bridgeValue)
                        {
                            Transform bridgeObject = GameObject.Find("HOLDER: Ruined Pieces").transform.Find("MiniBridge");
                            if (bridgeObject != null)
                            {
                                bridgeObject.gameObject.SetActive(true);
                                bridgeObject.position = new Vector3(264.8f, -117.1f, -148.6f);
                                bridgeObject.eulerAngles = new Vector3(270, 277, 0);
                                bridgeObject.localScale = new Vector3(3.64f, 3.64f, 3.64f);
                                AesLog.LogInfo("Unused bridge loaded.");
                            }
                        }
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
                        if (roostArray[counter] == "vanilla")
                        {
                            if (scenename == "blackbeach2" && RoostChanges.Value) rainCheck = true;
                            if (RoostChanges.Value) BlackBeach.VanillaBeach(rain, scenename);
                            AesLog.LogInfo("Distant Roost loaded.");
                        }
                        else if (roostArray[counter] == "night")
                        {
                            if (scenename == "blackbeach2" && RoostChanges.Value) rainCheck = true;
                            BlackBeach.DarkBeach(fog, scenename, rain, cgrade);
                            AesLog.LogInfo("Midnight Roost loaded.");
                        }
                        else if (roostArray[counter] == "sunny")
                        {
                            BlackBeach.LightBeach(fog, scenename, cgrade);
                            AesLog.LogInfo("Sunny Roost loaded.");
                        }
                        else if (roostArray[counter] == "foggy")
                        {
                            rainCheck = true;
                            BlackBeach.FoggyBeach(fog, scenename, rain);
                            AesLog.LogInfo("Storm Roost loaded.");
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
                            emberCheck = true;
                            FoggySwamp.GoldSwamp(fog, cgrade, ember);
                            AesLog.LogInfo("Sunset Aspect loaded.");
                        }
                        else if (wetlandArray[counter] == "sky")
                        {
                            purpleCheck = true;
                            FoggySwamp.PinkSwamp(fog, cgrade, purple);
                            AesLog.LogInfo("Sky Meadow Aspect loaded.");
                        }
                        else if (wetlandArray[counter] == "dark")
                        {
                            rainCheck = true;
                            FoggySwamp.MoreSwamp(fog, rain);
                            AesLog.LogInfo("Dark Aspect loaded.");
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
                            if (AqueductChanges.Value) GooLake.VanillaChanges();
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
                            GooLake.BlueAqueduct(fog, rain);
                            AesLog.LogInfo("Rainy Aqueduct loaded.");
                        }
                        else if (aqueductArray[counter] == "nightrain")
                        {
                            rainCheck = true;
                            GooLake.NightAqueduct(fog, rain, cgrade);
                            AesLog.LogInfo("Midnight Aqueduct loaded.");
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
                            FrozenWall.NightWall(fog, cgrade);
                            AesLog.LogInfo("Night Delta loaded.");
                        }
                        else if (deltaArray[counter] == "foggy")
                        {
                            FrozenWall.OceanWall(fog);
                            AesLog.LogInfo("Foggy Delta loaded.");
                        }
                        else if (deltaArray[counter] == "green")
                        {
                            FrozenWall.GreenWall(fog, cgrade);
                            AesLog.LogInfo("Emerald Delta loaded.");
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
                            if (AcresChanges.Value) WispGraveyard.VanillaChanges();
                            AesLog.LogInfo("Scorched Acres loaded.");
                        }
                        else if (acresArray[counter] == "sunset")
                        {
                            purpleCheck = true;
                            WispGraveyard.SunsetAcres(fog, purple);
                            AesLog.LogInfo("Sunset Acres loaded.");
                        }
                        else if (acresArray[counter] == "night")
                        {
                            WispGraveyard.MoonAcres(fog);
                            AesLog.LogInfo("Midnight Acres loaded.");
                        }
                        else if (acresArray[counter] == "nothing")
                        {
                            WispGraveyard.OddAcres(fog);
                            AesLog.LogInfo("Sea Acres loaded.");
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
                            if (DepthsChanges.Value) DampCaveSimple.VanillaChanges();
                            AesLog.LogInfo("Abyssal Depths loaded.");
                        }
                        else if (depthsArray[counter] == "hive")
                        {
                            DampCaveSimple.HiveCave(fog, cgrade);
                            AesLog.LogInfo("Hive Cluster Depths loaded.");
                        }
                        else if (depthsArray[counter] == "gold")
                        {
                            DampCaveSimple.DarkCave(fog, cgrade);
                            AesLog.LogInfo("Azure Depths loaded.");
                        }
                        else if (depthsArray[counter] == "sky")
                        {
                            purpleCheck = true;
                            DampCaveSimple.MeadowCave(fog, purple);
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
                            ShipGraveyard.ShipNight(fog, cgrade);
                            AesLog.LogInfo("Night's Call loaded.");
                        }
                        else if (sirenArray[counter] == "sunny")
                        {
                            ShipGraveyard.ShipSkies(fog);
                            AesLog.LogInfo("Siren's Sun loaded.");
                        }
                        else if (sirenArray[counter] == "storm")
                        {
                            rainCheck = true;
                            ShipGraveyard.ShipDeluge(fog, rain);
                            AesLog.LogInfo("Siren's Storm loaded.");
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
                            RootJungle.GreenJungle(fog, cgrade);
                            AesLog.LogInfo("Olive Grove loaded.");
                        }
                        else if (groveArray[counter] == "sunny")
                        {
                            RootJungle.SunJungle(fog, cgrade);
                            AesLog.LogInfo("Sunny Grove loaded.");
                        }
                        else if (groveArray[counter] == "storm")
                        {
                            rainCheck = true;
                            RootJungle.StormJungle(fog, rain, cgrade);
                            AesLog.LogInfo("Overcast Grove loaded.");
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
                            purpleCheck = true;
                            if (MeadowChanges.Value) SkyMeadow.VanillaChanges(purple);
                            AesLog.LogInfo("Sky Meadow loaded.");
                        }
                        else if (meadowArray[counter] == "night")
                        {
                            purpleCheck = true;
                            SkyMeadow.NightMeadow(fog, purple);
                            AesLog.LogInfo("Night Meadow loaded.");
                        }
                        else if (meadowArray[counter] == "storm")
                        {
                            SkyMeadow.StormyMeadow(fog);
                            AesLog.LogInfo("Stormy Meadow loaded.");
                        }
                        else if (meadowArray[counter] == "abyss")
                        {
                            emberCheck = true;
                            SkyMeadow.EpicMeadow(fog, cgrade, ember);
                            AesLog.LogInfo("Abyssal Meadow loaded.");
                        }
                        else AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                    }
                    meadowVariant = counter;
                }
                // Currently gives a null error
                /*if (scenename == "goldshores" && GildedAlt.Value)
                {
                    GameObject.Find("GoldShoresSkybox").transform.GetChild(1).gameObject.SetActive(true);
                }*/
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
                // Currently gives a null error
                /*Transform moonBase = GameObject.Find("HOLDER: Effects, Lights, Etc").transform;
                Transform sunBase = moonBase.Find("Light Direction").Find("SunHolder");
                sunBase.position = new Vector3(2400, 200, -2500);
                sunBase.localScale = new Vector3(70, 100, 100);
                Transform lightBase = moonBase.Find("Directional Light (SUN)");
                lightBase.eulerAngles = new Vector3(7, 350, 2);
                lightBase.gameObject.GetComponent<Light>().intensity = 1.2f;*/
            }
            else AesLog.LogWarning("Post process volume could not be found.");
            if (!epic && Run.instance.stageClearCount >= 5) GooLake.IgnoreThis(AestheticConfig.warning2);
        }
        // Used for uh everything
        public static PostProcessVolume volume;
        // All of these are set to -1 in order to allow all variants to spawn when first loaded
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
        // Used to store a dummy volume to use in Commencement
        public static PostProcessProfile commencementVolume;
        // Used for rain effect
        public static GameObject rainEffect;
        public static GameObject rain;
        public static GameObject rainObj;
        public static GameObject ember;
        public static GameObject emberObj;
        public static GameObject purple;
        public static GameObject purpleObj;
        public static GameObject quad;
        // Used during SceneCamera hook
        public static bool rainCheck;
        public static bool emberCheck;
        public static bool purpleCheck;
        // Custom log
        internal static BepInEx.Logging.ManualLogSource AesLog;
        // Plains
        public static ConfigEntry<bool> VanillaPlains { get; set; }
        public static ConfigEntry<bool> PlainsChanges { get; set; }
        public static ConfigEntry<bool> SunsetPlains { get; set; }
        public static ConfigEntry<bool> RainyPlains { get; set; }
        public static ConfigEntry<bool> NightPlains { get; set; }
        public static ConfigEntry<int> PlainsBridge { get; set; }
        // Roost
        public static ConfigEntry<bool> VanillaRoost { get; set; }
        public static ConfigEntry<bool> RoostChanges { get; set; }
        public static ConfigEntry<bool> SunnyRoost { get; set; }
        public static ConfigEntry<bool> NightRoost { get; set; }
        public static ConfigEntry<bool> FoggyRoost { get; set; }
        // Wetland
        public static ConfigEntry<bool> VanillaWetland { get; set; }
        public static ConfigEntry<bool> SunsetWetland { get; set; }
        public static ConfigEntry<bool> SkyWetland { get; set; }
        public static ConfigEntry<bool> EveningWetland { get; set; }
        // Aqueduct
        public static ConfigEntry<bool> VanillaAqueduct { get; set; }
        public static ConfigEntry<bool> AqueductChanges { get; set; }
        public static ConfigEntry<bool> NightAqueduct { get; set; }
        public static ConfigEntry<bool> RainyAqueduct { get; set; }
        public static ConfigEntry<bool> MistyAqueduct { get; set; }
        // Delta
        public static ConfigEntry<bool> VanillaDelta { get; set; }
        public static ConfigEntry<bool> NightDelta { get; set; }
        public static ConfigEntry<bool> FoggyDelta { get; set; }
        public static ConfigEntry<bool> PurpleDelta { get; set; }
        // Acres
        public static ConfigEntry<bool> VanillaAcres { get; set; }
        public static ConfigEntry<bool> AcresChanges { get; set; }
        public static ConfigEntry<bool> SunsetAcres { get; set; }
        public static ConfigEntry<bool> NightAcres { get; set; }
        public static ConfigEntry<bool> BlueAcres { get; set; }
        // Depths
        public static ConfigEntry<bool> VanillaDepths { get; set; }
        public static ConfigEntry<bool> DepthsChanges { get; set; }
        public static ConfigEntry<bool> DarkDepths { get; set; }
        public static ConfigEntry<bool> SkyDepths { get; set; }
        public static ConfigEntry<bool> BlueDepths { get; set; }
        // Grove
        public static ConfigEntry<bool> VanillaGrove { get; set; }
        public static ConfigEntry<bool> GreenGrove { get; set; }
        public static ConfigEntry<bool> SunnyGrove { get; set; }
        public static ConfigEntry<bool> HannibalGrove { get; set; }
        // Siren
        public static ConfigEntry<bool> VanillaSiren { get; set; }
        public static ConfigEntry<bool> NightSiren { get; set; }
        public static ConfigEntry<bool> SunnySiren { get; set; }
        public static ConfigEntry<bool> MistySiren { get; set; }
        // Meadow
        public static ConfigEntry<bool> VanillaMeadow { get; set; }
        public static ConfigEntry<bool> MeadowChanges { get; set; }
        public static ConfigEntry<bool> NightMeadow { get; set; }
        public static ConfigEntry<bool> StormyMeadow { get; set; }
        public static ConfigEntry<bool> CrimsonMeadow { get; set; }
        // Base Config
        public static ConfigFile AesConfig { get; set; }
        public static ConfigEntry<bool> CommencementAlt { get; set; }
        public static ConfigEntry<bool> GildedAlt { get; set; }
        public static ConfigEntry<bool> TitleScene { get; set; }
        public static ConfigEntry<bool> WeatherEffects { get; set; }
        // String lists for config
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
        // don't worry about this :)
        public static bool epic = false;
    }
}
