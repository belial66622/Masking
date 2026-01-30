using Assets.Script.Interface;
using Assets.Script.Utility;
using UnityEngine;

namespace Assets.Script.Boss.State
{
    internal class BossMaskulinState : IState
    {
        private BossStateControl _bossStateControl;
        private IBossState bossState;
        public BossMaskulinState(BossStateControl bossStateControl)
        {
            _bossStateControl = bossStateControl;
            bossState = bossStateControl;
        }

        public void OnEnter()
        {
            Helper.Log("Maskulin enter");
            _bossStateControl.StartCoroutine(Helper.delay(
                () =>
                {
                    bossState.Cooldown();
                },1));
        }

        public void OnExit()
        {
            Helper.Log("Maskulin Exit");
        }

        public void Tick()
        {
            
        }


    }
}
