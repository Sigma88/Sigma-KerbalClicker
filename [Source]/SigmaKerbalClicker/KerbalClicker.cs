using UnityEngine;
using System.Reflection;
using Random = UnityEngine.Random;


namespace SigmaKerbalClickerPlugin
{
    internal class KerbalClicker : MonoBehaviour
    {
        // MunScene
        internal Animation animation;
        internal MenuRandomKerbalAnims mrka;
        static float[] groundDelays = new float[] { 0.25f, 0.2f, 0.25f, 0.4f, 0, 0.1f, 0.35f };
        static string[] groundAnims = new string[] { "idle_a", "idle_b", "idle_d", "idle_f", "idle_g", "idle_i", "idle_c" };

        // OrbitScene
        internal Animator animator;
        internal Bobber bobber;

        internal void OnMouseDown()
        {
            if (mrka != null)
            {
                if (!animation.IsPlaying("idle")) return;

                int rnd = Random.Range(0, mrka.anims.Length - 1);

                string clipName = groundAnims[rnd];
                animation.Play(clipName);
                animation[clipName].normalizedTime = groundDelays[rnd];

                typeof(MenuRandomKerbalAnims).GetField("elapsedTime", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(mrka, 0);
            }

            else

            if (animator != null)
            {
                animator.SetFloat("Expression", Random.Range(0, 9) * 0.25f - 1);
                animator.SetFloat("Variance", Random.Range(0, 5) * 0.25f);
                animator.SetFloat("SecondaryVariance", Random.Range(0, 5) * 0.25f);
            }

            else

            if (animation != null)
            {
                mouseDown = true;
            }
        }

        bool mouseDown = false;
        bool start = true;
        Vector3 originalPos;
        Vector3 offset;

        void LateUpdate()
        {
            if (mouseDown)
            {
                if (start)
                {
                    if (Input.GetMouseButtonUp(0)) return;

                    if (bobber != null)
                        bobber.enabled = false;

                    start = false;
                    originalPos = animation.transform.position;

                    float deltaZ = originalPos.z - Camera.main.transform.position.z;
                    Vector3 mousePos = Input.mousePosition + Vector3.forward * deltaZ;
                    Vector3 world = Camera.main.ScreenToWorldPoint(mousePos);
                    offset = originalPos - world;
                }
                else
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        start = true;
                        mouseDown = false;
                        animation.transform.position = originalPos;

                        if (bobber != null)
                            bobber.enabled = true;
                    }
                    else
                    {
                        float deltaZ = animation.transform.position.z - Camera.main.transform.position.z;
                        Vector3 mousePos = Input.mousePosition + Vector3.forward * deltaZ;
                        Vector3 world = Camera.main.ScreenToWorldPoint(mousePos);
                        animation.transform.position = world + offset;
                    }
                }
            }
        }
    }

    internal class HelmetClicker : MonoBehaviour
    {
        internal KerbalClicker kc;

        void OnMouseDown()
        {
            kc?.OnMouseDown();
        }
    }
}
