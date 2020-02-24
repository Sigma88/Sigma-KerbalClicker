using UnityEngine;


namespace SigmaKerbalClickerPlugin
{
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    internal class KerbalClickerLoader : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(CallbackUtil.DelayedCallback(1, AddKerbalClicker));
        }

        void AddKerbalClicker()
        {
            GameObject[] scenes = FindObjectOfType<MainMenu>().envLogic.areas;

            for (int i = 0; i < 2; i++)
            {
                if (!scenes[i].activeSelf) continue;

                for (int j = 0; j < scenes[i].GetChild("Kerbals").transform.childCount; j++)
                {
                    GameObject kerbal = scenes[i].GetChild("Kerbals").transform.GetChild(j).gameObject;

                    GameObject capsule = kerbal.GetChild("joints01_capsule_collider");

                    if (capsule == null)
                    {
                        capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                        capsule.name = "joints01_capsule_collider";
                        capsule.layer = 15;
                        capsule.transform.SetParent(kerbal.GetChild("joints01").transform);
                        capsule.transform.localPosition = new Vector3(0, 0.42f, -0.025f);
                        capsule.transform.localScale = new Vector3(0.25f, 0.42f, 0.35f);
                        capsule.transform.localRotation = Quaternion.identity;
                        capsule.transform.SetParent(kerbal.GetChild("bn_spA01").transform);
                        DestroyImmediate(capsule.GetComponent<Renderer>());
                    }

                    GameObject helmet = kerbal.GetChild("helmet01") ?? kerbal.GetChild("mesh_female_kerbalAstronaut01_helmet01");
                    GameObject sphere = kerbal.GetChild("bn_helmet01_sphere_collider");

                    if (helmet?.activeSelf == true && sphere == null)
                    {
                        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        sphere.name = "bn_helmet01_sphere_collider";
                        sphere.layer = 15;
                        sphere.transform.SetParent(kerbal.GetChild("bn_helmet01").transform, true);
                        sphere.transform.localPosition = new Vector3(-0.25f, 0.05f, 0);
                        sphere.transform.localScale = new Vector3(0.58f, 0.58f, 0.58f);
                        sphere.transform.localRotation = Quaternion.identity;
                        DestroyImmediate(sphere.GetComponent<Renderer>());
                    }

                    // Kerbal Clicker
                    KerbalClicker kc = capsule.AddOrGetComponent<KerbalClicker>();

                    // Mun Scene
                    kc.mrka = kerbal.GetComponent<MenuRandomKerbalAnims>();
                    kc.animation = kerbal.GetComponent<Animation>();

                    // OrbitScene
                    kc.animator = kerbal.GetComponent<Animator>();
                    kc.bobber = kerbal.GetComponent<Bobber>();

                    // Helmet Clicker
                    if (sphere != null)
                    {
                        sphere.AddOrGetComponent<HelmetClicker>().kc = kc;
                    }
                }
            }
        }
    }
}
