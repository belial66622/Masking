using Assets.Script.Interface;
using Assets.Script.Projectile;
using Assets.Script.Utility;
using UnityEngine;

namespace Assets.Script.Boss.Attack
{
    public class MaskingAttack : BaseAttack, IAttack
    {


        public void Attack()
        {
            Helper.Log("maskerAttack");
        }

    }
}
