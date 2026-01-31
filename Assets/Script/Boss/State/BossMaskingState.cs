using Assets.Script.Interface;
using Assets.Script.Utility;

namespace Assets.Script.Boss.State
{
    internal class BossMaskingState : IState
    {
        private BossStateControl _bossStateControl;
        private IBossState bossState;
        private IAttack _attack;
        public BossMaskingState(BossStateControl bossStateControl, IAttack attack)
        {
            _bossStateControl = bossStateControl;
            bossState = bossStateControl;
            _attack = attack;
        }

        public void OnEnter()
        {
            Helper.Log("Masking enter"); _attack.Attack();
            _bossStateControl.StartCoroutine(Helper.delay(
                () =>
                {
                    bossState.Cooldown();
                },1));
        }

        public void OnExit()
        {
            Helper.Log("Masking Exit");
        }

        public void Tick()
        {
            
        }

    }
}
