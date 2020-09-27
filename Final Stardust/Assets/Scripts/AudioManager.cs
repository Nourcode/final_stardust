using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
      foreach(Sound s in sounds)
      {
          s.source = gameObject.AddComponent<AudioSource>();
          s.source.clip = s.clip;

          s.source.volume = s.volume;
          s.source.pitch = s.pitch;
          s.source.loop = s.loop;
      }  
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void LowerVolume(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = 0.5f;
    }

    public IEnumerator Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.volume -= s.source.volume * Time.deltaTime / 0.05f;
         yield return new WaitForSeconds(0.1f);
        s.source.volume -= s.source.volume * Time.deltaTime / 0.05f;
         yield return new WaitForSeconds(0.1f);
        s.source.volume -= s.source.volume * Time.deltaTime / 0.05f;
         yield return new WaitForSeconds(0.1f);
        s.source.volume -= s.source.volume * Time.deltaTime / 0.05f;
         yield return new WaitForSeconds(0.1f);
        s.source.volume -= s.source.volume * Time.deltaTime / 0.05f;
         yield return new WaitForSeconds(0.1f);

        s.source.Stop();

        yield return null;
    }
}
