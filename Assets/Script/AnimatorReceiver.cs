using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Script.Boss;
using UnityEngine;

namespace Assets.Script
{
    internal class AnimatorReceiver : MonoBehaviour
    {
        [SerializeField]
        BossStateControl bossStateControl;

        public void EndAttack()
        {
            bossStateControl?.AttackAnimationFinished();
        }

        public void Attack()
        {
            bossStateControl?.ActivateAttack();
        }

        public void AdditionalSound()
        {
            bossStateControl?.ActivateSound();
        }

        public void AdditionalAnimation()
        {
            bossStateControl?.ActivateAnimation();
        }

        public void Death()
        { 
            bossStateControl?.Death();
        }
    }
}
