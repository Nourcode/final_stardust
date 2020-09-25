using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster
{
    public MonsterBase Base { get; set; }
    public int Level { get; set; }

    public int HP { get; set; }

    public int MP { get; set; }

    public List<Move> Moves { get; set; }

    public Monster(MonsterBase mBase, int mLevel)
    {
        Base = mBase;
        Level = mLevel;
        HP = MaxHP;
        MP = MaxMP;

        // Generate Moves
        Moves = new List<Move>();
        foreach (var move in Base.LearnableMoves)
        {
            if(move.Level <= Level)
            {
                Moves.Add(new Move(move.Base));
            }

            if(Moves.Count >= 4){
                break;
            }
        }
    }

    public int Attack {
        get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5;}
    }

    public int Defense {
        get { return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5;}
    }
    public int SpAttack {
        get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5;}
    }
    public int SpDefense {
        get { return Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5;}
    }
    public int Speed {
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5;}
    }

    public int MaxHP {
        get { return Mathf.FloorToInt((Base.MaxHP * Level) / 100f) + 10;}
    }

    public int MaxMP {
        get { return Mathf.FloorToInt((Base.MaxMP * Level) / 100f) + 10;}
    }

    public bool TakeDamage(Move move, Monster attacker)
    {
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attacker.Attack / Defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            return true;
        }

        return false;
    }

    public void LoseMP(Move move)
    {
        MP -= move.Base.MP;
    }

    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }

    public void DoubleAttack(Monster noob)
    {
        //noob.Attack = (float)noob.Attack) * 2; 
    }


}
