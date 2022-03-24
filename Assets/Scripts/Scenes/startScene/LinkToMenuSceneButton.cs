using System.Collections;
using System.Collections.Generic;
using Lib.Scene;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LinkToMenuSceneButton : MonoBehaviour {
    public void onClickMenuSceneButton (AudioClip sound) {

        int isFirstLaunch = PlayerPrefs.GetInt ("isFirstLaunch", 1);
        Debug.Log ("isFirstLaunch:" + isFirstLaunch);

        StartScene.instance.audioSource.PlayOneShot (sound);
        if (isFirstLaunch == 1) {
            // display modal
            Debug.Log ("isFirstLaunch");
            StartScene.instance.notesModal.SetActive (true);
        } else {
            SceneManager.LoadScene ("menuScene");
        }
    }
}
