using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DialogueHolder : MonoBehaviour
{

    public Canvas crossFade;
    public Animator transition;
    public float transitionTime = 1f;
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

        

        //crossFade.enabled = true;

        //transition.SetTrigger("Start");

        //yield return new WaitForSeconds(transitionTime);

        if(director != null)
        {
            director.Stop();
        }

        playerController.CheckForBattle();
        //StartCoroutine(LoadLevel());
        //playerController.CheckForBattle();
    }

    private void Deactive()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    IEnumerator LoadLevel()
    {
        yield return null;
        
    }
}
