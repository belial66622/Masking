using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Script.Interface;
using UnityEngine;

namespace Assets.Script.Utility
{
    internal class CameraShake : MonoBehaviour, IShake
    {
        Coroutine _coroutine;
        public void ShakeCamera(float duration, float magnitude)
        {
            Helper.Log("mamamama");
            if (_coroutine == null)
            {
                Helper.Log("sasasasa");

                _coroutine = StartCoroutine(transform.Shake(duration, magnitude, () => { 
                    _coroutine = null;
                    Helper.Log("wawawaw");
                } ) );
            }
        }
    }
}
