using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public LayerMask solidObjectsLayer;

    public event Action OnDialogue;

    private bool isMoving;
    private Vector2 input;

    private Animator animator;

    [SerializeField] PlayableDirector director; 

    private void Awake() {
        animator = GetComponent<Animator>();
    }


    public void HandleUpdate()
    {
        if(!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // remove diagonal movement
            if(input.x != 0)
            {
                input.y = 0;
            }

            if(input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (isWalkable(targetPos)){
                    StartCoroutine(Move(targetPos));
                    
                }
            }

        }
        
        animator.SetBool("isMoving", isMoving);
        
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;


    }

    private bool isWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer))
        {
            return false;
        }

        return true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(director != null)
        {
            director.Play();
        }

        //CheckForBattle();
    }
    
    public void CheckForBattle()
    {
        animator.SetBool("isMoving", false);
        OnDialogue();
    }
}
