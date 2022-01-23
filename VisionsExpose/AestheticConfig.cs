using System;
using System.Collections.Generic;
using System.Text;
using BepInEx.Configuration;
using BepInEx;
using RoR2;
using InLobbyConfig.Fields;

namespace StageAesthetic
{
    public class AestheticConfig : StageAesthetic.Aesthetic
    {
        public static void SetConfig()
        {
            AesConfig = new ConfigFile(Paths.ConfigPath + "\\StageAesthetic.cfg", true);
            VanillaPlains = AesConfig.Bind<bool>("A. Titanic Plains", "Enable vanilla?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            SunsetPlains = AesConfig.Bind<bool>("A. Titanic Plains", "Enable Sunset Plains?", true, "Gives the stage a darker, red-orange appearance along with heavily angled lighting.");
            RainyPlains = AesConfig.Bind<bool>("A. Titanic Plains", "Enable Rainy Plains?", true, "Gives the appearance of a light grey fog throughout the stage and adds rain.");
            NightPlains = AesConfig.Bind<bool>("A. Titanic Plains", "Enable Night Plains?", true, "Requested by HIFU. Gives the stage a darker, blue appearance.");
            PlainsChanges = AesConfig.Bind<bool>("A. Titanic Plains", "Alter vanilla Titanic Plains?", true, "Slightly increases lighting intensity, and forces it to line up with the stage's sun.");
            PlainsBridge = AesConfig.Bind<int>("A. Titanic Plains", "Bridge % Chance", 40, "How often the unused bridge in Titanic Plains should appear. Setting to 0 also disables the code to import it, in case you need it for something else.");
            VanillaRoost = AesConfig.Bind<bool>("B. Distant Roost", "Enable vanilla?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            NightRoost = AesConfig.Bind<bool>("B. Distant Roost", "Enable Night Roost?", true, "Gives the stage a much darker appearance with an indigo color scheme, in addition to increasing the intensity and range of light effects in the stage. Also enables unused fog in default Distant Roost.");
            SunnyRoost = AesConfig.Bind<bool>("B. Distant Roost", "Enable Sunny Roost?", true, "Greatly alters lighting alongside a less blue fog to give the appearance of heavy sunlight. Also enables unused fog and disables rain in default Distant Roost.");
            FoggyRoost = AesConfig.Bind<bool>("B. Distant Roost", "Enable Storm Roost?", true, "Requested by HIFU. Darkens the fog and lighting while also adding a much stronger rain effect.");
            RoostChanges = AesConfig.Bind<bool>("B. Distant Roost", "Add rain in alt version?", true, "Enables rain in alt Distant Roost to match the default version.");
            VanillaWetland = AesConfig.Bind<bool>("C. Wetland Aspect", "Enable vanilla?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            SunsetWetland = AesConfig.Bind<bool>("C. Wetland Aspect", "Enable Sunset Aspect?", true, "Gives the stage a marigold color scheme to imitate sunsets at swamps and marshes.");
            SkyWetland = AesConfig.Bind<bool>("C. Wetland Aspect", "Enable Sky Aspect?", true, "Gives the stage a pink appearance resembling that of Sky Meadow.");
            EveningWetland = AesConfig.Bind<bool>("C. Wetland Aspect", "Enable Dark Aspect?", true, "Gives the stage a greener, darker appearance with weaker lighting.");
            VanillaAqueduct = AesConfig.Bind<bool>("D. Abandoned Aqueduct", "Enable vanilla?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            NightAqueduct = AesConfig.Bind<bool>("D. Abandoned Aqueduct", "Enable Night Aqueduct?", true, "Gives the stage a much darker appearance, using a murky brown instead of the usual peach coloration, and replaces the warning banners with unused statues.");
            RainyAqueduct = AesConfig.Bind<bool>("D. Abandoned Aqueduct", "Enable Rainy Aqueduct?", true, "Gives the stage a blue-grey coloration and adds rain.");
            warning2 = "if you hear voices inside your head, ";
            MistyAqueduct = AesConfig.Bind<bool>("D. Abandoned Aqueduct", "Enable Haunted Aqueduct?", true, "Gives the stage a dark red coloration and adds heavy rain.");
            AqueductChanges = AesConfig.Bind<bool>("D. Abandoned Aqueduct", "Alter vanilla Abandoned Aqueduct?", true, "Makes the sun a slightly more intense yellow-orange, and changes its angle.");
            VanillaDelta = AesConfig.Bind<bool>("E. Rallypoint Delta", "Enable vanilla?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            NightDelta = AesConfig.Bind<bool>("E. Rallypoint Delta", "Enable Night Delta?", true, "Heavily darkens the fog while also making the skybox stronger, giving an appearance of post-sunset/pre-sunrise.");
            FoggyDelta = AesConfig.Bind<bool>("E. Rallypoint Delta", "Enable Foggy Delta?", true, "Increases the fog intensity while making it greyer, and also raises the stage's water level (no gameplay effects).");
            PurpleDelta = AesConfig.Bind<bool>("E. Rallypoint Delta", "Enable Emerald Delta?", true, "Makes the stage green. Doesn't really do much else besides that, since Rallypoint doesn't give me much freedom.");
            VanillaAcres = AesConfig.Bind<bool>("F. Scorched Acres", "Enable vanilla?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            SunsetAcres = AesConfig.Bind<bool>("F. Scorched Acres", "Enable Sunset Acres?", true, "Gives the stage a deeper magenta color scheme while also altering sunlight to match the vanilla sun position.");
            NightAcres = AesConfig.Bind<bool>("F. Scorched Acres", "Enable Night Acres?", true, "Replaces the sun with a starry sky taken from unused assets, gives the stage a dark blue appearance, and adds rain.");
            BlueAcres = AesConfig.Bind<bool>("F. Scorched Acres", "Enable Emerald Acres?", true, "Replaces the sun with a starry sky taken from unused assets, alongside a sea green color scheme and a blue ember effect.");
            AcresChanges = AesConfig.Bind<bool>("F. Scorched Acres", "Alter vanilla Scorched Acres?", true, "Greatly increases the sunlight intensity, and alters the light angle and sun position towards a different corner of the map.");
            VanillaDepths = AesConfig.Bind<bool>("G. Abyssal Depths", "Enable vanilla", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            DarkDepths = AesConfig.Bind<bool>("G. Abyssal Depths", "Enable Azure Depths?", true, "Messes with ColorGrading to give the stage a more blue appearance. Unfortunately not much of a success, but hopefully it's interesting enough.");
            BlueDepths = AesConfig.Bind<bool>("G. Abyssal Depths", "Enable Hive Cluster Depths?", true, "A replacement for the older Dark Depths that resembles the stage from RoR1.");
            SkyDepths = AesConfig.Bind<bool>("G. Abyssal Depths", "Enable Sky Meadow Depths?", true, "Gives the stage a pink appearance resembling that of Sky Meadow.");
            DepthsChanges = AesConfig.Bind<bool>("G. Abyssal Depths", "Alter vanilla Abyssal Depths?", true, "Greatly increases the sunlight intensity, and alters the light angle.");
            VanillaGrove = AesConfig.Bind<bool>("H. Sundered Grove", "Enable vanilla?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            GreenGrove = AesConfig.Bind<bool>("H. Sundered Grove", "Enable Olive Grove?", true, "Gives the stage a darker olive-green color scheme while also increasing the lighting slightly.");
            warning2 += "start listening before ";
            SunnyGrove = AesConfig.Bind<bool>("H. Sundered Grove", "Enable Sunny Grove?", true, "Heavily increases lighting while also brightening the color scheme slightly. May be on the chopping block if I update this mod, as it's not that different from vanilla.");
            HannibalGrove = AesConfig.Bind<bool>("H. Sundered Grove", "Enable Overcast Grove?", true, "Makes the stage greyish while replacing the rain with a more consistent effect.");
            VanillaSiren = AesConfig.Bind<bool>("I. Sirens Call", "Enable vanilla?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            NightSiren = AesConfig.Bind<bool>("I. Sirens Call", "Enable Night Call?", true, "Makes the stage a darker blue -> black while also making the skybox stronger, giving a nighttime appearance.");
            SunnySiren = AesConfig.Bind<bool>("I. Sirens Call", "Enable Sirens Sun?", true, "Gives the stage a lighter blue color scheme, while also heavily increasing the sunlight's intensity and shifting it to a light orange color.");
            MistySiren = AesConfig.Bind<bool>("I. Sirens Call", "Enable Sirens Storm?", true, "Gives the stage a thicker, grey fog while making the wind and rain much stronger.");
            VanillaMeadow = AesConfig.Bind<bool>("J. Sky Meadow", "Enable vanilla?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            NightMeadow = AesConfig.Bind<bool>("J. Sky Meadow", "Enable Night Meadow?", true, "Gives the stage a darker blue color scheme and enables an unused star effect similar to that of Void Fields.");
            StormyMeadow = AesConfig.Bind<bool>("J. Sky Meadow", "Enable Stormy Meadow?", true, "Gives the stage a greyer color scheme and enables rain.");
            CrimsonMeadow = AesConfig.Bind<bool>("J. Sky Meadow", "Enable Abyssal Meadow?", true, "Requested by Heyimnoob. Gives the stage a very red appearance, with significantly stronger fog.");
            MeadowChanges = AesConfig.Bind<bool>("J. Sky Meadow", "Alter vanilla Sky Meadow?", true, "Makes the sun a slightly more intense yellow-orange.");
            CommencementAlt = AesConfig.Bind<bool>("K. Extras", "Commencement alt?", true, "Enables a darker, bluer color scheme on Commencement. Now additionally includes lighting changes and an unused sun object to match.");
            //GildedAlt = AesConfig.Bind<bool>("K. Extras", "Gilded Coast rings?", true, "Enables the unused falling ring effect in Gilded Coast. May take a while to appear...");
            TitleScene = AesConfig.Bind<bool>("K. Extras", "Alter title screen?", true, "Enables the title screen changes.");
            warning2 += "it's too late";
            WeatherEffects = AesConfig.Bind<bool>("K. Extras", "Import weather effects?", true, "Hooks into SceneCamera to import rain and ember effects into stages that normally don't have them. Disabling this is recommended if performance is an issue or if playing Starstorm 2, as it overlaps with the latter's weather.");
            var configEntry = ConfigFieldUtilities.CreateFromBepInExConfigFile(AesConfig, "StageAesthetic Config");
            InLobbyConfig.ModConfigCatalog.Add(configEntry);
        }
        public static String warning2;
        public static void ApplyConfig(Run obj)
        {
            if (VanillaPlains.Value) plainsList.Add("vanilla");
            if (SunsetPlains.Value) plainsList.Add("sunset");
            if (RainyPlains.Value) plainsList.Add("rain");
            if (NightPlains.Value) plainsList.Add("night");
            if (plainsList.Count == 0)
            {
                AesLog.LogWarning("Titanic Plains list empty - adding vanilla...");
                plainsList.Add("vanilla");
            }
            if (VanillaRoost.Value) roostList.Add("vanilla");
            if (NightRoost.Value) roostList.Add("night");
            if (SunnyRoost.Value) roostList.Add("sunny");
            if (FoggyRoost.Value) roostList.Add("foggy");
            if (roostList.Count == 0)
            {
                AesLog.LogWarning("Distant Roost list empty - adding vanilla...");
                roostList.Add("vanilla");
            }
            if (VanillaWetland.Value) wetlandList.Add("vanilla");
            if (SunsetWetland.Value) wetlandList.Add("sunset");
            if (SkyWetland.Value) wetlandList.Add("sky");
            if (EveningWetland.Value) wetlandList.Add("dark"); 
            if (wetlandList.Count == 0)
            {
                AesLog.LogWarning("Wetland Aspect list empty - adding vanilla...");
                wetlandList.Add("vanilla");
            }
            if (VanillaAqueduct.Value) aqueductList.Add("vanilla");
            if (NightAqueduct.Value) aqueductList.Add("night");
            if (RainyAqueduct.Value) aqueductList.Add("rain");
            if (MistyAqueduct.Value) aqueductList.Add("nightrain");
            if (aqueductList.Count == 0)
            {
                AesLog.LogWarning("Abandoned Aqueduct list empty - adding vanilla...");
                aqueductList.Add("vanilla");
            }
            if (VanillaDelta.Value) deltaList.Add("vanilla");
            if (NightDelta.Value) deltaList.Add("night");
            if (FoggyDelta.Value) deltaList.Add("foggy");
            if (PurpleDelta.Value) deltaList.Add("green");
            if (deltaList.Count == 0)
            {
                AesLog.LogWarning("Rallypoint Delta list empty - adding vanilla...");
                deltaList.Add("vanilla");
            }
            if (VanillaAcres.Value) acresList.Add("vanilla");
            if (SunsetAcres.Value) acresList.Add("sunset");
            if (NightAcres.Value) acresList.Add("night");
            if (BlueAcres.Value) acresList.Add("nothing");
            if (acresList.Count == 0)
            {
                AesLog.LogWarning("Scorched Acres list empty - adding vanilla...");
                acresList.Add("vanilla");
            }
            if (VanillaDepths.Value) depthsList.Add("vanilla");
            if (DarkDepths.Value) depthsList.Add("gold");
            if (BlueDepths.Value) depthsList.Add("hive");
            if (SkyDepths.Value) depthsList.Add("sky");
            if (depthsList.Count == 0)
            {
                AesLog.LogWarning("Abyssal Depths list empty - adding vanilla...");
                depthsList.Add("vanilla");
            }
            if (VanillaGrove.Value) groveList.Add("vanilla");
            if (GreenGrove.Value) groveList.Add("green");
            if (SunnyGrove.Value) groveList.Add("sunny");
            if (HannibalGrove.Value) groveList.Add("storm");
            if (groveList.Count == 0)
            {
                AesLog.LogWarning("Sundered Grove list empty - adding vanilla...");
                groveList.Add("vanilla");
            }
            if (VanillaSiren.Value) sirenList.Add("vanilla");
            if (NightSiren.Value) sirenList.Add("night");
            if (SunnySiren.Value) sirenList.Add("sunny");
            if (MistySiren.Value) sirenList.Add("storm");
            if (sirenList.Count == 0)
            {
                AesLog.LogWarning("Siren's Call list empty - adding vanilla...");
                sirenList.Add("vanilla");
            }
            if (VanillaMeadow.Value) meadowList.Add("vanilla");
            if (NightMeadow.Value) meadowList.Add("night");
            if (StormyMeadow.Value) meadowList.Add("storm");
            if (CrimsonMeadow.Value) meadowList.Add("abyss");
            if (meadowList.Count == 0)
            {
                AesLog.LogWarning("Sky Meadow list empty - adding vanilla...");
                meadowList.Add("vanilla");
            }
        }
    }
}
