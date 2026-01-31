using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Script.Boss.Health;

public class GameManagerScript : MonoBehaviour
{
    enum Action {Idle, Attack, Defense, Parry}
    enum Result {Attack, Defense, Parry, Miss, PerfectAttack}
    Action player1 = Action.Idle;
    Action player2 = Action.Idle;
    float parryTime = 0.2f;
    float perfectWindow = 0.2f;
    float p1DefenseTime;
    float p2DefenseTime;
    public VFXScript VFX;
    bool roundResolved = false;
    float p1ActionTime;
    float p2ActionTime;
    public BossHealth Boss;
    

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
            if (timeDiff <= perfectWindow)  
            {
                PerfectAttackBoss();
                return Result.PerfectAttack; 
            }
            else
            {
                AttackBoss();
                return Result.Attack;
            }
        }
        else if (player1 == Action.Parry && player2 == Action.Parry)
        {
            return Result.Parry;
        }
        else if (player1 == Action.Defense && player2 == Action.Defense)
        {
            return Result.Defense;
        }
        else
        {
            return Result.Miss;
        }
    }

    void AttackBoss()
    {
        if (Boss != null)
        {
            Boss.OnDamage(5);
            Boss.OnPoiseDamage(5f); 
        }
    }

    void PerfectAttackBoss()
    {
        if (Boss != null)
        {
            Boss.OnDamage(10);
            Boss.OnPoiseDamage(10f); 
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
    }
}
