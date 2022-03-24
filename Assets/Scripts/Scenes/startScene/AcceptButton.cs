using System.Collections;
using System.Collections.Generic;
using Lib.Scene;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AcceptButton : MonoBehaviour {

    public void onClick (AudioClip sound) {
#if UNITY_IOS && !UNITY_EDITOR
        PlayerPrefs.SetInt ("isFirstLaunch", 0);
#endif
        StartScene.instance.audioSource.PlayOneShot (sound);
        SceneManager.LoadScene ("menuScene");
    }
}
