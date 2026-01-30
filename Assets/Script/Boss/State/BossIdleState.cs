using Assets.Script.Interface;
using Assets.Script.Utility;
using UnityEngine;

namespace Assets.Script.Boss.State
{
    internal class BossIdleState : IState
    {
        private BossStateControl _bossStateControl;

        private IBossState _bossState;

        private Ihealth _health;
        private float _currentHealth;

        private float _defCooldownTime;

        private float _currentCooldownTime;
        public BossIdleState(BossStateControl bossStateControl, Ihealth health , float cooldownTime)
        {
            _bossStateControl = bossStateControl;
            _bossState = bossStateControl;
            _health = health;
            _defCooldownTime = cooldownTime;
            _health.OnHealthChange += HealthChange;
        }

        public void OnEnter()
        {
            Helper.Log($"Idle Enter {_defCooldownTime}");
            _bossState.ChooseAttack();
            _currentCooldownTime = _defCooldownTime;
        }

        public void OnExit()
        {
            Helper.Log("Idle Exit");
        }

        public void Tick()
        {
            _currentCooldownTime -= Time.deltaTime * SetMultiplier();
            if (_currentCooldownTime <= 0)
            {
                _bossState.CanAttack();
            }
        }

        private void HealthChange(float health)
        {
            _currentHealth = health;
        }

        private float SetMultiplier()
        {
            if (_currentHealth < _health.MaxHealth * .45f)
            {
                return 2;
            }
            return 1;
        }

    }
}
