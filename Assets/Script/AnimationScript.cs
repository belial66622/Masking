using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Animator animate;
    Animator anim;
    public KeyCode attackKey;
    public KeyCode defenseKey;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(attackKey))
        anim.SetTrigger("attack");

        if (Input.GetKeyDown(defenseKey))
        anim.SetTrigger("defense");
    }
}
