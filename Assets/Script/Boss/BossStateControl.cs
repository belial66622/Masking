using System;
using Assets.Script.Boss.State;
using Assets.Script.Interface;
using Script.Boss.Health;
using UnityEngine;

namespace Assets.Script.Boss
{
    [DefaultExecutionOrder(-999)]
    public class BossStateControl : MonoBehaviour, IBossState
    {
        private StateMachine _stateMachine;

        [SerializeField] GameObject _player;

        [SerializeField] private BossHealth health;

        [SerializeField] private AttackState _attackingState;

        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _stunTime;

        private bool _isDead = false;
        private bool _isStunned;
        private bool _isCooldownAttack = true;

        private void Update() => _stateMachine.Tick();
        // Update is called once per frame
        void Awake()
        {
            health.EventDelete();
            _stateMachine = new StateMachine();

            var deadState = new BossDeadState(this);
            var MaskerState = new BossMaskerMode(this);
            var MaskulinState = new BossMaskulinState(this);
            var MaskingState = new BossMaskingState(this);
            var stunState = new BossStunState(this, _stunTime);
            var idleState = new BossIdleState(this,health, _attackCooldown);

            //idle to skillstate
            At(idleState, MaskerState, CanAttackMasker());
            At(idleState, MaskulinState, CanAttackMaskulin());
            At(idleState, MaskingState, CanAttackMasking());

            //stun to idle
            At(stunState, idleState, StunFinish());

            //to stun state
            At(idleState, stunState, IsStunned());
            At(MaskerState, stunState, IsStunned());
            At(MaskulinState, stunState, IsStunned());
            At(MaskingState, stunState, IsStunned());

            //skill state to idle
            At(MaskerState, idleState, IsIdle());
            At(MaskulinState, idleState, IsIdle());
            At(MaskingState, idleState, IsIdle());

            // ant state to die
            AtAny(deadState, IsDead());

            health.SetHealth();
            health.SetPoise();
            _stateMachine.SetState(idleState);

            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            void AtAny(IState From, Func<bool> condition) => _stateMachine.AddAnyTransition(From, condition);

            Func<bool> CanAttackMasker() => () => !_isCooldownAttack && _attackingState == AttackState.Masker;
            Func<bool> CanAttackMasking() => () => !_isCooldownAttack && _attackingState == AttackState.Masking;
            Func<bool> CanAttackMaskulin() => () => !_isCooldownAttack && _attackingState == AttackState.Maskulin;
            Func<bool> IsStunned() => () => _isStunned;
            Func<bool> StunFinish() => () => !_isStunned;
            Func<bool> IsIdle() => () => _isCooldownAttack;
            Func<bool> IsDead() => () => _isDead;


        }

        private void Start()
        {
            health.OnPoiseDepleted += Stun;
            health.OnDeath += Dead;
        }

        private void OnDestroy()
        {
            health.OnPoiseDepleted -= Stun;
            health.OnDeath -= Dead;
        }
        #region IBossState
        public void CanAttack()
        {
            _isCooldownAttack = false;
        }

        public void ChooseAttack()
        {
            var t = Enum.GetNames(typeof(AttackState)).Length;
            _attackingState =  (AttackState)UnityEngine.Random.Range(0, t);
        }

        public void ClearStun()
        {
           _isStunned = false;
        }

        public void Cooldown()
        {
            _isCooldownAttack = true;
        }

        public void Dead()
        {
            _isDead = true;
        }

        public void Stun()
        {
           _isStunned = true;
        }
        #endregion

        public enum BossState
        {
            Masking,
            Masker,
            Maskulin,
            DeadState,
            Stun
        }

        public enum AttackState
        {
            Masking,
            Masker,
            Maskulin
        }
    }
}
