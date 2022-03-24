using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingButton : MonoBehaviour {
    public void onClick () {
        MenuScene.instance.pageGoto (MenuScene.Pages.Setting);
    }
}
