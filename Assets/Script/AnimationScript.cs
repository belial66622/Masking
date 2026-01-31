using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Animator animate;
    Animator anim;
    public KeyCode attackKey;
    public KeyCode defenseKey;
    Vector3 startPos;
    public float attackOffsetX;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(attackKey))
        anim.SetTrigger("attack");

        if (Input.GetKeyDown(defenseKey))
        anim.SetTrigger("defense");
    }

    public void Adjustment()
    {
        StartCoroutine(MoveAndReset());
    }

    // Update is called once per frame
    IEnumerator MoveAndReset()
    {
        transform.position += new Vector3(attackOffsetX, 0f, 0f);

        yield return new WaitForSeconds(0.2f);

        transform.position = startPos;
    }
}
