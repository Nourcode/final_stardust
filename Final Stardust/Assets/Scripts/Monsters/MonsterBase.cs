using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Monster/Create new monster")]
public class MonsterBase : ScriptableObject
{
   [SerializeField] string name;
   [SerializeField] string description;

   [SerializeField] Sprite frontSprite;
   [SerializeField] Sprite backSprite;

    [SerializeField] MonsterType type1;

    // Base Stats
    [SerializeField] int maxHP;

    [SerializeField] int maxMP;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;


    public string Name {
        get { return name;}
    }

    public string Description {
        get { return description;}
    }

    public Sprite FrontSprite {
        get { return frontSprite;}
    }

    public Sprite BackSprite {
        get { return backSprite;}
    }

    public MonsterType Type1 {
        get { return type1;}
    }

    public int MaxHP {
        get { return maxHP;}
    }

    public int MaxMP {
        get { return maxMP;}
    }

    public int Attack {
        get { return attack;}
    }

    public int Defense {
        get { return defense;}
    }

    public int SpAttack {
        get { return spAttack;}
    }

    public int SpDefense {
        get { return spDefense;}
    }

    public int Speed {
        get { return speed;}
    }

}

public enum MonsterType
{
    None,
    Venus,
    Mercury,
    Mars,
    Jupiter
    
}
