using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Monster/Create new move")]
public class MoveBase : ScriptableObject 
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;
    [SerializeField] MonsterType type;
    [SerializeField] int power; 
    [SerializeField] int accuracy;   
    [SerializeField] int mp;

    public string Name {
        get { return name; }
    }

    public string Description {
        get { return description; }
    }

    public MonsterType Type {
        get { return type; }
    }

    public int Power {
        get { return power;}
    }

    public int Accuracy {
        get { return accuracy; }
    }

    public int MP {
        get { return mp; }
    }

}
