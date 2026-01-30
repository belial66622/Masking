using Assets.Script.Interface;
using Assets.Script.Utility;

namespace Assets.Script.Boss.State
{
    internal class BossMaskingState : IState
    {
        private BossStateControl _bossStateControl;
        private IBossState bossState;
        public BossMaskingState(BossStateControl bossStateControl)
        {
            _bossStateControl = bossStateControl;
            bossState = bossStateControl;
        }

        public void OnEnter()
        {
            Helper.Log("Masking enter");
            _bossStateControl.StartCoroutine(Helper.delay(
                () =>
                {
                    bossState.Cooldown();
                },1));
        }

        public void OnExit()
        {
            Helper.Log("Msaking Exit");
        }

        public void Tick()
        {
            
        }

    }
}
