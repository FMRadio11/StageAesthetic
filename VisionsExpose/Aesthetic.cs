using System;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using UnityEngine;
using RoR2;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Reflection;
using System.IO;
using BepInEx;
using BepInEx.Configuration;

namespace VisionsExpose
{

    public class Aesthetic
    {
        public static void Nice()
        {
            using (Stream iconStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("StageAesthetic.aesthetic"))
            {
                artifactIcon = AssetBundle.LoadFromStream(iconStream);
            }
            ConfigSetup();
            SeasonsSetup();
            MonsterLists.CardSetup();
            On.RoR2.SceneDirector.Start += new On.RoR2.SceneDirector.hook_Start(SceneDirector_Start);
        }
        private static void ConfigSetup()
        {
            AesConfig = new ConfigFile(Paths.ConfigPath + "\\StageAesthetic.cfg", true);
            CommencementAlt = AesConfig.Bind<bool>("Core", "Commencement alt?", true, "Enable this to use the alternate palette of Commencement. Note that this will break if you enter Commencement without clearing a stage first!");
            StageRestriction = AesConfig.Bind<bool>("Core", "Restrict variants/bosses to stage count?", true, "Some variants and bosses will only appear after a certain amount of stages are clear. Disabling this will increase the likelihood of horde encounters early.");
            BossConfig = AesConfig.Bind<bool>("Mod Compatibility", "BossConfig compatibility?", false, "Set this to true to remove the boss-specific changes Artifact of Seasons makes.");
        }
        private static void SeasonsSetup()
        {
            seasons = ScriptableObject.CreateInstance<ArtifactDef>();
            seasons.smallIconSelectedSprite = artifactIcon.LoadAsset<Sprite>("seasonson");
            seasons.smallIconDeselectedSprite = artifactIcon.LoadAsset<Sprite>("seasonsoff");
            seasons.nameToken = "Artifact of Seasons";
            String seasonsDesc = "Alternate stage palettes now use their own monster lists.";
            if (StageRestriction.Value) seasonsDesc = seasonsDesc + " Some palettes will not appear until a certain stage count is met.";
            seasons.descriptionToken = seasonsDesc;
            seasons.unlockableDef = null;
            seasons.pickupModelPrefab = null;
            ArtifactAPI.Add(seasons);
        }
        private static void SceneDirector_Start(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
        {
            seasonActive = RunArtifactManager.instance.IsArtifactEnabled(seasons.artifactIndex) && RunArtifactManager.instance;
            ChangeProfile(SceneManager.GetActiveScene().name);
            orig(self);
        }
        private static void ChangeProfile(string scenename)
        {
            // Loading in the current PostProcessVolume
            SceneInfo currentScene = SceneInfo.instance;
            if (currentScene) volume = currentScene.GetComponent<PostProcessVolume>();
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
                // Needed for the aesthetical portion of the mod
                RampFog fog = volume.profile.GetSetting<RampFog>();
                if (Run.instance.stageClearCount == 0 && CommencementAlt.Value)
                {
                    commencementVolume = volume.profile;
                    Debug.Log("Commencement volume set: " + commencementVolume);
                }
                // Moving onto the big list of stage changes. I'll comment the Titanic Plains one for context, but the rest won't be.
                // Due to Titanic Plains and Distant Roost having two different variations, the if statement for them is an OR check for both.
                if (scenename == "golemplains" || scenename == "golemplains2")
                {
                    // Setting up a random number between 1-3. This shouldn't give 4, but if it does I think it just returns the default stage.
                    int counter = UnityEngine.Random.Range(1, 4);
                    // This should??? sync clients to use the same variant in multiplayer, but I haven't had a chance to test it.
                    //new AestheticSync(counter).Send(NetworkDestination.Clients);
                    //counter = networkCounter;
                    // Setting up a bool to check whether A. the variant used is the same as before or B. Seasons was not previously active on this stage.
                    bool useList = plainsVariant != counter || !plainsCheck;
                    // Checking for whether the Seasons artifact is active, and that the previous variant wasn't either 1 or identical to the one being prepared. If all of these are true, PlainsReset() is run to clear out any previous monster changes.
                    if (seasonActive && plainsVariant != 1 && plainsVariant != counter) MonsterLists.PlainsReset();
                    // If Seasons is not active AND this stage had previously had Seasons active...
                    if (!seasonActive && plainsCheck)
                    {
                        // This checks for whether the last stage was an alt palette before Seasons was changed. If it was, the same step as above is taken.
                        if (plainsVariant != 1) MonsterLists.PlainsReset();
                        // TryApplyChangesNow() is applied here due to none of the switch statements applying it if Seasons is off.
                        DirectorAPI.Helpers.TryApplyChangesNow();
                    }
                    // This sets the stage's internal bool to match whether Seasons is active.
                    plainsCheck = seasonActive;
                    // Using a switch statement based on the random number above, giving all variants a 33.3% chance of appearing:
                    switch (counter)
                    {
                        case 1:
                            if (plainsVariant != 1) DirectorAPI.Helpers.TryApplyChangesNow(); // This checks for whether the last use of this stage was not the vanilla variant; if so, it applies the changes that were set up in PlainsReset().
                            break;
                        case 2:
                            PlainsSunset(fog, scenename); // Changes the stage to the Sunset palette. The scenename here is used due to this void being used by two different stages with different effects.
                            if (seasonActive && useList) MonsterLists.PlainsTwo(); // Checks for whether Seasons is active and whether the bool described above was passed. If both are true, the monsters on this stage are changed.
                            break;
                        case 3:
                            Overcast(fog, scenename); // Changes the stage to the Overcast palette.
                            if (seasonActive && useList) MonsterLists.PlainsThree(); // Same as PlainsTwo().
                            break;
                        default:
                            Debug.Log("Idk what you did but this ain't right"); // You shouldn't be able to reach this, but it acts the same as 1 alongside logging this in the console window.
                            if (useList) DirectorAPI.Helpers.TryApplyChangesNow();
                            break;
                    }
                    // Finally, the active palette is stored for the next time this stage is loaded.
                    plainsVariant = counter;
                }
                if (scenename == "blackbeach" || scenename == "blackbeach2")
                {
                    // This works more or less the same way as Titanic Plains, but introduces two new factors. The first is a differing switch statement depending on whether or not you've reached a certain stage count (3+ in this case).
                    int counter = 1;
                    if (Run.instance.stageClearCount >= 2 || !seasonActive || !StageRestriction.Value) counter = UnityEngine.Random.Range(1, 4);
                    // This omits the third variant unless the above condition is passed, giving a 50/50 chance for the other two.
                    else counter = UnityEngine.Random.Range(1, 3);
                    networkCounter = counter;
                    //new AestheticSync(counter).Send(NetworkDestination.Clients);
                    //counter = networkCounter;
                    bool useList = roostVariant != counter || !roostCheck;
                    if (seasonActive && roostVariant != 1 && roostVariant != counter) MonsterLists.RoostReset();
                    if (!seasonActive && roostCheck)
                    {
                        if (roostVariant != 1) MonsterLists.RoostReset();
                        DirectorAPI.Helpers.TryApplyChangesNow();
                    }
                    roostCheck = seasonActive;
                    switch (counter)
                    {
                        case 1:
                            if (useList) DirectorAPI.Helpers.TryApplyChangesNow();
                            break;
                        case 2:
                            NightEvening(fog);
                            if (seasonActive && useList) MonsterLists.RoostTwo();
                            break;
                        case 3:
                            // This also introduces ColorGrading, which is a filter applied on the screen itself. Only the RedMist() variants and AbyssalShadow() use this.
                            ColorGrading cgrade = volume.profile.AddSettings<ColorGrading>();
                            RoostMist(fog, cgrade);
                            if (seasonActive && useList) MonsterLists.RoostThree();
                            break;
                        default:
                            Debug.Log("Idk what you did but this ain't right");
                            break;
                    }
                    roostVariant = counter;
                }
                if (scenename == "foggyswamp")
                {
                    int counter = 1;
                    if (Run.instance.stageClearCount >= 4 || !seasonActive || !StageRestriction.Value) counter = UnityEngine.Random.Range(1, 4);
                    else counter = UnityEngine.Random.Range(1, 3);
                    networkCounter = counter;
                    //new AestheticSync(counter).Send(NetworkDestination.Clients);
                    //counter = networkCounter;
                    bool useList = wetlandVariant != counter || !wetlandCheck;
                    if (seasonActive && wetlandVariant != 1 && wetlandVariant != counter) MonsterLists.WetlandReset();
                    if (!seasonActive && wetlandCheck)
                    {
                        if (wetlandVariant != 1) MonsterLists.WetlandReset();
                        DirectorAPI.Helpers.TryApplyChangesNow();
                    }
                    wetlandCheck = seasonActive;
                    switch (counter)
                    {
                        case 1:
                            if (useList) DirectorAPI.Helpers.TryApplyChangesNow();
                            break;
                        case 2:
                            PlainsSunset(fog, scenename);
                            if (seasonActive && useList) MonsterLists.WetlandTwo();
                            break;
                        case 3:
                            PinkAspect(fog, scenename);
                            if (seasonActive && useList) MonsterLists.WetlandThree();
                            break;
                        default:
                            Debug.Log("Idk what you did but this ain't right");
                            break;
                    }
                }
                if (scenename == "goolake")
                {
                    int counter = UnityEngine.Random.Range(1, 4);
                    bool useList = aqueductVariant != counter || !aqueductCheck;
                    if (seasonActive && aqueductVariant != 1 && aqueductVariant != counter) MonsterLists.AqueductReset();
                    if (!seasonActive && aqueductCheck)
                    {
                        if (aqueductVariant != 1) MonsterLists.AqueductReset();
                        DirectorAPI.Helpers.TryApplyChangesNow();
                    }
                    aqueductCheck = seasonActive;
                    switch (counter)
                    {
                        case 1:
                            if (useList) DirectorAPI.Helpers.TryApplyChangesNow();
                            break;
                        case 2:
                            NightDark(fog);
                            if (seasonActive && useList) MonsterLists.AqueductTwo();
                            break;
                        case 3:
                            Overcast(fog, scenename);
                            if (seasonActive && useList) MonsterLists.AqueductThree();
                            break;
                        default:
                            Debug.Log("Idk what you did but this ain't right");
                            break;
                    }
                    aqueductVariant = counter;
                }
                if (scenename == "frozenwall")
                {
                    int counter = 1;
                    if (Run.instance.stageClearCount >= 3 || !seasonActive || !StageRestriction.Value) counter = UnityEngine.Random.Range(1, 4);
                    else counter = UnityEngine.Random.Range(1, 3);
                    //new AestheticSync(counter).Send(NetworkDestination.Clients);
                    //counter = networkCounter;
                    bool useList = deltaVariant != counter || !deltaCheck;
                    if (seasonActive && deltaVariant != 1 && deltaVariant != counter) MonsterLists.DeltaReset();
                    if (!seasonActive && deltaCheck)
                    {
                        if (deltaVariant != 1) MonsterLists.DeltaReset();
                        DirectorAPI.Helpers.TryApplyChangesNow();
                    }
                    deltaCheck = seasonActive;
                    switch (counter)
                    {
                        case 1:
                            if (useList) DirectorAPI.Helpers.TryApplyChangesNow();
                            break;
                        case 2:
                            NightEvening(fog);
                            if (seasonActive && useList) MonsterLists.DeltaTwo();
                            break;
                        case 3:
                            Misty(fog);
                            if (seasonActive && useList) MonsterLists.DeltaThree();
                            break;
                        default:
                            Debug.Log("Idk what you did but this ain't right");
                            break;
                    }
                    deltaVariant = counter;
                }
                if (scenename == "wispgraveyard")
                {
                    int counter = 1;
                    if (Run.instance.stageClearCount >= 3 || !seasonActive || !StageRestriction.Value) counter = UnityEngine.Random.Range(1, 4);
                    else counter = UnityEngine.Random.Range(1, 3);
                    //new AestheticSync(counter).Send(NetworkDestination.Clients);
                    //counter = networkCounter;
                    bool useList = acresVariant != counter || !acresCheck;
                    if (seasonActive && acresVariant != 1 && acresVariant != counter) MonsterLists.AcresReset();
                    if (!seasonActive && acresCheck)
                    {
                        if (acresVariant != 1) MonsterLists.AcresReset();
                        DirectorAPI.Helpers.TryApplyChangesNow();
                    }
                    acresCheck = seasonActive;
                    switch (counter)
                    {
                        case 1:
                            if (useList) DirectorAPI.Helpers.TryApplyChangesNow();
                            break;
                        case 2:
                            AcresSunset(fog);
                            if (seasonActive && useList) MonsterLists.AcresTwo();
                            break;
                        case 3:
                            AcresSky(fog);
                            if (seasonActive && useList) MonsterLists.AcresThree();
                            break;
                        default:
                            Debug.Log("Idk what you did but this ain't right");
                            break;
                    }
                    acresVariant = counter;
                }
                if (scenename == "dampcavesimple")
                {
                    // This one works a bit differently due to only having two variants.
                    int counter = UnityEngine.Random.Range(1, 3);
                    //new AestheticSync(counter).Send(NetworkDestination.Clients);
                    //counter = networkCounter;
                    // Only one if statement is needed here due to how the options here work.
                    if (!seasonActive && depthsCheck && depthsVariant == 2) MonsterLists.DepthsOne();
                    depthsCheck = seasonActive;
                    switch (counter)
                    {
                        case 1:
                            // DepthsOne() is used instead of a Reset() void; it's more or less the same thing aside from being built more like the variants.
                            if (seasonActive && depthsVariant == 2) MonsterLists.DepthsOne();
                            break;
                        case 2:
                            ColorGrading cgrade = volume.profile.AddSettings<ColorGrading>();
                            AbyssalShadow(fog, cgrade);
                            if (seasonActive && depthsVariant == 1) MonsterLists.DepthsTwo();
                            break;
                        default:
                            Debug.Log("Idk what you did but this ain't right");
                            break;
                    }
                    depthsVariant = counter;
                }
                if (scenename == "shipgraveyard")
                {
                    int counter = UnityEngine.Random.Range(1, 3);
                    //new AestheticSync(counter).Send(NetworkDestination.Clients);
                    //counter = networkCounter;
                    if (!seasonActive && sirenCheck && sirenVariant == 2) MonsterLists.SirenOne();
                    sirenCheck = seasonActive;
                    switch (counter)
                    {
                        case 1:
                            if (seasonActive && sirenVariant == 2) MonsterLists.SirenOne();
                            break;
                        case 2:
                            NightEvening(fog);
                            if (seasonActive && sirenVariant == 1) MonsterLists.SirenTwo();
                            break;
                        default:
                            Debug.Log("Idk what you did but this ain't right");
                            break;
                    }
                    sirenVariant = counter;
                }
                if (scenename == "rootjungle")
                {
                    int counter = UnityEngine.Random.Range(1, 3);
                    //new AestheticSync(counter).Send(NetworkDestination.Clients);
                    //counter = networkCounter;
                    // If Seasons was activated and Grove hadn't had it previously, this line adds Shrine of the Woods to both variants.
                    // NOTE: THIS CODE HAS BEEN DISABLED TEMPORARILY DUE TO ISSUES WITH THE SPAWN CARD
                    // if (seasonActive && !groveCheck) DirectorAPI.Helpers.AddNewInteractableToStage(MonsterLists.woodShrine, DirectorAPI.InteractableCategory.Shrines, DirectorAPI.Stage.SunderedGrove);
                    // Due to the above, this if statement needed to be expanded to two lines.
                    if (!seasonActive && groveCheck)
                    {
                        // Removing the Shrine of the Woods...
                        // DirectorAPI.Helpers.RemoveExistingInteractableFromStage(DirectorAPI.Helpers.InteractableNames.WoodsShrine, DirectorAPI.Stage.SunderedGrove);
                        // And applying the same change as above if the alt palette was the last one in memory.
                        if (groveVariant == 2) MonsterLists.GroveOne();
                    }
                    groveCheck = seasonActive;
                    switch (counter)
                    {
                        case 1:
                            if (seasonActive && groveVariant == 2) MonsterLists.GroveOne();
                            break;
                        case 2:
                            ColorGrading cgrade = volume.profile.AddSettings<ColorGrading>();
                            RedMist(fog, cgrade);
                            if (seasonActive && groveVariant == 1) MonsterLists.GroveTwo();
                            break;
                        default:
                            Debug.Log("Idk what you did but this ain't right");
                            break;
                    }
                    groveVariant = counter;
                }
                if (scenename == "skymeadow")
                {
                    int counter = 1;
                    if (Run.instance.stageClearCount >= 5 || !seasonActive || !StageRestriction.Value) counter = UnityEngine.Random.Range(1, 4);
                    else counter = UnityEngine.Random.Range(1, 3);
                    //new AestheticSync(counter).Send(NetworkDestination.Clients);
                    //counter = networkCounter;
                    bool useList = meadowVariant != counter || !meadowCheck;
                    if (seasonActive && meadowVariant != 1 && meadowVariant != counter) MonsterLists.MeadowReset();
                    if (!seasonActive && meadowCheck)
                    {
                        if (meadowVariant != 1) MonsterLists.MeadowReset();
                        DirectorAPI.Helpers.TryApplyChangesNow();
                    }
                    meadowCheck = seasonActive;
                    switch (counter)
                    {
                        case 1:
                            DirectorAPI.Helpers.TryApplyChangesNow();
                            break;
                        case 2:
                            NightMeadow(fog);
                            if (seasonActive && useList) MonsterLists.MeadowTwo();
                            break;
                        case 3:
                            PinkAspect(fog, scenename);
                            if (seasonActive && useList) MonsterLists.MeadowThree();
                            break;
                        default:
                            Debug.Log("Idk what you did but this ain't right");
                            break;
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
                fog.fogColorEnd.value = new Color(0.2f, 0.21f, 0.27f, 0.75f);
                fog.skyboxStrength.value = 0f;
            }
            else Debug.Log("Post process volume could not be found.");
        }
        private static void NightMeadow(RampFog fog)
        {
            // Fog makes up 95% of the graphical changes this mod makes. The three Colors below determine how the stage appears at the following:
            fog.fogColorStart.value = new Color(0.13f, 0.065f, 0.19f, 0.4f); // At close range, roughly 30-40m or so?
            fog.fogColorMid.value = new Color(0.19f, 0.11f, 0.28f, 0.775f); // Past that point, up to quite a ways in the distance
            fog.fogColorEnd.value = new Color(0.25f, 0.155f, 0.36f, 0.95f); // How stuff looks at a very far range
            // This determines how strongly the skybox's appearance shines through. A very low value, like what's used here, means more of the End fog color bleeds through.
            fog.skyboxStrength.value = 0.08f;
        }
        private static void NightDark(RampFog fog)
        {
            fog.fogColorStart.value = new Color(0.15f, 0.09f, 0.05f, 0.4f);
            fog.fogColorMid.value = new Color(0.18f, 0.105f, 0.07f, 0.775f);
            fog.fogColorEnd.value = new Color(0.21f, 0.12f, 0.08f, 0.95f);
            fog.skyboxStrength.value = 0.06f;
        }
        private static void NightEvening(RampFog fog)
        {
            fog.fogColorStart.value = new Color(0.1f, 0.14f, 0.2f, 0.55f);
            fog.fogColorMid.value = new Color(0.16f, 0.22f, 0.3f, 0.675f);
            fog.fogColorEnd.value = new Color(0.22f, 0.3f, 0.4f, 0.8f);
            fog.skyboxStrength.value = 0.1f;
        }
        private static void Overcast(RampFog fog, String scenename)
        {
            fog.fogColorStart.value = new Color(0.15f, 0.21f, 0.3f, 0.55f);
            fog.fogColorMid.value = new Color(0.19f, 0.28f, 0.4f, 0.75f);
            fog.fogColorEnd.value = new Color(0.24f, 0.35f, 0.5f, 0.95f);
            if (scenename == "goolake")
            {
                fog.fogColorEnd.value.b = 0.45f;
                fog.fogColorEnd.value.r = 0.29f;
                fog.fogColorMid.value.b = 0.365f;
                fog.fogColorMid.value.r = 0.22f;
            }
            fog.skyboxStrength.value = 0.125f;
        }
        private static void Misty(RampFog fog)
        {
            fog.fogColorStart.value = new Color(0.3f, 0.3f, 0.325f, 0.2f);
            fog.fogColorMid.value = new Color(0.49f, 0.51f, 0.55f, 0.6f);
            fog.fogColorEnd.value = new Color(0.575f, 0.6f, 0.67f, 0.9f);
            fog.skyboxStrength.value = 0.1f;
        }
        private static void RedMist(RampFog fog, ColorGrading cgrade)
        {
            cgrade.colorFilter.value = new Color(0.16f, 0.08f, 0.075f);
            fog.fogColorStart.value = new Color(0.12f, 0.03f, 0.01f, 0.4f);
            fog.fogColorMid.value = new Color(0.24f, 0.06f, 0.02f, 0.725f);
            fog.fogColorEnd.value = new Color(0.37f, 0.1f, 0.04f, 0.85f);
            fog.skyboxStrength.value = 0f;
            Chat.AddMessage("<color=#C3E8E8>A bloody mist fills the air...</color>");
        }
        private static void RoostMist(RampFog fog, ColorGrading cgrade)
        {
            cgrade.colorFilter.value = new Color(0.16f, 0.08f, 0.075f);
            fog.fogColorStart.value = new Color(0.09f, 0.03f, 0.13f, 0.4f);
            fog.fogColorMid.value = new Color(0.18f, 0.06f, 0.18f, 0.725f);
            fog.fogColorEnd.value = new Color(0.37f, 0.1f, 0.25f, 0.85f);
            fog.skyboxStrength.value = 0.03f;
            Chat.AddMessage("<color=#C3E8E8>A bloody mist fills the air...</color>");
        }
        private static void AcresSky(RampFog fog)
        {
            fog.fogColorStart.value = new Color(0.3f, 0.28f, 0.26f, 0.2f);
            fog.fogColorMid.value = new Color(0.49f, 0.43f, 0.41f, 0.65f);
            fog.fogColorEnd.value = new Color(0.533f, 0.627f, 0.588f, 1f);
            fog.skyboxStrength.value = 0.45f;
            Chat.AddMessage("<color=#C3E8E8>An oppressive feeling of nothingness permeates the skies above...</color>");
        }
        private static void PinkAspect(RampFog fog, String scenename)
        {
            fog.fogColorStart.value = new Color(0.3f, 0.2f, 0.325f, 0.25f);
            fog.fogColorMid.value = new Color(0.42f, 0.38f, 0.49f, 0.5f);
            fog.fogColorEnd.value = new Color(0.55f, 0.47f, 0.61f, 0.75f);
            fog.skyboxStrength.value = 0.2f;
            if (scenename == "skymeadow")
            {
                fog.skyboxStrength.value = 0.05f;
                fog.fogColorMid.value.r = 0.36f;
                fog.fogColorMid.value.a = 0.35f;
                fog.fogColorEnd.value.r = 0.44f;
                fog.fogColorEnd.value.a = 0.45f;
                Chat.AddMessage("<color=#C3E8E8>An oppressive feeling of nothingness permeates the skies above...</color>");
            }
            if (scenename == "foggyswamp")
            {
                Chat.AddMessage("<color=#C3E8E8>An ethereal mist lies over the swamp... </color>");
            }
        }
        private static void AcresSunset(RampFog fog)
        {
            fog.fogColorStart.value = new Color(0.076f, 0.039f, 0.033f, 0.1f);
            fog.fogColorMid.value = new Color(0.384f, 0.145f, 0.083f, 0.4f);
            fog.fogColorEnd.value = new Color(0.457f, 0.171f, 0.108f, 0.7f);
            fog.skyboxStrength.value = 0.126f;
            fog.fogZero.value = -0.049f;
            fog.fogOne.value = 0.211f;
        }
        private static void AbyssalShadow(RampFog fog, ColorGrading cgrade)
        {
            cgrade.colorFilter.value = new Color(0.08f, 0.16f, 0.075f);
            fog.fogColorStart.value = new Color(0.1f, 0.1f, 0.1f, 0.4f);
            fog.fogColorMid.value = new Color(0.1875f, 0.2f, 0.14f, 0.8f);
            fog.fogColorEnd.value = new Color(0.28f, 0.3f, 0.19f, 1f);
        }
        private static void PlainsSunset(RampFog fog, String scenename)
        {
            if (scenename == "foggyswamp")
            {
                fog.fogColorStart.value = new Color(0.076f, 0.056f, 0.054f, 0.1f);
                fog.fogColorMid.value = new Color(0.324f, 0.194f, 0.103f, 0.325f);
                fog.fogColorEnd.value = new Color(0.397f, 0.282f, 0.138f, 0.45f);
                fog.skyboxStrength.value = 0.03f;
            }
            else
            {
                fog.fogColorStart.value = new Color(0.076f, 0.055f, 0.044f, 0.1f);
                fog.fogColorMid.value = new Color(0.384f, 0.206f, 0.093f, 0.4f);
                fog.fogColorEnd.value = new Color(0.457f, 0.244f, 0.108f, 0.7f);
                fog.skyboxStrength.value = 0.146f;
            }
            fog.fogZero.value = -0.049f;
            fog.fogOne.value = 0.211f;
        }
        // I have no idea if any of this is correct...
        /*public class AestheticSync : INetMessage
        {
            int variant;
            public void Deserialize(NetworkReader reader)
            {
                variant = reader.ReadInt32();
            }
            public void OnReceived()
            {
                if (NetworkServer.active) return;
                Debug.Log("Networking attempted: setting to "+variant);
                networkCounter = variant;
            }
            public void Serialize(NetworkWriter writer)
            {
                networkCounter = variant;
                Debug.Log("Attempting networking: sending out "+networkCounter);
                writer.Write(variant);
            }
            public AestheticSync()
            {
            }
            public AestheticSync(int num)
            {
                variant = num;
            }
        }*/
        public static int networkCounter;
        public static PostProcessVolume volume;
        public static int plainsVariant = 1;
        public static int roostVariant = 1;
        public static int wetlandVariant = 1;
        public static int aqueductVariant = 1;
        public static int deltaVariant = 1;
        public static int acresVariant = 1;
        public static int depthsVariant = 1;
        public static int sirenVariant = 1;
        public static int groveVariant = 1;
        public static int meadowVariant = 1;
        public static int currentVariant;
        public static AssetBundle artifactIcon;
        public static ArtifactDef seasons;
        public static bool seasonActive;
        public static bool groveCheck = false;
        public static bool plainsCheck = false;
        public static bool roostCheck = false;
        public static bool wetlandCheck = false;
        public static bool aqueductCheck = false;
        public static bool deltaCheck = false;
        public static bool acresCheck = false;
        public static bool depthsCheck = false;
        public static bool sirenCheck = false;
        public static bool meadowCheck = false;
        public static ConfigFile AesConfig { get; set; }
        public static ConfigEntry<bool> CommencementAlt { get; set; }
        public static ConfigEntry<bool> StageRestriction { get; set; }
        public static ConfigEntry<bool> BossConfig { get; set; }
        public static PostProcessProfile commencementVolume;
    }
}
