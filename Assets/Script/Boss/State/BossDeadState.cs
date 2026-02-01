using System.Diagnostics;
using Assets.Script.Utility;
using UnityEngine;

namespace Assets.Script.Boss.State
{
    internal class BossDeadState : IState
    {
        private BossStateControl _bossStateControl;
        private Animator _animator;
        public void OnEnter()
        {
            Helper.Log("dead"); 
            SoundPlay.Instance.PlaySound("Death");
            _animator.SetTrigger(BossStateControl.DEATH);
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            
        }

        public BossDeadState(BossStateControl bossStateControl,Animator animator)
        {
            _bossStateControl = bossStateControl;
            _animator = animator;
        }
    }
}
