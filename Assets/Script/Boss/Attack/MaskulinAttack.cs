using Assets.Script.Interface;
using Assets.Script.Utility;
using UnityEngine;

namespace Assets.Script.Boss.Attack
{
    public class MaskulinAttack : BaseAttack, IAttack
    {
        [SerializeField]
        private float _cameraShakeDuration;
        [SerializeField]
        private float _cameraMagnitude;
        public void Attack()
        {
            Camera.main.GetComponent<IShake>().ShakeCamera(_cameraShakeDuration, _cameraMagnitude);
            Helper.Log("MaskulinAttack");
        }
    }
}
