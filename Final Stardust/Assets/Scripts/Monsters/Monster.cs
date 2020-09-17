using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster
{
    MonsterBase _base;
    int level;

    public Monster(MonsterBase mBase, int mLevel)
    {
        _base = mBase;
        level = mLevel;
    }

    public int Attack {
        get { return Mathf.FloorToInt((_base.Attack * level) / 100f) + 5;}
    }

    public int Defense {
        get { return Mathf.FloorToInt((_base.Defense * level) / 100f) + 5;}
    }
    public int SpAttack {
        get { return Mathf.FloorToInt((_base.SpAttack * level) / 100f) + 5;}
    }
    public int SpDefense {
        get { return Mathf.FloorToInt((_base.SpDefense * level) / 100f) + 5;}
    }
    public int Speed {
        get { return Mathf.FloorToInt((_base.Speed * level) / 100f) + 5;}
    }

    public int MaxHP {
        get { return Mathf.FloorToInt((_base.MaxHP * level) / 100f) + 10;}
    }

    public int MaxMP {
        get { return Mathf.FloorToInt((_base.MaxMP * level) / 100f) + 10;}
    }
}
