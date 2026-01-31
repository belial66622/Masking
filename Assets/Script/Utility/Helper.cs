using System;
using System.Collections;
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
    }
}
