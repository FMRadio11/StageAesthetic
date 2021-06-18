using R2API.Utils;
using R2API.Networking;
using BepInEx;

namespace VisionsExpose
{
    [BepInPlugin("com.FMRadio11.StageAesthetics", "StageAesthetics", "0.0.5")]
    [R2APISubmoduleDependency(new string[]
    {
        "DirectorAPI",
        "ArtifactAPI",
        "NetworkingAPI"
    })]
    class StageAesthetics : BaseUnityPlugin 
    {
        public void Awake()
        {
            NetworkingAPI.RegisterMessageType<Aesthetic.AestheticSync>();
            Aesthetic.Nice();
        }
    }
}
