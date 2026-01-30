using Assets.Script.Interface;
using Assets.Script.Utility;
using UnityEngine;

namespace Assets.Script.Boss.State
{
    internal class BossStunState : IState
    {
        private BossStateControl _bossStateControl;
        private IBossState _bossState;

        private float _defStunTime;

        private float _currentCooldownTime;
        public BossStunState(BossStateControl bossStateControl,float cooldownTime)
        {
            _bossStateControl = bossStateControl;
            _defStunTime = cooldownTime;
            _bossState = bossStateControl;
        }

        public void OnEnter()
        {
            Helper.Log("Stun Enter");
            _currentCooldownTime = _defStunTime;
        }

        public void OnExit()
        {
            Helper.Log("Stun Exit");
        }

        public void Tick()
        {
            _currentCooldownTime -= Time.deltaTime ;
            if (_currentCooldownTime <= 0)
            {
                _bossState.ClearStun();
            }
        }


    }
}
