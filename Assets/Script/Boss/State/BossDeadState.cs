using System.Diagnostics;
using Assets.Script.Utility;

namespace Assets.Script.Boss.State
{
    internal class BossDeadState : IState
    {
        private BossStateControl _bossStateControl;
        public void OnEnter()
        {
            Helper.Log("dead");
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            
        }

        public BossDeadState(BossStateControl bossStateControl)
        {
            _bossStateControl = bossStateControl;
        }
    }
}
