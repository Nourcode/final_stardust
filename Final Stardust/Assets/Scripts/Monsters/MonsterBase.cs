using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Monster/Create new monster")]
public class MonsterBase : ScriptableObject
{
   [SerializeField] string name;

   [TextArea]
   [SerializeField] string description;

   [SerializeField] Sprite frontSprite;
   [SerializeField] Sprite backSprite;

    [SerializeField] MonsterType type1;
    [SerializeField] MonsterType type2;

    // Base Stats
    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;



}

public enum MonsterType
{
    None,
    Venus,
    Mercury,
    Mars,
    Jupiter
    
}
