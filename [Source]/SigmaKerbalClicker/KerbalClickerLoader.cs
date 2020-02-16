using UnityEngine;


namespace SigmaKerbalClickerPlugin
{
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    internal class KerbalClickerLoader : MonoBehaviour
    {
        bool skip = false;

        void Start()
        {
            if (skip) return;
            skip = true;

            GameObject[] scenes = FindObjectOfType<MainMenu>().envLogic.areas;

            for (int i = 0; i < 2; i++)
            {
                if (!scenes[i].activeSelf) continue;

                for (int j = 0; j < scenes[i].GetChild("Kerbals").transform.childCount; j++)
                {
                    GameObject kerbal = scenes[i].GetChild("Kerbals").transform.GetChild(j).gameObject;

                    GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    capsule.transform.SetParent(kerbal.GetChild("joints01").transform, true);
                    capsule.transform.localPosition = new Vector3(0, 0.42f, -0.025f);
                    capsule.transform.localScale = new Vector3(0.25f, 0.42f, 0.35f);
                    capsule.transform.localRotation = Quaternion.identity;
                    DestroyImmediate(capsule.GetComponent<Renderer>());

                    // Kerbal Clicker
                    KerbalClicker kc = capsule.AddComponent<KerbalClicker>();

                    // Mun Scene
                    kc.mrka = kerbal.GetComponent<MenuRandomKerbalAnims>();
                    kc.animation = kerbal.GetComponent<Animation>();

                    // OrbitScene
                    kc.animator = kerbal.GetComponent<Animator>();
                    kc.bobber = kerbal.GetComponent<Bobber>();
                }
            }
        }
    }
}
