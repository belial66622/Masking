using Assets.Script.Utility;
using UnityEngine;

namespace Assets.Script.Projectile
{
    public abstract class BaseProjectile : MonoBehaviour, IProjectile
    {
        public abstract void SetProjectile(Vector3 position, Quaternion angle, Vector3 target);
    }
}
