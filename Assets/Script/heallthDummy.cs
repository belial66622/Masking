using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Script.Utility;
using UnityEditor;
using UnityEngine;

namespace Assets.Script
{
    internal class heallthDummy : MonoBehaviour, Ihealth
    {
        public float MaxHealth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<float> OnHealthChange;
        public event Action OnDeath;

        public void OnDamage(float damage)
        {
            Helper.Log("not adooh");
        }

        public void SetHealth()
        {
            throw new NotImplementedException();
        }
    }
}
