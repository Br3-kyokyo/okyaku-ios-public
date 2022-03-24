using System.Collections;
using System.Collections.Generic;
using Lib.Scene;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioButton : MonoBehaviour {

    public int category_id { get; set; }
    public int position { get; set; }
    public int category_scenario_num { get; set; }

    public void onClick () {
        SceneManagerEx.LoadSceneWithArg ("gameScene", LoadSceneMode.Single, category_id, position, category_scenario_num);
    }
}
