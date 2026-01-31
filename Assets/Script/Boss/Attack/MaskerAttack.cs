using Assets.Script.Interface;
using Assets.Script.Projectile;
using Assets.Script.Utility;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.Boss.Attack
{
    public class MaskerAttack : BaseAttack, IAttack
    {
        [SerializeField]
        private Ingus Ingus;
        [SerializeField]
        BossStateControl _boss;

        public void Attack()
        {
            Helper.Log("maskerAttack"); 
            var pro = Instantiate(Ingus);
            pro.SetProjectile(_bossStateControl.GetMulut().position, AddOffset(_bossStateControl.GetMulut().rotation), _bossStateControl.GetPlayer().position, _bossStateControl);

        }

        private Quaternion AddOffset(Quaternion quaternion)
        {
            return quaternion * Quaternion.Euler(Random.Range(-60f, 60f), Random.Range(-60f, 60f), 0);
        }
    }
}
