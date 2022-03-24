using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClickSound : MonoBehaviour {
    private AudioSource audioSource;
    public AudioClip onClick;
    void Awake () {
        audioSource = (AudioSource) FindObjectOfType (typeof (AudioSource));
    }

    public void onClickButton () {
        audioSource.PlayOneShot (onClick);
    }
}
