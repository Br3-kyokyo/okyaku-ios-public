using UnityEngine;
using UnityEngine.UI;

public class CustomerResponseUIController : MonoBehaviour {
    private StateMachine state_machine;
    private HumanAgent agent;

    void Start () {
        agent = GameScene.instance.humanAgent;
        state_machine = agent.state_machine;
    }

    void Update () {
        if (state_machine?.prev_transition?.customer_action?.text_ja != null && SpeechSynthesizerStub.isSpeaking)
            this.GetComponent<Text> ().text = state_machine.prev_transition.customer_action.text_ja;
    }
}
