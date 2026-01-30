using Assets.Script.Interface;
using Assets.Script.Utility;

namespace Assets.Script.Boss.State
{
    internal class BossMaskerMode : IState
    {
        private BossStateControl _bossStateControl;
        private IBossState bossState;
        public BossMaskerMode(BossStateControl bossStateControl)
        {
            _bossStateControl = bossStateControl;
            bossState = bossStateControl;
        }

        public void OnEnter()
        {
            Helper.Log("Masker enter");
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
