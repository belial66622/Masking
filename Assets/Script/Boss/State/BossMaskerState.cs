using Assets.Script.Interface;
using Assets.Script.Utility;
using UnityEngine;

namespace Assets.Script.Boss.State
{
    internal class BossMaskerState : IState
    {
        private BossStateControl _bossStateControl;
        private IBossState bossState;
        private IAttack _attack;
        private Animator _animator;
        public BossMaskerState(BossStateControl bossStateControl , IAttack attack, Animator animator, Animator head)
        {
            _bossStateControl = bossStateControl;
            bossState = bossStateControl;
            _attack = attack;
            _animator = animator;
        }

        public void OnEnter()
        {
            bossState.DoneAttack += AttackFinished;
            bossState.OnAttack += Attack;
            Helper.Log("Masker enter"); 

            _animator.SetTrigger(BossStateControl.MASKER);
        }

        public void OnExit()
        {
            bossState.DoneAttack -= AttackFinished;
            bossState.OnAttack -= Attack;
            Helper.Log("Masker Exit");
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
