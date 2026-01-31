using Assets.Script.Interface;
using Assets.Script.Utility;
using UnityEditor;

namespace Assets.Script.Boss.Attack
{
    public class MaskerAttack : BaseAttack, IAttack
    {
        public void Attack()
        {
            Helper.Log("maskerAttack");
        }
    }
}
