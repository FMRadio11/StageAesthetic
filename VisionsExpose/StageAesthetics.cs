using R2API.Utils;
using BepInEx;

namespace VisionsExpose
{
    [BepInPlugin("com.FMRadio11.StageAesthetics", "StageAesthetics", "0.0.1")]
    [R2APISubmoduleDependency(new string[]
    {
        "DirectorAPI",
        "ArtifactAPI"
    })]
    class StageAesthetics : BaseUnityPlugin 
    {
        public void Awake()
        {
            Aesthetic.Nice();
        }
    }
}
