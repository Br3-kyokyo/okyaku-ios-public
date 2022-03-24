using System.Collections.Generic;
using Lib.Scene;
using NCMB;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Lib.Util.StaticMethods;

public class GameScene : MonoBehaviour, ISceneWasLoaded {
    public static GameScene instance { get; private set; }
    public HumanAgent humanAgent { get; private set; }
    int scenario_category_id;
    int state_machine_position_id;
    int category_scenario_num;
    public bool isLoaded { get; private set; } = false;

    [SerializeField]
    private List<GameObject> scenes;
    private int phase = -1;

    public GameObject UpperPanel;
    public GameObject DialoguePanel;
    public GameObject MiddlePanelTextArea;
    public GameObject ResultScoreTMProArea;
    public Button ResultNextLessonButton;

    [HideInInspector]
    public int failureNum;

    public Animator HumanAgentAnimationController;
    public SimpleSQL.SimpleSQLManager staticDBManager;
    public SimpleSQL.SimpleSQLManager dynamicDBManager;

    void Awake () {
        instance = this;
        humanAgent = FindObjectOfType<HumanAgent> ();
        failureNum = 0;
    }

    //called by ScenarioButton#onClick
    public void OnSceneWasLoaded (params object[] arguments) {

        this.scenario_category_id = (int) arguments[0];
        this.state_machine_position_id = (int) arguments[1];
        this.category_scenario_num = (int) arguments[2];

        humanAgent.state_machine.initializer (scenario_category_id, state_machine_position_id);

        activateAllPhase ();
        nextPhase ();

        NCMBObject obj = new NCMBObject ("TestClass");
        obj["message"] = "Hello, NCMB!";
        obj.SaveAsync ((NCMBException e) => {
            if (e != null) {
                UnityEngine.Debug.Log ("保存に失敗: " + e.ErrorMessage);
            } else {
                UnityEngine.Debug.Log ("保存に成功");
            }
        });

        isLoaded = true;
    }

    public void nextPhase () {
        this.phase++;

        logWithMethodName (phase.ToString ());

        //initialize phases
        switch (phase) {
            case 0: //readyPhase
                break;
            case 1: //gamePhase
                humanAgent.startConversation ();
                break;
            case 2: //resultPhase
                var textArea = ResultScoreTMProArea.GetComponent<TMPro.TextMeshProUGUI> ();
                if (failureNum == 0) {
                    var scenario_status_one_or_empty = dynamicDBManager.Query<Models.DynamicDB.ScenarioStatus> ("SELECT SS.* FROM ScenarioStatus SS WHERE state_machine_id = ?", humanAgent.state_machine.state_machine.id);
                    var state_machine_id = humanAgent.state_machine.state_machine.id;
                    if (scenario_status_one_or_empty.Count == 0) {
                        dynamicDBManager.Insert (new Models.DynamicDB.ScenarioStatus { state_machine_id = state_machine_id, is_cleared = true });
                    } else {
                        var scenario_status = scenario_status_one_or_empty[0];
                        scenario_status.state_machine_id = state_machine_id;
                        scenario_status.is_cleared = true;
                        dynamicDBManager.UpdateTable (scenario_status);
                    }

                    textArea.text = "Success!";
                    textArea.color = new Color (116, 253, 128, 255);
                } else {
                    textArea.text = failureNum + " failures";
                    textArea.color = new Color (255, 107, 165, 255);
                }

                ResultNextLessonButton.interactable = (state_machine_position_id < category_scenario_num - 1);
                break;
        }

        for (int i = 0; i < scenes.Count; i++)
            scenes[i].SetActive (this.phase == i);
    }

    public void toMenuScene () {
        destroyScene ();
        SceneManager.LoadScene ("menuScene");
    }

    public void nextGameScene () {
        destroyScene ();
        SceneManagerEx.LoadSceneWithArg ("gameScene", LoadSceneMode.Single, scenario_category_id, state_machine_position_id + 1);
    }

    public void retryGameScene () {
        destroyScene ();
        SceneManagerEx.LoadSceneWithArg ("gameScene", LoadSceneMode.Single, scenario_category_id, state_machine_position_id);
    }

    private void destroyScene () {
        SpeechRecognizerStub.StopRecord ();
    }

    private void activateAllPhase () {
        for (int i = 0; i < scenes.Count; i++) scenes[i].SetActive (true);
    }
}
