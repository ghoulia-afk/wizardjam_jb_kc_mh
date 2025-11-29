using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnAudioCuePlayer : MonoBehaviour
{
    public DialogueRunner dialogueRunner;

    public AudioClip[] audioClips;

    AudioSource audioSource;

    void Awake()
    {
        dialogueRunner.AddCommandHandler<string>("PlayAudioClip", PlayAudioClip);
    }

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    public  void PlayAudioClip(string clipName){
      foreach (AudioClip clip in audioClips)
{
    if(clip.name == clipName){
        audioSource.PlayOneShot(clip);
    }
}
    }


}
