using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Script.Boss.Health;
using Assets.Script;
using Assets.Script.Utility;

public class GameManagerScript : MonoBehaviour
{
    enum Action {Idle, Attack, Defense, Parry}
    enum Result {Attack, Defense, Parry, Miss, PerfectAttack}
    enum Block {Normal, Perfect, None}
    Action player1 = Action.Idle;
    Action player2 = Action.Idle;
    public float parryTime = 0.2f;
    float perfectWindow = 0.2f;
    float perfectBlockWindow = 0.2f;
    float p1DefenseTime;
    float p2DefenseTime;
    public VFXScript VFX;
    bool roundResolved = false;
    float p1ActionTime;
    float p2ActionTime;
    public BossHealth Boss;
    public heallthDummy playerHealth;
    float bossAttackTime;
    bool bossIsAttacking;
    public float bossWindupTime = 1f;

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

            Helper.Log($"ada result {result.ToString()}");
            PlayBattleVFX(result);
            Invoke(nameof(ResetRound), 1f);
        }
        
    }

    Result Battle()
    {
        float timeDiff = Mathf.Abs(p1ActionTime - p2ActionTime);

        if (timeDiff > connectWindow)
            return Result.Miss;
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
        else if (player1 == Action.Defense && player2 == Action.Defense)
        {
            if (IsParryTiming())
            {
                playerHealth.SetBlock(heallthDummy.BlockType.Perfect);
                playerHealth.GrantShield();
                ParryAttack();
                return Result.Parry;
            }
            if (timeDiff <= perfectBlockWindow)
            {
                playerHealth.SetBlock(heallthDummy.BlockType.Perfect);
                // playerHealth.GrantShield();
                return Result.Defense;
                
            }
            else 
            {
                playerHealth.SetBlock(heallthDummy.BlockType.Normal);
                return Result.Defense;
            }
        }
        else
        {
            return Result.Miss;
        }
    }

    bool IsParryTiming()
    {
        if (!bossIsAttacking)
            return false;

        float p1Diff = Mathf.Abs(p1ActionTime - bossAttackTime);
        float p2Diff = Mathf.Abs(p2ActionTime - bossAttackTime);

        return p1Diff <= parryTime && p2Diff <= parryTime;
    }

    bool DefenseTiming()
    {
        if (!bossIsAttacking)
            return false;

        float d1 = bossAttackTime - p1ActionTime; // positive if player pressed before attack
        float d2 = bossAttackTime - p2ActionTime;

        // must be pressed before the attack, but within the perfectBlockWindow
        return d1 >= 0f && d1 <= perfectBlockWindow &&
            d2 >= 0f && d2 <= perfectBlockWindow;
    }
    

    void AttackBoss()
    {
        if (Boss != null)
        {
            Boss.OnDamage(5);
            //Boss.OnPoiseDamage(5f); 
        }
    }

    void PerfectAttackBoss()
    {
        if (Boss != null)
        {
            Boss.OnDamage(10);
            //Boss.OnPoiseDamage(10f); 
        }
    }

    void ParryAttack()
    {
        if (Boss != null)
        {
            Boss.OnDamage(30);
            Boss.OnPoiseDamage(15f); 
        }
    }

    public void BossAttack()
    {
        bossAttackTime = Time.time;
        bossIsAttacking = true;

        Invoke(nameof(EndBossAttack), 0.2f);
    }

    void EndBossAttack()
    {
        bossIsAttacking = false;
    }

    void PlayBattleVFX(Result result)
    {
        switch (result)
        {
            case Result.PerfectAttack:
                VFX.PlayAttack();
                    break;
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
    

    void ResetRound()
    {
        roundResolved = false;
        player1 = Action.Idle;
        player2 = Action.Idle;
        playerHealth.SetBlock(heallthDummy.BlockType.None);
        bossIsAttacking = false;
    }
}
