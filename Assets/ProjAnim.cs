using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjAnim : MonoBehaviour
{
    public event Action OnAnim;
    public event Action OnSound;

    public void OnAnimActive()
    { 
        OnAnim?.Invoke();
    }

    public void OnSoundActive()
    {
        OnSound?.Invoke();
    }
}
