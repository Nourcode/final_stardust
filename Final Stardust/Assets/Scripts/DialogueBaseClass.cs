using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueBaseClass : MonoBehaviour
{
    //[SerializeField] PlayerController playerController; 
    public bool finished { get; private set; }
    protected IEnumerator WriteText(string input, Text textHolder, float delay, AudioClip sound, float delayBetweenLines)
    {
        SoundManager.instance.PlaySound(sound);
        
        for(int i = 0; i < input.Length; i++)
        {
            textHolder.text += input[i];
            
            yield return new WaitForSeconds(0.03f);
        }

        //yield return new WaitForSeconds(delayBetweenLines);
        
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        
        finished = true;
    }

    
}
