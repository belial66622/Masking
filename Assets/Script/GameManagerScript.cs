using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    enum Action {Idle, Attack, Defense, Parry}
    enum Result {Attack, Defense, Parry, Miss}
    public TextMeshProUGUI resultText;
    Action player1 = Action.Idle;
    Action player2 = Action.Idle;
    float parryTime = 0.2f;
    float p1DefenseTime;
    float p2DefenseTime;
    public VFXScript VFX;
    bool roundResolved = false;
    float p1ActionTime;
    float p2ActionTime;

    float connectWindow = 0.5f;
        

    // Update is called once per frame
    void Update()
    {
        if (player1 == Action.Idle)
        {
            if (Input.GetKeyDown(KeyCode.S))
            player1 = Action.Attack;
            p1ActionTime = Time.time;
            if (Input.GetKeyDown(KeyCode.D))
            {
                player1 = Action.Defense;
                p1DefenseTime = Time.time;
                p1ActionTime = Time.time;
            }
        }

        if (player2 == Action.Idle)
        {
            if (Input.GetKeyDown(KeyCode.J))
            player2 = Action.Attack;
            p2ActionTime = Time.time;
            if (Input.GetKeyDown(KeyCode.K))
            {
                player2 = Action.Defense;
                p2DefenseTime = Time.time;
                p2ActionTime = Time.time;
            }
        }

        if (!roundResolved && player1 != Action.Idle && player2 != Action.Idle)
        {
            roundResolved = true;
            Result result = Battle();
            PlayBattleVFX(result);
            Invoke(nameof(ResetRound), 1f);
        }
        
    }

    Result Battle()
    {
        float timeDiff = Mathf.Abs(p1ActionTime - p2ActionTime);

        if (timeDiff > connectWindow)
            return Result.Miss;
        DefenseParry();
        if (player1 == Action.Attack && player2 == Action.Attack)
        {
            resultText.text = "attack";
            return Result.Attack;
        }
        else if (player1 == Action.Parry && player2 == Action.Parry)
        {
            resultText.text = "parry";
            return Result.Parry;
        }
        else if (player1 == Action.Defense && player2 == Action.Defense)
        {
            resultText.text = "defense";
            return Result.Defense;
        }
        else
        {
            resultText.text = "miss";
            return Result.Miss;
        }
    }

    void PlayBattleVFX(Result result)
    {
        switch (result)
        {
            case Result.Attack:
                VFX.PlayAttack();
                break;

            case Result.Parry:
                VFX.PlayParry();
                break;

            case Result.Defense:
                VFX.PlayDefense();
                break;
        }
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
        roundResolved = false;
        player1 = Action.Idle;
        player2 = Action.Idle;
        resultText.text = "test:";
    }
}
