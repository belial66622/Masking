using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Script.Boss;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Script.Projectile
{
    internal class MaskingProj : BaseProjectile
    {
        [SerializeField] private float forwardSpeed;
        Vector3 _target = Vector3.zero; [SerializeField]
        private BossStateControl _boss;
        [SerializeField] private float _damage;
        private bool canMove = false;
        [SerializeField]ProjAnim _projAnim;

        private void OnEnable()
        {
            _projAnim.OnAnim += CanMove;
            _projAnim.OnSound += Sound;
        }

        private void OnDisable()
        {
            _projAnim.OnAnim -= CanMove;
            _projAnim.OnSound += Sound;
        }

        public override void SetProjectile(Vector3 position, Quaternion angle, Vector3 target, BossStateControl boss)
        {
            transform.position = position;
            transform.rotation = Quaternion.identity;
            _target = target;
            _boss = boss;
        }

        private void Update()
        {
            if (!canMove) { return; }
            //transform.position += transform.forward * Time.deltaTime * forwardSpeed;
            transform.position = Vector3.MoveTowards(transform.position, _target, forwardSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _target) < .1f)
            {
                _boss.attack(_damage);
                Destroy(gameObject);
            }
        }

        public void CanMove()
        {
            canMove = true;
        }

        public void Sound()
        {
            SoundPlay.Instance.PlaySound("Masking");
        }
    }
}
