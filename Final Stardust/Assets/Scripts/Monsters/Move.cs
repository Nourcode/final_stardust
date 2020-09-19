using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move 
{
    public MoveBase Base { get; set; }
    public int MP { get; set;}

    public Move(MoveBase mBase)
    {
        Base = mBase;
        MP = mBase.MP;
    }
}
