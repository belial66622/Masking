using Assets.Script.Boss;
using Assets.Script.Utility;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Script.Projectile
{
    internal class Ingus : BaseProjectile
    {
        [SerializeField] private float rotationspeed;
        [SerializeField] private float forwardSpeed;
        Vector3 _target = Vector3.zero; [SerializeField]
        private BossStateControl _boss;
        [SerializeField]private float _damage;

        float MaxRotationSpeed;

        public override void SetProjectile(Vector3 position, Quaternion angle, Vector3 target, BossStateControl boss)
        {
            transform.position = position;
            transform.rotation = angle;
            _target = target;
            _boss = boss;
        }

        private void Update()
        {
            transform.position += transform.forward * Time.deltaTime * forwardSpeed;

            Rotate();

            if (Vector3.Distance(transform.position, _target) < .5f)
            {
                _boss.attack(_damage);
                Destroy(gameObject);
            }
        }

        private void Rotate()
        {
            if (_target == null) return;

            Vector3 toTarget = _target - transform.position;

            if (toTarget.sqrMagnitude < 0.0001f)
                return;

            // Desired rotation (tracks ALL axes)
            Quaternion targetRotation = Quaternion.LookRotation(toTarget);

            // Distance-based turn speed (closer = faster)
            float distance = toTarget.magnitude;
            Helper.Log($"dis {distance}");
            float t = Mathf.InverseLerp(15, 0f, distance);
            float turnSpeed = Mathf.Lerp(rotationspeed, rotationspeed * 7, t);

            // Smooth rotation only (no movement influence)
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                turnSpeed * Time.deltaTime
            );
        }
    }
}
