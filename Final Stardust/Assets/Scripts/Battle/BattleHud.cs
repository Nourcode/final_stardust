using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;
    [SerializeField] MPBar mpBar;

    public void SetData(Monster monster)
    {
       nameText.text = monster.Base.Name;
       levelText.text = "Lvl." + monster.Level;
       hpBar.SetHP((float)(monster.HP / monster.MaxHP));
       mpBar.SetMP((float)(monster.MP / monster.MaxMP));
    }
}
