using R2API.Utils;
using R2API.Networking;
using BepInEx;

namespace StageAesthetic
{
    [BepInPlugin("com.FMRadio11.StageAesthetics", "StageAesthetics", "0.1.0")]
    [R2APISubmoduleDependency(new string[]
    {
        "DirectorAPI",
        "ArtifactAPI",
        "NetworkingAPI",
        "PrefabAPI"
    })]
    class StageAesthetics : BaseUnityPlugin 
    {
        public void Awake()
        {
            Aesthetic.AesLog = this.Logger;
            Aesthetic.Nice();
        }
    }
}
