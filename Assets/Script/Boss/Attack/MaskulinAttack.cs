using Assets.Script.Interface;
using Assets.Script.Utility;

namespace Assets.Script.Boss.Attack
{
    public class MaskulinAttack : BaseAttack, IAttack
    {
        public void Attack()
        {
            Helper.Log("MaskulinAttack");
        }
    }
}
