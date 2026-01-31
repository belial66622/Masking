using System;
using Assets.Script.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script
{
    internal class heallthDummy : MonoBehaviour, Ihealth
    {
        [field:SerializeField]
        public float MaxHealth{ get; set; }

        public event Action<float> OnHealthChange;
        public event Action OnDeath;

        private float currentHealth;


        [SerializeField]
        Image healthBar;

        private void Start()
        {
            SetHealth();
        }

        public void OnDamage(float damage)
        {
            currentHealth -= damage;
            healthBar.fillAmount = currentHealth/MaxHealth;
            Helper.Log("asda");
        }

        public void SetHealth()
        {
            currentHealth = MaxHealth;
        }
    }
}
