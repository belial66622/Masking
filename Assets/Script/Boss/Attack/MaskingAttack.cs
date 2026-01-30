using Assets.Script.Interface;
using Assets.Script.Projectile;
using Assets.Script.Utility;
using UnityEngine;

namespace Assets.Script.Boss.Attack
{
    public class MaskingAttack : BaseAttack, IAttack
    {
        [SerializeField]
        private Ingus Ingus;

        public void Attack()
        {
            var pro = Instantiate(Ingus);
            pro.SetProjectile(_bossStateControl.GetMulut().position, AddOffset(_bossStateControl.GetMulut().rotation), _bossStateControl.GetPlayer().position);
        }

        private Quaternion AddOffset(Quaternion quaternion)
        {
            return quaternion * Quaternion.Euler(Random.Range(-60f, 60f), Random.Range(-60f, 60f), 0);
        }
    }
}
