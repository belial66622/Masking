using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Interface
{
    internal interface IBossState
    {
        public event Action DoneAttack;

        public event Action OnAttack;
        public void Dead();
        public void Cooldown();
        public void Stun();
        public void ClearStun();
        public void ChooseAttack();
        public void CanAttack();
    }
}
