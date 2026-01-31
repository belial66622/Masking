using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXScript : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAttack()
    {
        anim.SetTrigger("Attack");
    }

    public void PlayDefense()
    {
        anim.SetTrigger("Defense");
    }

    public void PlayParry()
    {
        anim.SetTrigger("Parry");
    }

    // Update is called once per frame
    
}
