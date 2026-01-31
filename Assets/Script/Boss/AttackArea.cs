using UnityEngine;

namespace Assets.Script.Boss
{
    internal class AttackArea: MonoBehaviour
    {
        [SerializeField]
        Transform player;

        [SerializeField]
        LayerMask mask;

        public bool Attack(out Collider col)
        {
            col = Physics.OverlapSphere(player.position, 1, mask)[0];
            return col != null;
        }
    }
}
