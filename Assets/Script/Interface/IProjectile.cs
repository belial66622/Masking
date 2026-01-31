

using Assets.Script.Boss;
using UnityEngine;

namespace Assets.Script.Utility
{
    internal interface IProjectile
    {
        public void SetProjectile(Vector3 position, Quaternion angle, Vector3 target, BossStateControl boss);
    }
}
