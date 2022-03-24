using System.Linq;
using Lib.Util;
using UnityEngine;
using UnityEngine.UI;
using static Lib.Util.StaticMethods;
using TMPro;

public class DialoguePanelController : MonoBehaviour {

    HumanAgent agent;
    StateMachine state_machine;
    public TextMeshProUGUI textArea { get; set; }

    void Awake () {
        agent = GameScene.instance.humanAgent;
        state_machine = agent.state_machine;
        textArea = gameObject.transform.GetChild (0).gameObject.GetComponentInChildren<TextMeshProUGUI> ();
    }
}
