using System;
using System.Collections;
using System.Collections.Generic;
using Lib.Scene;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour {
    public static MenuScene instance;
    public SimpleSQL.SimpleSQLManager staticDBManager;
    public SimpleSQL.SimpleSQLManager dynamicDBManager;

    public GameObject CategoryPanel;
    public GameObject ScenarioPanel;
    public GameObject SettingPanel;

    public GameObject SettingButton;

    //現在の画面の状態 戻るボタンの遷移先などに使う
    public enum Pages {
        StartScene,
        Category,
        Scenario,
        Setting
    }

    private Stack<Pages> pageHistory = new Stack<Pages> ();
    private Pages currentPage = Pages.StartScene;

    void Awake () {
        instance = this;
        pageGoto (Pages.Category);
    }

    public void pageGoto (Pages next) {
        Debug.Log (next);
        pageHistory.Push (currentPage);
        pageTransition (next);
        currentPage = next;
    }

    public void pageBack () {
        var backto = pageHistory.Pop ();
        Debug.Log (backto);
        pageTransition (backto);
        currentPage = backto;
    }

    private void pageTransition (Pages target) {
        CategoryPanel.SetActive (false);
        ScenarioPanel.SetActive (false);
        SettingPanel.SetActive (false);
        switch (target) {
            case Pages.StartScene:
                SceneManagerEx.LoadSceneWithArg ("startScene", LoadSceneMode.Single);
                break;
            case Pages.Category:
                CategoryPanel.SetActive (true);
                break;
            case Pages.Scenario:
                ScenarioPanel.SetActive (true);
                break;
            case Pages.Setting:
                SettingPanel.SetActive (true);
                break;
        }
        if (target == Pages.Setting) {
            SettingButton.SetActive (false);
        } else {
            SettingButton.SetActive (true);
        }
    }
}
