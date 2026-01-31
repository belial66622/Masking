using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ihealth 
{
    public float MaxHealth { get; set; }
    public void OnDamage(float damage);

    public event Action<float> OnHealthChange;

    public void SetHealth();

    public event Action OnDeath;
}
