using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy}
public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    //[SerializeField] Image test;
 
    BattleState state;
    int currentAction;

    private void Start() {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.Monster);
        enemyHud.SetData(enemyUnit.Monster);

        yield return dialogBox.TypeDialog("Mirashi challenged you to a battle!");
        yield return new WaitForSeconds(1.5f);
        yield return dialogBox.TypeDialog("*You draw a card*");
        yield return new WaitForSeconds(1.5f);

        PlayerAction();
    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action"));
        dialogBox.EnableActionSelector(true);
        //test.enabled = true;
    }

    private void Update() {
        if(state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
    }
    

    void HandleActionSelection()
    {
        Debug.Log(currentAction);
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currentAction < 1)
            {
                ++currentAction;
            }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(currentAction > 0)
                --currentAction;
        }
        
        dialogBox.UpdateActionSelection(currentAction);
    }
}
