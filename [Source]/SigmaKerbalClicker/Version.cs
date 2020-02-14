using UnityEngine;


namespace SigmaKerbalClickerPlugin
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class Version : MonoBehaviour
    {
        public static readonly System.Version number = new System.Version("0.1.0");

        void Awake()
        {
            Debug.Log("[SigmaLog] Version Check:   Sigma KerbalClicker v" + number);
        }
    }
}
