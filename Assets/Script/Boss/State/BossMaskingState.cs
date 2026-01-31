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
        private Animator _animatorhead;
        public BossMaskingState(BossStateControl bossStateControl, IAttack attack, Animator animator, Animator head)
        {
            _bossStateControl = bossStateControl;
            bossState = bossStateControl;
            _attack = attack;
            _animator = animator;
            _animatorhead = head;
        }

        public void OnEnter()
        {
            bossState.DoneAttack += AttackFinished;
            bossState.OnAttack += Attack;
            Helper.Log("Masking enter");
            _animatorhead.SetInteger(BossStateControl.FACEID, 1);
            _bossStateControl.StartCoroutine(Helper.delay(
                () => { _animator.SetTrigger(BossStateControl.MASKING); },1));
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
