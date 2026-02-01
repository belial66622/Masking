using System;
using Assets.Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Script
{
    public class heallthDummy : MonoBehaviour, Ihealth
    {
        [SerializeField]GameManagerScript bossDamage;
        public enum BlockType
        {None,Normal,Perfect}
        [field:SerializeField]
        public float MaxHealth{ get; set; }
        private BlockType blockType = BlockType.None;

        public event Action<float> OnHealthChange;
        public event Action OnDeath;
        private float BlockReduction = 0.5f;

        private float currentHealth;
        private bool hasShield = false;
        [SerializeField]
        private Animator shieldAnimator; 


        [SerializeField]
        Image healthBar;

        private void Start()
        {
            SetHealth();
        }



        public void OnDamage(float damage)
        {
            if (hasShield)
            {
                ConsumeShield();
                return;
            }
            float finalDamage = damage;
            bossDamage?.BossAttack();
            StartCoroutine(Helper.delay(
                () =>
                {
                    switch (blockType)
                    {
                        case BlockType.Perfect:
                            Helper.Log("Perfect Block!");
                            finalDamage = 0f;
                            break;

                        case BlockType.Normal:
                            Helper.Log("Block!");
                            finalDamage *= BlockReduction;
                            break;
                    }
                    if (finalDamage > 0)
                    {
                        SoundPlay.Instance.PlaySound("PlayerHit");
                    }
                    currentHealth -= finalDamage;
                    Debug.Log("final dmg"+ finalDamage);
                    currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
                    healthBar.fillAmount = currentHealth / MaxHealth;
                    OnHealthChange?.Invoke(currentHealth);
                    if (currentHealth == 0)
                    {
                        SceneManager.LoadScene(1);
                    }
                },bossDamage.parryTime));
            
            //Helper.Log("asda");
           
        }
        public void SetBlock(BlockType type)
        {
            blockType = type;
        }

        public void SetHealth()
        {
            currentHealth = MaxHealth;
            healthBar.fillAmount = 1f;
            
        }

        public void GrantShield()
        {
            hasShield = true;

            if (shieldAnimator != null)
            {
                shieldAnimator.gameObject.SetActive(true);
                shieldAnimator.ResetTrigger("Break");
                shieldAnimator.SetTrigger("Appear");
            }
        }

        void ConsumeShield()
        {
            hasShield = false;
            shieldAnimator.SetTrigger("Break");
        }

        public void DisableShield() 
        {
            shieldAnimator.gameObject.SetActive(false);
        }
    }
}
