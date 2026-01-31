using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Script.Utility
{
    public static class Helper
    {
        public static IEnumerator delay(Action Onfinished, float time)
        {
            yield return new WaitForSeconds(time);
            Onfinished?.Invoke();
        }

        public static void Log(string log)
        {
            Debug.Log(log);
        }

        public static IEnumerator Shake(this Transform transform, float duration, float magnitude , Action action = null)
        {
            Vector3 originalPosition = transform.localPosition;
            float elapsed = 0f;
            float proMag = magnitude;
            while (elapsed < 1)
            {
                yield return null;

                proMag = Mathf.Lerp(magnitude, 0, easeInExpo(elapsed));
                // Use Perlin noise for smoother shake
                float x = originalPosition.x + (Mathf.PerlinNoise(Time.time * 10f, 0f) * 2f - 1f) * proMag;
                float y = originalPosition.y + (Mathf.PerlinNoise(0f, Time.time * 10f) * 2f - 1f) * proMag;

                transform.localPosition = new Vector3(x, y, originalPosition.z);

                elapsed += Time.deltaTime /duration;
                yield return null; // Wait for the next frame
            }

            transform.localPosition = originalPosition;
            action?.Invoke();
        }

        public static float easeInExpo(float x)
        {
                return x == 0 ? 0 : Mathf.Pow(2, 10 * x - 10);
        }
    }
}
