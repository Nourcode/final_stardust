﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerMove, PlayerCard, EnemyMove, Busy}
public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;    
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    [SerializeField] private Animator rivalController;

    public event Action<bool> OnBattleOver;
 
    BattleState state;
    int currentAction;
    int currentMove;

    int currentAttack;
    int newAttack;

    public void StartBattle() {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        // Wait For Battle Transition
        FindObjectOfType<AudioManager>().Stop("VoidSong");
        FindObjectOfType<AudioManager>().Play("BattleSong");
        yield return new WaitForSeconds(1.7f);

        // Lower Volume To Hear Sound Effects
        FindObjectOfType<AudioManager>().LowerVolume("BattleSong");

        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.Monster);
        enemyHud.SetData(enemyUnit.Monster);

        dialogBox.SetMoveNames(playerUnit.Monster.Moves);

        yield return dialogBox.TypeDialog("Mirashi challenged you!");
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
    }

    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    void PlayerCard()
    {
        state = BattleState.PlayerCard;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableCardSelector(true);
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;

        var move = playerUnit.Monster.Moves[currentMove];
        
        yield return dialogBox.TypeDialog($"{playerUnit.Monster.Base.Name} uses {move.Base.Name}");

        FindObjectOfType<AudioManager>().Play("CutMove");
        StartCoroutine(playerUnit.PlayAttackAnimation());
        
        
        yield return new WaitForSeconds(1f);

        enemyUnit.PlayHitAnimation();

        bool isFainted = enemyUnit.Monster.TakeDamage(move, playerUnit.Monster);
        yield return enemyHud.UpdateHP();

        yield return new WaitForSeconds(0.5f);
        playerUnit.Monster.LoseMP(move);
        yield return playerHud.UpdateMP();

        //Main attack value before using a card
        playerUnit.Monster.Base.Attack = 52;

        if(isFainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.Monster.Base.Name} is K.O!");
            enemyUnit.PlayFaintAnimation();

            yield return new WaitForSeconds(1f);

            rivalController.SetBool("battleOver", true);
            
            yield return new WaitForSeconds(2f);
            
            StartCoroutine(FindObjectOfType<AudioManager>().FadeOut("BattleSong"));
            FindObjectOfType<AudioManager>().Play("FinalLine");

            yield return dialogBox.TypeDialog("This can't happen...");
            yield return new WaitForSeconds(2f);
            yield return dialogBox.TypeDialog("THE END. Made By Nour Saidana.");
            //OnBattleOver(true);
            yield return new WaitForSeconds(3f);
            Application.Quit();


        } else 
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;

        var move = enemyUnit.Monster.GetRandomMove();

        yield return dialogBox.TypeDialog($"{enemyUnit.Monster.Base.Name} uses {move.Base.Name}");

        FindObjectOfType<AudioManager>().Play("CutMove");
        StartCoroutine(enemyUnit.PlayEnemyAttackAnimation());
        
        yield return new WaitForSeconds(1f);

        playerUnit.PlayHitAnimation();
        bool isFainted = playerUnit.Monster.TakeDamage(move, enemyUnit.Monster);
        yield return playerHud.UpdateHP();

        if(isFainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.Monster.Base.Name} is K.O!");
            playerUnit.PlayFaintAnimation();

            yield return new WaitForSeconds(2f);
            yield return dialogBox.TypeDialog("Restart the game. Next time make sure to use the card!");
            //OnBattleOver(false);

        } else 
        {
            PlayerAction();
        }

    }

    IEnumerator IncreaseStat()
    {
        FindObjectOfType<AudioManager>().Play("GoKairox");
        yield return dialogBox.TypeDialog("Go! Kairox!");

        playerUnit.PlayBoostAnimation();

        currentAttack = playerUnit.Monster.Base.Attack;
        newAttack = currentAttack * 2;
        playerUnit.Monster.Base.Attack = newAttack;

        yield return new WaitForSeconds(1f);

        yield return dialogBox.TypeDialog("You have no cards left.");

        yield return new WaitForSeconds(0.5f);
        //Debug.Log($" this is newAttack {newAttack} and this is the base's attack{playerUnit.Monster.Base.Attack}");
        PlayerAction();
    }

    public void HandleUpdate() {
        if(state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if(state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
        else if(state == BattleState.PlayerCard)
        {
            HandleCardSelection();
        }
    }
    

    void HandleActionSelection()
    {
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

        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(currentAction == 0)
            {
                PlayerMove();
            }
            else if(currentAction == 1)
            {
                PlayerCard();
            }
        }

    }

    void HandleMoveSelection()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(currentMove < playerUnit.Monster.Moves.Count - 1)
            {
                ++currentMove;
            }
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(currentMove > 0)
                --currentMove;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currentMove < playerUnit.Monster.Moves.Count - 2)
                currentMove += 2;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(currentMove > 0)
               currentMove -= 2;
        }

        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Monster.Moves[currentMove]);

        if(Input.GetKeyDown(KeyCode.Z))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());

        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            PlayerAction();
        }

    }

    void HandleCardSelection()
    {
        dialogBox.UpdateCardSelection();

        if(Input.GetKeyDown(KeyCode.Z))
        {
            dialogBox.EnableCardSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(IncreaseStat());
            dialogBox.HideCards(false);
            --currentAction;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            dialogBox.EnableCardSelector(false);
            dialogBox.EnableDialogText(true);
            PlayerAction();
        }
    }
}
