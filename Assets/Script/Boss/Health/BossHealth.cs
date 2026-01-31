using System;
using System.Collections;
using Assets.Script.Interface;
using UnityEngine;
using NaughtyAttributes;
using Assets.Script.Utility;
using UnityEngine.UI;

namespace Script.Boss.Health
{
    public class BossHealth : MonoBehaviour, Ihealth, Ipoise ,IEventDelete
    {
        #region Variable

        private float _currentHealth;

        private float _currentPoise;

        [SerializeField] private Material _damagedMaterial;

        [SerializeField] private BossMaterialControl _currentMesh;

        [SerializeField] private Image _healthHud;

        private Coroutine _coroutine;
        #endregion

        #region Ihealth
        [field: SerializeField]
        public float MaxHealth { get; set; }

        public event Action OnDeath;

        public event Action<float> OnHealthChange;
        public void OnDamage(float damage)
        {
            if (damage > 0)
            {
                ChangeMat(_currentMesh, _damagedMaterial);
            }
            ChangeHealth(damage);
            _healthHud.fillAmount = _currentHealth/MaxHealth;
            OnHealthChange?.Invoke(_currentHealth);
            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
        }

        public void SetHealth()
        {
            _currentHealth = MaxHealth;
            OnHealthChange?.Invoke(_currentHealth);
        }
        #endregion

        #region IPoise
        public event Action<float> OnPoiseChange;

        public event Action OnPoiseDepleted;
        [field: SerializeField]
        public float Maxpoise { get; set; }
        public void OnPoiseDamage(float damage)
        {
            ChangePoise(damage);
            OnPoiseChange?.Invoke(_currentPoise);

            if (_currentPoise <= 0)
            {
                OnPoiseDepleted?.Invoke();
                SetPoise();
            }
        }

        public void SetPoise()
        {
            _currentPoise = Maxpoise;

        }
        #endregion

        #region Main
        private void ChangeHealth(float damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, MaxHealth);
        }

        private void ChangePoise(float damage)
        {
            _currentPoise = Mathf.Clamp(_currentPoise - damage, 0, Maxpoise);
        }

        private void ChangeMat(BossMaterialControl mesh, Material to)
        {
            mesh.changeMat(to);
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(Helper.delay(() =>
                {
                    mesh.changeMat();
                    _coroutine = null;
                },.1f));
            }
        }



        #endregion

        #region Naugty attribute
        [Button]
        public void Damage()
        {
            //SetHealth();
            OnDamage(10);
        }

        [Button]
        public void PoiseDamage()
        {
            //SetHealth();
            OnPoiseDamage(10);
        }

        #endregion

        #region IEventDelete
        public void EventDelete()
        {
            OnDeath = null;
            OnHealthChange = null;
            OnPoiseChange = null;
            OnPoiseDepleted = null;
        } 
        #endregion
    }
}