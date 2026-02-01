using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXScript : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start() {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAttack()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        anim.ResetTrigger("Attack");
        anim.SetTrigger("Attack");
    }

    public void PlayDefense()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        anim.ResetTrigger("Defense");
        anim.SetTrigger("Defense");
    }

    public void PlayParry()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        anim.ResetTrigger("Parry");
        anim.SetTrigger("Parry");
    }

    // Update is called once per frame
    
}
