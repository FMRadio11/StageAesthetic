using R2API;
using RoR2;
using UnityEngine;

namespace VisionsExpose
{
    class MonsterLists : Aesthetic
    {
        public static void PlainsTwo()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscGolem", DirectorAPI.Stage.TitanicPlains);
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscHermitCrab", DirectorAPI.Stage.TitanicPlains);
            DirectorAPI.Helpers.AddNewMonsterToStage(elderLem5, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.TitanicPlains);
            DirectorAPI.Helpers.AddNewMonsterToStage(beetleGuard, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.TitanicPlains);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void PlainsThree()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscHermitCrab", DirectorAPI.Stage.TitanicPlains);
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscLesserWisp", DirectorAPI.Stage.TitanicPlains);
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscJellyfish", DirectorAPI.Stage.TitanicPlains);
            DirectorAPI.Helpers.AddNewMonsterToStage(vulture5, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.TitanicPlains);
            DirectorAPI.Helpers.AddNewMonsterToStage(miniMushruum5, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.TitanicPlains);
            DirectorAPI.Helpers.AddNewMonsterToStage(jellyfish0, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.TitanicPlains);
            if (StageRestriction.Value)
            {
                if (Run.instance.stageClearCount >= 2)
                {
                    if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscVagrant", DirectorAPI.Stage.TitanicPlains);
                    plainsVagrant = true;
                    if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(magmaWorm, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.TitanicPlains);
                }
            }
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(overloadingWormPlains, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.TitanicPlains);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void PlainsReset()
        {
            if (plainsVariant == 2)
            {
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscLemurianBruiser", DirectorAPI.Stage.TitanicPlains);
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscBeetleGuard", DirectorAPI.Stage.TitanicPlains);
                DirectorAPI.Helpers.AddNewMonsterToStage(hermitCrab, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.TitanicPlains);
                DirectorAPI.Helpers.AddNewMonsterToStage(stoneGolem, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.TitanicPlains);
            }
            if (plainsVariant == 3)
            {
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscVulture", DirectorAPI.Stage.TitanicPlains);
                if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscMagmaWorm", DirectorAPI.Stage.TitanicPlains);
                if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscElectricWorm", DirectorAPI.Stage.TitanicPlains);
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscJellyfish", DirectorAPI.Stage.TitanicPlains);
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscMiniMushroom", DirectorAPI.Stage.TitanicPlains);
                DirectorAPI.Helpers.AddNewMonsterToStage(hermitCrab, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.TitanicPlains);
                DirectorAPI.Helpers.AddNewMonsterToStage(jellyfish, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.TitanicPlains);
                DirectorAPI.Helpers.AddNewMonsterToStage(lesserWisp, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.TitanicPlains);
                bool vagrantCheck = Run.instance.stageClearCount >= 2 || !StageRestriction.Value;
                if (vagrantCheck && plainsVagrant == true)
                {
                    if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(wanderingVagrant, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.TitanicPlains);
                    plainsVagrant = false;
                }
            }
        }
        public static void RoostTwo()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscJellyfish", DirectorAPI.Stage.DistantRoost);
            DirectorAPI.Helpers.AddNewMonsterToStage(solusProbeRoost, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.DistantRoost);
            if (StageRestriction.Value)
            {
                if (Run.instance.stageClearCount >= 2)
                {
                    if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscBeetleQueen", DirectorAPI.Stage.DistantRoost);
                    if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(solusControlUnit, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.DistantRoost);
                }
            }
            else
            {
                if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscBeetleQueen", DirectorAPI.Stage.DistantRoost);
                if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(solusControlUnit, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.DistantRoost);
            }
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void RoostThree()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscLesserWisp", DirectorAPI.Stage.DistantRoost);
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscGreaterWisp", DirectorAPI.Stage.DistantRoost);
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscJellyfish", DirectorAPI.Stage.DistantRoost);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscVagrant", DirectorAPI.Stage.DistantRoost);
            DirectorAPI.Helpers.AddNewMonsterToStage(imp, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.DistantRoost);
            DirectorAPI.Helpers.AddNewMonsterToStage(vulture, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.DistantRoost);
            DirectorAPI.Helpers.AddNewMonsterToStage(elderLem, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.DistantRoost);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(impOverlord, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.DistantRoost);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void RoostReset()
        {
            if (roostVariant == 2)
            {
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscRoboBallMini", DirectorAPI.Stage.DistantRoost);
                if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscRoboBallBoss", DirectorAPI.Stage.DistantRoost);
                bool queenCheck = Run.instance.stageClearCount >= 2 || !StageRestriction.Value;
                if (queenCheck && roostQueen)
                {
                    if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(beetleQueen, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.DistantRoost);
                    roostQueen = false;
                }
                DirectorAPI.Helpers.AddNewMonsterToStage(jellyfish, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.DistantRoost);
            }
            if (roostVariant == 3)
            {
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscImp", DirectorAPI.Stage.DistantRoost);
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscVulture", DirectorAPI.Stage.DistantRoost);
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscLemurianBruiser", DirectorAPI.Stage.DistantRoost);
                if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscImpOverlord", DirectorAPI.Stage.DistantRoost);
                DirectorAPI.Helpers.AddNewMonsterToStage(lesserWisp, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.DistantRoost);
                DirectorAPI.Helpers.AddNewMonsterToStage(greaterWisp, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.DistantRoost);
                DirectorAPI.Helpers.AddNewMonsterToStage(jellyfish, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.DistantRoost);
                if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(wanderingVagrant, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.DistantRoost);
            }
        }
        public static void WetlandTwo()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscGolem", DirectorAPI.Stage.WetlandAspect);
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscBell", DirectorAPI.Stage.WetlandAspect);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscVagrant", DirectorAPI.Stage.WetlandAspect);
            DirectorAPI.Helpers.AddNewMonsterToStage(beetleGuard, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.WetlandAspect);
            DirectorAPI.Helpers.AddNewMonsterToStage(greaterWisp, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.WetlandAspect);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(grovetender, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.WetlandAspect);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void WetlandThree()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscGolem", DirectorAPI.Stage.WetlandAspect);
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscBeetle", DirectorAPI.Stage.WetlandAspect);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscBeetleQueen", DirectorAPI.Stage.WetlandAspect);
            DirectorAPI.Helpers.AddNewMonsterToStage(miniMushruum, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.WetlandAspect);
            DirectorAPI.Helpers.AddNewMonsterToStage(parent, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.WetlandAspect);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(grandParent, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.WetlandAspect);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void WetlandReset()
        {
            if (wetlandVariant == 2)
            {
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscBeetleGuard", DirectorAPI.Stage.WetlandAspect);
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscGreaterWisp", DirectorAPI.Stage.WetlandAspect);
                if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscGravekeeper", DirectorAPI.Stage.WetlandAspect);
                DirectorAPI.Helpers.AddNewMonsterToStage(stoneGolem, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.WetlandAspect);
                DirectorAPI.Helpers.AddNewMonsterToStage(brassContraption, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.WetlandAspect);
                if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(wanderingVagrant, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.WetlandAspect);
            }
            if (wetlandVariant == 3)
            {
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscMiniMushroom", DirectorAPI.Stage.WetlandAspect);
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscParent", DirectorAPI.Stage.WetlandAspect);
                if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscGrandparent", DirectorAPI.Stage.WetlandAspect);
                DirectorAPI.Helpers.AddNewMonsterToStage(stoneGolem, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.WetlandAspect);
                DirectorAPI.Helpers.AddNewMonsterToStage(beetle, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.WetlandAspect);
                if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(beetleQueen, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.WetlandAspect);
            }
        }
        public static void AqueductTwo()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscLemurian", DirectorAPI.Stage.AbandonedAqueduct);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscTitanGooLake", DirectorAPI.Stage.AbandonedAqueduct);
            DirectorAPI.Helpers.AddNewMonsterToStage(imp, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.AbandonedAqueduct);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(impOverlord, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.AbandonedAqueduct);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void AqueductThree()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscLesserWisp", DirectorAPI.Stage.AbandonedAqueduct);
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscGreaterWisp", DirectorAPI.Stage.AbandonedAqueduct);
            DirectorAPI.Helpers.AddNewMonsterToStage(hermitCrab, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.AbandonedAqueduct);
            DirectorAPI.Helpers.AddNewMonsterToStage(brassContraption, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.AbandonedAqueduct);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void AqueductReset()
        {
            if (aqueductVariant == 2)
            {
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscImp", DirectorAPI.Stage.AbandonedAqueduct);
                if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscImpBoss", DirectorAPI.Stage.AbandonedAqueduct);
                DirectorAPI.Helpers.AddNewMonsterToStage(lemurian, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.AbandonedAqueduct);
                if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(aqueductTitan, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.AbandonedAqueduct);
            }
            if (aqueductVariant == 3)
            {
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscHermitCrab", DirectorAPI.Stage.AbandonedAqueduct);
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscBell", DirectorAPI.Stage.AbandonedAqueduct);
                DirectorAPI.Helpers.AddNewMonsterToStage(lesserWisp, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.AbandonedAqueduct);
                DirectorAPI.Helpers.AddNewMonsterToStage(greaterWisp, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.AbandonedAqueduct);
            }
        }
        public static void DeltaTwo()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscLesserWisp", DirectorAPI.Stage.RallypointDelta);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscMagmaWorm", DirectorAPI.Stage.RallypointDelta);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscElectricWorm", DirectorAPI.Stage.RallypointDelta);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscClayBoss", DirectorAPI.Stage.RallypointDelta);
            DirectorAPI.Helpers.AddNewMonsterToStage(solusProbe, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.RallypointDelta);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(solusControlUnit, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.RallypointDelta);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(roostTitan, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.RallypointDelta);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void DeltaThree()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscLemurian", DirectorAPI.Stage.RallypointDelta);
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscGolem", DirectorAPI.Stage.RallypointDelta);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscMagmaWorm", DirectorAPI.Stage.RallypointDelta);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscElectricWorm", DirectorAPI.Stage.RallypointDelta);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscClayBoss", DirectorAPI.Stage.RallypointDelta);
            DirectorAPI.Helpers.AddNewMonsterToStage(beetleGuard, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.RallypointDelta);
            DirectorAPI.Helpers.AddNewMonsterToStage(vulture, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.RallypointDelta);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(grovetender, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.RallypointDelta);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(beetleQueen, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.RallypointDelta);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void DeltaReset()
        {
            if (deltaVariant != 1)
            {
                if (deltaVariant == 2)
                {
                    DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscRoboBallMini", DirectorAPI.Stage.RallypointDelta);
                    if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscRoboBallBoss", DirectorAPI.Stage.RallypointDelta);
                    if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscTitanBlackBeach", DirectorAPI.Stage.RallypointDelta);
                    DirectorAPI.Helpers.AddNewMonsterToStage(lesserWisp, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.RallypointDelta);
                }
                if (deltaVariant == 3)
                {
                    DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscBeetleGuard", DirectorAPI.Stage.RallypointDelta);
                    DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscVulture", DirectorAPI.Stage.RallypointDelta);
                    if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscGravekeeper", DirectorAPI.Stage.RallypointDelta);
                    if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscBeetleQueen", DirectorAPI.Stage.RallypointDelta);
                    DirectorAPI.Helpers.AddNewMonsterToStage(lemurian, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.RallypointDelta);
                    DirectorAPI.Helpers.AddNewMonsterToStage(bison, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.RallypointDelta);
                }
                if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(magmaWorm, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.RallypointDelta);
                if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(overloadingWorm, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.RallypointDelta);
                if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(clayDunestrider, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.RallypointDelta);
            }
        }
        public static void AcresTwo()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscBeetleGuard", DirectorAPI.Stage.ScorchedAcres);
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscClayBruiser", DirectorAPI.Stage.ScorchedAcres);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscImpBoss", DirectorAPI.Stage.ScorchedAcres);
            DirectorAPI.Helpers.AddNewMonsterToStage(stoneGolem, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.ScorchedAcres);
            DirectorAPI.Helpers.AddNewMonsterToStage(elderLem, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.ScorchedAcres);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(aqueductTitan, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.ScorchedAcres);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void AcresThree()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscVulture", DirectorAPI.Stage.ScorchedAcres);
            DirectorAPI.Helpers.AddNewMonsterToStage(brassContraption, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.ScorchedAcres);
            DirectorAPI.Helpers.AddNewMonsterToStage(zenithDesignsWisp, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.ScorchedAcres);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void AcresReset()
        {
            if (acresVariant == 2)
            {
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscGolem", DirectorAPI.Stage.ScorchedAcres);
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscLemurianBruiser", DirectorAPI.Stage.ScorchedAcres);
                if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscTitanGooLake", DirectorAPI.Stage.ScorchedAcres);
                DirectorAPI.Helpers.AddNewMonsterToStage(beetleGuard, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.ScorchedAcres);
                DirectorAPI.Helpers.AddNewMonsterToStage(clayTemplar, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.ScorchedAcres);
                if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(impOverlord, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.ScorchedAcres);
            }
            if (acresVariant == 3)
            {
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscBell", DirectorAPI.Stage.ScorchedAcres);
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscLunarWisp", DirectorAPI.Stage.ScorchedAcres);
                DirectorAPI.Helpers.AddNewMonsterToStage(vulture, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.ScorchedAcres);
            }
        }
        public static void DepthsTwo()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscLemurian", DirectorAPI.Stage.AbyssalDepths);
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscBell", DirectorAPI.Stage.AbyssalDepths);
            DirectorAPI.Helpers.AddNewMonsterToStage(lesserWisp, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.AbyssalDepths);
            DirectorAPI.Helpers.AddNewMonsterToStage(stoneGolem, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.AbyssalDepths);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void DepthsOne()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscLesserWisp", DirectorAPI.Stage.AbyssalDepths);
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscGolem", DirectorAPI.Stage.AbyssalDepths);
            DirectorAPI.Helpers.AddNewMonsterToStage(lemurian, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.AbyssalDepths);
            DirectorAPI.Helpers.AddNewMonsterToStage(brassContraption, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.AbyssalDepths);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void SirenTwo()
        {
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscMagmaWorm", DirectorAPI.Stage.SirensCall);
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscBell", DirectorAPI.Stage.SirensCall);
            DirectorAPI.Helpers.AddNewMonsterToStage(parent, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.SirensCall);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(grandParent, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.SirensCall);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void SirenOne()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscParent", DirectorAPI.Stage.SirensCall);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscGrandparent", DirectorAPI.Stage.SirensCall);
            DirectorAPI.Helpers.AddNewMonsterToStage(brassContraption, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.SirensCall);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(magmaWorm, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.SirensCall);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void GroveTwo()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscLemurian", DirectorAPI.Stage.SunderedGrove);
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscJellyfish", DirectorAPI.Stage.SunderedGrove);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscVagrant", DirectorAPI.Stage.SunderedGrove);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscClayBoss", DirectorAPI.Stage.SunderedGrove);
            DirectorAPI.Helpers.AddNewMonsterToStage(imp, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.SunderedGrove);
            DirectorAPI.Helpers.AddNewMonsterToStage(lesserWisp, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.SunderedGrove);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(grovetender, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.SunderedGrove);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(impOverlord, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.SunderedGrove);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void GroveOne()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscImp", DirectorAPI.Stage.SunderedGrove);
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscLesserWisp", DirectorAPI.Stage.SunderedGrove);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscGravekeeper", DirectorAPI.Stage.SunderedGrove);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscImpBoss", DirectorAPI.Stage.SunderedGrove);
            DirectorAPI.Helpers.AddNewMonsterToStage(lemurian, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.SunderedGrove);
            DirectorAPI.Helpers.AddNewMonsterToStage(jellyfish0, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.SunderedGrove);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(wanderingVagrant, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.SunderedGrove);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(clayDunestrider, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.SunderedGrove);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void MeadowTwo()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscBell", DirectorAPI.Stage.SkyMeadow);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscRoboBallBoss", DirectorAPI.Stage.SkyMeadow);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscMagmaWorm", DirectorAPI.Stage.SkyMeadow);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscElectricWorm", DirectorAPI.Stage.SkyMeadow);
            DirectorAPI.Helpers.AddNewMonsterToStage(imp, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.SkyMeadow);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(wanderingVagrant, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.SkyMeadow);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(impOverlord, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.SkyMeadow);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void MeadowThree()
        {
            DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscLesserWisp", DirectorAPI.Stage.SkyMeadow);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscMagmaWorm", DirectorAPI.Stage.SkyMeadow);
            if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscElectricWorm", DirectorAPI.Stage.SkyMeadow);
            DirectorAPI.Helpers.AddNewMonsterToStage(jellyfish0, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.SkyMeadow);
            DirectorAPI.Helpers.AddNewMonsterToStage(zenithDesignsBall, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.SkyMeadow);
            if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(wanderingVagrant, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.SkyMeadow);
            DirectorAPI.Helpers.AddNewMonsterToStage(zenithDesignsGolem, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.SkyMeadow);
            DirectorAPI.Helpers.TryApplyChangesNow();
        }
        public static void MeadowReset()
        {
            if (meadowVariant == 2)
            {
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscImp", DirectorAPI.Stage.SkyMeadow);
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscVagrant", DirectorAPI.Stage.SkyMeadow);
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscImpBoss", DirectorAPI.Stage.SkyMeadow);
                DirectorAPI.Helpers.AddNewMonsterToStage(brassContraption, DirectorAPI.MonsterCategory.Minibosses, DirectorAPI.Stage.SkyMeadow);
                if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(magmaWorm, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.SkyMeadow);
                if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(overloadingWorm, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.SkyMeadow);
                if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(solusControlUnit, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.SkyMeadow);
            }
            if (meadowVariant == 3)
            {
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscJellyfish", DirectorAPI.Stage.SkyMeadow);
                if (!BossConfig.Value) DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscVagrant", DirectorAPI.Stage.SkyMeadow);
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscLunarGolem", DirectorAPI.Stage.SkyMeadow);
                DirectorAPI.Helpers.RemoveExistingMonsterFromStage("cscLunarExploder", DirectorAPI.Stage.SkyMeadow);
                DirectorAPI.Helpers.AddNewMonsterToStage(lesserWisp, DirectorAPI.MonsterCategory.BasicMonsters, DirectorAPI.Stage.SkyMeadow);
                if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(magmaWorm, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.SkyMeadow);
                if (!BossConfig.Value) DirectorAPI.Helpers.AddNewMonsterToStage(overloadingWorm, DirectorAPI.MonsterCategory.Champions, DirectorAPI.Stage.SkyMeadow);
            }
        }
        public static void CardSetup()
        {
            overloadingWormPlains = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscElectricWorm"),
                selectionWeight = 1,
                allowAmbushSpawn = false,
                preventOverhead = false,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            vulture5 = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVulture"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            elderLem5 = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLemurianBruiser"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            beetleGuard = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBeetleGuard"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            hermitCrab = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscHermitCrab"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 1,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            stoneGolem = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGolem"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            solusProbeRoost = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallMini"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 1,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            imp = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImp"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            elderLem = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLemurianBruiser"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            impOverlord = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImpBoss"),
                selectionWeight = 1,
                allowAmbushSpawn = false,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            vulture = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVulture"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            vultureAcres = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVulture"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            beetleQueen = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBeetleQueen"),
                selectionWeight = 1,
                allowAmbushSpawn = false,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            wanderingVagrant = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVagrant"),
                selectionWeight = 1,
                allowAmbushSpawn = false,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            lesserWisp = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLesserWisp"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            greaterWisp = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGreaterWisp"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            jellyfish = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscJellyfish"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 1,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            woodShrine = new DirectorCard
            {
                spawnCard = Resources.Load<InteractableSpawnCard>("SpawnCards/InteractableSpawnCards/iscShrineHealing"),
                selectionWeight = 1,
                minimumStageCompletions = 0
            };
            miniMushruum = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMiniMushroom"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            parent = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscParent"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            grandParent = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/titan/cscGrandparent"),
                selectionWeight = 1,
                allowAmbushSpawn = false,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            beetle = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBeetle"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            brassContraption = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBell"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            grovetender = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGravekeeper"),
                selectionWeight = 1,
                allowAmbushSpawn = false,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            lemurian = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLemurian"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            aqueductTitan = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/titan/cscTitanGooLake"),
                selectionWeight = 1,
                allowAmbushSpawn = false,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            miniMushruum5 = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMiniMushroom"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            jellyfish0 = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscJellyfish"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            roostTitan = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/titan/cscTitanBlackBeach"),
                selectionWeight = 1,
                allowAmbushSpawn = false,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            solusProbe = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallMini"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            solusControlUnit = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallBoss"),
                selectionWeight = 1,
                allowAmbushSpawn = false,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            magmaWorm = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMagmaWorm"),
                selectionWeight = 1,
                allowAmbushSpawn = false,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            overloadingWorm = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscElectricWorm"),
                selectionWeight = 1,
                allowAmbushSpawn = false,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            clayDunestrider = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayBoss"),
                selectionWeight = 1,
                allowAmbushSpawn = false,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            bison = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBison"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            zenithDesignsWisp = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarWisp"),
                selectionWeight = 30,
                allowAmbushSpawn = false,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            zenithDesignsGolem = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarGolem"),
                selectionWeight = 30,
                allowAmbushSpawn = false,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            zenithDesignsBall = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarExploder"),
                selectionWeight = 5,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            clayTemplar = new DirectorCard
            {
                spawnCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayBruiser"),
                selectionWeight = 1,
                allowAmbushSpawn = true,
                preventOverhead = false,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
        }
        public static DirectorCard overloadingWormPlains;
        public static DirectorCard vulture5;
        public static DirectorCard elderLem5;
        public static DirectorCard beetleGuard;
        public static DirectorCard hermitCrab;
        public static DirectorCard stoneGolem;
        public static DirectorCard solusProbeRoost;
        public static DirectorCard imp;
        public static DirectorCard elderLem;
        public static DirectorCard impOverlord;
        public static DirectorCard vulture;
        public static DirectorCard beetleQueen;
        public static DirectorCard wanderingVagrant;
        public static DirectorCard lesserWisp;
        public static DirectorCard greaterWisp;
        public static DirectorCard jellyfish;
        public static DirectorCard woodShrine;
        public static DirectorCard miniMushruum;
        public static DirectorCard parent;
        public static DirectorCard grandParent;
        public static DirectorCard beetle;
        public static DirectorCard brassContraption;
        public static DirectorCard grovetender;
        public static DirectorCard lemurian;
        public static DirectorCard aqueductTitan;
        public static DirectorCard zenithDesignsWisp;
        public static DirectorCard zenithDesignsGolem;
        public static DirectorCard zenithDesignsBall;
        public static DirectorCard miniMushruum5;
        public static DirectorCard jellyfish0;
        public static DirectorCard roostTitan;
        public static DirectorCard solusControlUnit;
        public static DirectorCard solusProbe;
        public static DirectorCard magmaWorm;
        public static DirectorCard overloadingWorm;
        public static DirectorCard clayDunestrider;
        public static DirectorCard bison;
        public static DirectorCard clayTemplar;
        public static DirectorCard vultureAcres;
        public static bool plainsVagrant;
        public static bool roostQueen;
    }
}
