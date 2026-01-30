using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    enum Action {Idle, Attack, Defense, Parry}
    public TextMeshProUGUI resultText;
    Action player1 = Action.Idle;
    Action player2 = Action.Idle;
    float parryTime = 0.5f;
    float p1DefenseTime;
    float p2DefenseTime;
    

    // Update is called once per frame
    void Update()
    {
        if (player1 == Action.Idle)
        {
            if (Input.GetKeyDown(KeyCode.S))
            player1 = Action.Attack;
            if (Input.GetKeyDown(KeyCode.D))
            player1 = Action.Defense;
            p1DefenseTime = Time.time;
        }

        if (player2 == Action.Idle)
        {
            if (Input.GetKeyDown(KeyCode.J))
            player2 = Action.Attack;
            if (Input.GetKeyDown(KeyCode.K))
            player2 = Action.Defense;
            p2DefenseTime = Time.time;
        }

        if (player1 != Action.Idle && player2 != Action.Idle)
        {
            Battle();
        }
        
    }

    void Battle()
    {
        DefenseParry();
        if (player1 == Action.Attack && player2 == Action.Attack)
        {
            resultText.text = "attack";
        }
        else if (player1 == Action.Parry && player2 == Action.Parry)
        {
            resultText.text = "parry";
        }
        else if (player1 == Action.Defense && player2 == Action.Defense)
        {
            resultText.text = "defense";
        }
        else
        {
            resultText.text = "miss";
        }
        Invoke(nameof(ResetRound), 1.5f);
    }

    void DefenseParry()
    {
        if (player1 == Action.Defense && player2 == Action.Defense)
        {
            float timeDiff = Mathf.Abs(p1DefenseTime - p2DefenseTime);

            if (timeDiff <= parryTime)
            {
                player1 = Action.Parry;
                player2 = Action.Parry;
            }
        }
    }

    void ResetRound()
    {
        player1 = Action.Idle;
        player2 = Action.Idle;
        resultText.text = "test:";
    }
}
