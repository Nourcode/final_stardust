using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueLine : DialogueBaseClass
{
    
    private Text textHolder;

    [Header ("Text Options")]
    [SerializeField] private string input;

    [Header ("Time parameters")]
    [SerializeField] private float delay;
    [SerializeField] private float delayBetweenLines;

    [Header ("Sound")]
    [SerializeField] private AudioClip sound;

    private void Awake() {
        textHolder = GetComponent<Text>();
        textHolder.text = "";
    }

    private void Start() {
        StartCoroutine(WriteText(input, textHolder, delay, sound, delayBetweenLines));
    }
}
