using System;
using System.Collections.Generic;
using System.Text;
using BepInEx.Configuration;
using BepInEx;

namespace StageAesthetic
{
    public class AestheticConfig : StageAesthetic.Aesthetic
    {
        public static void SetConfig()
        {
            AesConfig = new ConfigFile(Paths.ConfigPath + "\\StageAesthetic.cfg", true);
            VanillaPlains = AesConfig.Bind<bool>("A. Titanic Plains", "Enable vanilla appearance?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            PlainsChanges = AesConfig.Bind<bool>("A. Titanic Plains", "Alter vanilla Titanic Plains?", true, "Slightly increases lighting intensity, and forces it to line up with the stage's sun.");
            SunsetPlains = AesConfig.Bind<bool>("A. Titanic Plains", "Enable Sunset Plains?", true, "Gives the stage a darker, red-orange appearance along with heavily angled lighting.");
            RainyPlains = AesConfig.Bind<bool>("A. Titanic Plains", "Enable Rainy Plains?", true, "Gives the appearance of a light grey fog throughout the stage and adds rain.");
            VanillaRoost = AesConfig.Bind<bool>("B. Distant Roost", "Enable vanilla appearance?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            NightRoost = AesConfig.Bind<bool>("B. Distant Roost", "Enable Night Roost?", true, "Gives the stage a much darker appearance with an indigo color scheme, in addition to increasing the intensity and range of light effects in the stage. Also enables unused fog in default Distant Roost.");
            SunnyRoost = AesConfig.Bind<bool>("B. Distant Roost", "Enable Sunny Roost?", true, "Greatly alters lighting alongside a less blue fog to give the appearance of heavy sunlight. Also enables unused fog in default Distant Roost.");
            VanillaWetland = AesConfig.Bind<bool>("C. Wetland Aspect", "Enable vanilla appearance?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            SunsetWetland = AesConfig.Bind<bool>("C. Wetland Aspect", "Enable Sunset Aspect?", true, "Gives the stage a marigold color scheme to imitate sunsets at swamps and marshes.");
            SkyWetland = AesConfig.Bind<bool>("C. Wetland Aspect", "Enable Sky Aspect?", true, "Gives the stage a pink appearance resembling that of Sky Meadow.");
            VanillaAqueduct = AesConfig.Bind<bool>("D. Abandoned Aqueduct", "Enable vanilla appearance?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            AqueductChanges = AesConfig.Bind<bool>("D. Abandoned Aqueduct", "Alter vanilla Abandoned Aqueduct?", true, "Makes the sun a slightly more intense yellow-orange, and changes its angle.");
            NightAqueduct = AesConfig.Bind<bool>("D. Abandoned Aqueduct", "Enable Night Aqueduct?", true, "Gives the stage a much darker appearance, using a murky brown instead of the usual peach coloration, and replaces the warning banners with unused statues.");
            RainyAqueduct = AesConfig.Bind<bool>("D. Abandoned Aqueduct", "Enable Rainy Aqueduct?", true, "Gives the stage a blue-grey coloration and adds rain.");
            warning2 = "if you hear voices inside your head, ";
            VanillaDelta = AesConfig.Bind<bool>("E. Rallypoint Delta", "Enable vanilla appearance?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            NightDelta = AesConfig.Bind<bool>("E. Rallypoint Delta", "Enable Night Delta?", true, "Heavily darkens the fog while also making the skybox stronger, giving an appearance of post-sunset/pre-sunrise.");
            FoggyDelta = AesConfig.Bind<bool>("E. Rallypoint Delta", "Enable Foggy Delta?", true, "Increases the fog intensity while making it greyer, and also raises the stage's water level (no gameplay effects).");
            VanillaAcres = AesConfig.Bind<bool>("F. Scorched Acres", "Enable vanilla appearance?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            AcresChanges = AesConfig.Bind<bool>("F. Scorched Acres", "Alter vanilla Scorched Acres?", true, "Greatly increases the sunlight intensity, and alters the light angle and sun position towards a different corner of the map.");
            SunsetAcres = AesConfig.Bind<bool>("F. Scorched Acres", "Enable Sunset Acres?", true, "Gives the stage a deeper red-orange color scheme while also altering sunlight to match the vanilla sun position.");
            NightAcres = AesConfig.Bind<bool>("F. Scorched Acres", "Enable Night Acres?", true, "Removes the sun entirely, gives the stage a dark blue appearance, and adds rain.");
            VanillaDepths = AesConfig.Bind<bool>("G. Abyssal Depths", "Enable vanilla appearance?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            DepthsChanges = AesConfig.Bind<bool>("G. Abyssal Depths", "Alter vanilla Abyssal Depths?", true, "Greatly increases the sunlight intensity, and alters the light angle.");
            DarkDepths = AesConfig.Bind<bool>("G. Abyssal Depths", "Enable Dark Depths?", true, "Gives the stage a much darker color scheme, although still close to vanilla. Also increases the intensity and range of light effects in the stage.");
            SkyDepths = AesConfig.Bind<bool>("G. Abyssal Depths", "Enable Sky Depths?", true, "Gives the stage a pink appearance resembling that of Sky Meadow.");
            VanillaGrove = AesConfig.Bind<bool>("H. Sundered Grove", "Enable vanilla appearance?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            GreenGrove = AesConfig.Bind<bool>("H. Sundered Grove", "Enable Olive Grove?", true, "Gives the stage an olive-green color scheme while also increasing the lighting slightly.");
            warning2 += "start listening before ";
            SunnyGrove = AesConfig.Bind<bool>("H. Sundered Grove", "Enable Sunny Grove?", true, "Heavily increases lighting while also brightening the color scheme slightly. May be on the chopping block if I update this mod, as it's not that different from vanilla.");
            VanillaSiren = AesConfig.Bind<bool>("I. Sirens Call", "Enable vanilla appearance?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            NightSiren = AesConfig.Bind<bool>("I. Sirens Call", "Enable Night Call?", true, "Heavily darkens the fog while also making the skybox stronger.");
            SunnySiren = AesConfig.Bind<bool>("I. Sirens Call", "Enable Sunny Call?", true, "Gives the stage a lighter blue color scheme, while also heavily increasing the sunlight's intensity and shifting it to a light orange color.");
            VanillaMeadow = AesConfig.Bind<bool>("J. Sky Meadow", "Enable vanilla appearance?", true, "Disabling this will remove the vanilla stage appearance from randomization unless everything else is also disabled.");
            MeadowChanges = AesConfig.Bind<bool>("J. Sky Meadow", "Alter vanilla Sky Meadow?", true, "Makes the sun a slightly more intense yellow-orange.");
            NightMeadow = AesConfig.Bind<bool>("J. Sky Meadow", "Enable Night Meadow?", true, "Gives the stage a darker blue color scheme and enables an unused star effect similar to that of Void Fields.");
            StormyMeadow = AesConfig.Bind<bool>("J. Sky Meadow", "Enable Stormy Meadow?", true, "Gives the stage a greyer color scheme and enables rain.");
            CommencementAlt = AesConfig.Bind<bool>("K. Extras", "Commencement alt?", true, "Enables a darker, bluer color scheme on Commencement.");
            warning2 += "it's too late";
            WeatherEffects = AesConfig.Bind<bool>("K. Extras", "Import weather effects?", true, "Hooks into both the title screen and SceneCamera to import rain into other stages. Disabling this may help if performance is an issue.");
            if (VanillaPlains.Value) plainsList.Add("vanilla");
            if (SunsetPlains.Value) plainsList.Add("sunset");
            if (RainyPlains.Value) plainsList.Add("rain");
            if (plainsList.Count == 0)
            {
                AesLog.LogWarning("Titanic Plains list empty - adding vanilla...");
                plainsList.Add("vanilla");
            }
            if (VanillaRoost.Value) roostList.Add("vanilla");
            if (NightRoost.Value) roostList.Add("night");
            if (SunnyRoost.Value) roostList.Add("sunny");
            if (roostList.Count == 0)
            {
                AesLog.LogWarning("Distant Roost list empty - adding vanilla...");
                roostList.Add("vanilla");
            }
            if (VanillaWetland.Value) wetlandList.Add("vanilla");
            if (SunsetWetland.Value) wetlandList.Add("sunset");
            if (SkyWetland.Value) wetlandList.Add("sky");
            if (wetlandList.Count == 0)
            {
                AesLog.LogWarning("Wetland Aspect list empty - adding vanilla...");
                wetlandList.Add("vanilla");
            }
            if (VanillaAqueduct.Value) aqueductList.Add("vanilla");
            if (NightAqueduct.Value) aqueductList.Add("night");
            if (RainyAqueduct.Value) aqueductList.Add("rain");
            if (aqueductList.Count == 0)
            {
                AesLog.LogWarning("Abandoned Aqueduct list empty - adding vanilla...");
                aqueductList.Add("vanilla");
            }
            if (VanillaDelta.Value) deltaList.Add("vanilla");
            if (NightDelta.Value) deltaList.Add("night");
            if (FoggyDelta.Value) deltaList.Add("foggy");
            if (deltaList.Count == 0)
            {
                AesLog.LogWarning("Rallypoint Delta list empty - adding vanilla...");
                deltaList.Add("vanilla");
            }
            if (VanillaAcres.Value) acresList.Add("vanilla");
            if (SunsetAcres.Value) acresList.Add("sunset");
            if (NightAcres.Value) acresList.Add("night");
            if (acresList.Count == 0)
            {
                AesLog.LogWarning("Scorched Acres list empty - adding vanilla...");
                acresList.Add("vanilla");
            }
            if (VanillaDepths.Value) depthsList.Add("vanilla");
            if (DarkDepths.Value) depthsList.Add("dark");
            if (SkyDepths.Value) depthsList.Add("sky");
            if (depthsList.Count == 0)
            {
                AesLog.LogWarning("Abyssal Depths list empty - adding vanilla...");
                depthsList.Add("vanilla");
            }
            if (VanillaGrove.Value) groveList.Add("vanilla");
            if (GreenGrove.Value) groveList.Add("green");
            if (SunnyGrove.Value) groveList.Add("sunny");
            if (groveList.Count == 0)
            {
                AesLog.LogWarning("Sundered Grove list empty - adding vanilla...");
                groveList.Add("vanilla");
            }
            if (VanillaSiren.Value) sirenList.Add("vanilla");
            if (NightSiren.Value) sirenList.Add("night");
            if (SunnySiren.Value) sirenList.Add("sunny");
            if (sirenList.Count == 0)
            {
                AesLog.LogWarning("Siren's Call list empty - adding vanilla...");
                sirenList.Add("vanilla");
            }
            if (VanillaMeadow.Value) meadowList.Add("vanilla");
            if (NightMeadow.Value) meadowList.Add("night");
            if (StormyMeadow.Value) meadowList.Add("storm");
            if (meadowList.Count == 0)
            {
                AesLog.LogWarning("Sky Meadow list empty - adding vanilla...");
                meadowList.Add("vanilla");
            }
        }
        public static String warning2;
    }
}
