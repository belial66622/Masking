using Assets.Script.Interface;
using Assets.Script.Utility;
using UnityEngine;

namespace Assets.Script.Boss.State
{
    internal class BossMaskingState : IState
    {
        private BossStateControl _bossStateControl;
        private IBossState bossState;
        private IAttack _attack;
        private Animator _animator;
        public BossMaskingState(BossStateControl bossStateControl, IAttack attack, Animator animator)
        {
            _bossStateControl = bossStateControl;
            bossState = bossStateControl;
            _attack = attack;
            _animator = animator;
        }

        public void OnEnter()
        {
            bossState.DoneAttack += AttackFinished;
            bossState.OnAttack += AttackFinished;
            Helper.Log("Masking enter");
            _animator.SetTrigger(BossStateControl.MASKING);
        }

        public void OnExit()
        {
            bossState.DoneAttack -= AttackFinished;
            bossState.OnAttack -= Attack;
            Helper.Log("Masking Exit");
        }

        public void Tick()
        {
            
        }

        private void AttackFinished()
        {
            bossState.Cooldown();
        }
        private void Attack()
        {
            _attack.Attack();
        }
    }
}
