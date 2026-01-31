using Assets.Script.Interface;
using Assets.Script.Utility;

namespace Assets.Script.Boss.State
{
    internal class BossMaskerState : IState
    {
        private BossStateControl _bossStateControl;
        private IBossState bossState;
        private IAttack _attack;
        public BossMaskerState(BossStateControl bossStateControl , IAttack attack)
        {
            _bossStateControl = bossStateControl;
            bossState = bossStateControl;
            _attack = attack;
        }

        public void OnEnter()
        {
            Helper.Log("Masker enter"); 
            _attack.Attack();
            _bossStateControl.StartCoroutine(Helper.delay(
                () =>
                {
                    bossState.Cooldown();
                }, 1));
        }

        public void OnExit()
        {
            Helper.Log("Masker Exit");
        }

        public void Tick()
        {
            
        }

    }
}
