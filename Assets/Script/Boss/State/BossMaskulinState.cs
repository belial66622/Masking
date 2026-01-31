using Assets.Script.Interface;
using Assets.Script.Utility;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Script.Boss.State
{
    internal class BossMaskulinState : IState
    {
        private BossStateControl _bossStateControl;
        private IBossState bossState;
        private IAttack _attack;
        private Animator _animator;
        public BossMaskulinState(BossStateControl bossStateControl, IAttack attack, Animator animator)
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
            Helper.Log("Maskulin enter");
            
            _animator.SetTrigger(BossStateControl.MASKULIN);
        }

        public void OnExit()
        {
            bossState.DoneAttack -= AttackFinished;
            bossState.OnAttack -= Attack;
            Helper.Log("Maskulin Exit");
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
