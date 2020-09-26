using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DialogueHolder : MonoBehaviour
{
    [SerializeField] PlayableDirector director; 
    [SerializeField] PlayerController playerController; 

    [SerializeField] Image dialogBoxCharacter;
    private void Awake()
    {
        StartCoroutine(dialogueSequence());
    }
    private IEnumerator dialogueSequence()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Deactive();
            transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
        }
        gameObject.SetActive(false);
        dialogBoxCharacter.enabled = false;

        if(director != null)
        {
            director.Stop();
        }

        playerController.CheckForBattle();
    }

    private void Deactive()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
