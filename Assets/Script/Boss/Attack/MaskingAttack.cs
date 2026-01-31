using System.Collections;
using Assets.Script.Interface;
using Assets.Script.Projectile;
using Assets.Script.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Boss.Attack
{
    public class MaskingAttack : BaseAttack, IAttack
    {
        [SerializeField] Image image;
        [SerializeField] float intensity;
        [SerializeField] float flashDur;
        public void Attack()
        {
            Helper.Log("maskerAttack");
            StartCoroutine(flash(flashDur));
            _bossStateControl.attack();
        }


        IEnumerator flash(float flashDuration)
        {
            float timer = 0f;
            while (timer < flashDuration)
            {
                yield return null;
                timer += Time.deltaTime;
                float alpha = Mathf.Lerp(0, 1, timer / flashDuration);
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                yield return null;
            }

            // Fade out
            timer = 0f;
            while (timer < flashDuration)
            {
                yield return null;
                timer += Time.deltaTime;
                float alpha = Mathf.Lerp(intensity, 0, timer / flashDuration);
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                yield return null;
            }

            image.color = new Color(image.color.r, image.color.g, image.color.b, 0); // Ensure it's fully transparent at the end

        }
    }
}
