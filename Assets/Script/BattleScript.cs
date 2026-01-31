using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float Move = 1f;
    public Transform mask1;
    public Transform mask2;
    Vector3 mask1Start;
    Vector3 mask2Start;
    bool maskMoving1 = false;
    bool maskMoving2 = false;

    void Start()
    {
        mask1Start = transform.position;
        mask2Start = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (!maskMoving1)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(MoveAndReset(mask1, Vector3.right, 1));
            }
        }
        if (!maskMoving2)
        {
            if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.J))
            {
                StartCoroutine(MoveAndReset(mask2, Vector3.left, 2));
            }
        }
    }

    IEnumerator MoveAndReset(Transform mask, Vector3 direction, int player)
    {
        if (player == 1) maskMoving1 = true;
        if (player == 2) maskMoving2 = true;

        Vector3 startPos = mask.position;

        // Move immediately
        mask.position += direction * Move;

        // Wait 1 second
        yield return new WaitForSeconds(1f);

        // Reset position
        mask.position = startPos;

        if (player == 1) maskMoving1 = false;
        if (player == 2) maskMoving2 = false;
    }
}
