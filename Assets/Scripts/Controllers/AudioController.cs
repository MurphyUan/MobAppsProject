using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("AudioSources")]
    [SerializeField] private AudioSource Music;
    [SerializeField] private AudioSource Effects;
    [SerializeField] private AudioSource UI;


    public void PlayClipFromMusic(AudioClip clip){
        Effects.PlayOneShot(clip);
    }

    public void PlayClipFromEffect(AudioClip clip){
        Effects.PlayOneShot(clip);
    }

    public void PlayClipFromUI(AudioClip clip){
        Effects.PlayOneShot(clip);
    }
}
