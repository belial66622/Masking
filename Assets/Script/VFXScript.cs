using System.Collections;
using System.Collections.Generic;
using Assets.Script;
using Assets.Script.Utility;
using UnityEngine;

public class VFXScript : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start() {
       // GetComponent<SpriteRenderer>().enabled = false;
    }
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAttack()
    {
        //GetComponent<SpriteRenderer>().enabled = true;

        anim.ResetTrigger("Attack");
        anim.SetTrigger("Attack");
        SoundPlay.Instance.PlaySound("Attack");
    }

    public void PlayDefense()
    {
       // GetComponent<SpriteRenderer>().enabled = true;
        anim.ResetTrigger("Defense");
        anim.SetTrigger("Defense");
        SoundPlay.Instance.PlaySound("Defense");
    }

    public void PlayParry()
    {
       // GetComponent<SpriteRenderer>().enabled = true;
        anim.ResetTrigger("Parry");
        anim.SetTrigger("Parry");
        SoundPlay.Instance.PlaySound("Parry");
    }

    // Update is called once per frame
    
}
