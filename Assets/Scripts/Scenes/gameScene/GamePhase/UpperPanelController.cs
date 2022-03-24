using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Lib.Util.StaticMethods;
using TMPro;

public class UpperPanelController : MonoBehaviour {

    HumanAgent agent;
    StateMachine state_machine;
    public TextMeshProUGUI textArea { get; set; }

    void Awake () {
        agent = GameScene.instance.humanAgent;
        state_machine = agent.state_machine;
        textArea = gameObject.transform.GetChild (0).gameObject.GetComponentInChildren<TextMeshProUGUI> ();
        this.textArea.text = "";
    }

    public void updateHint () {
        string text = "";
        foreach (var trigger in state_machine.state.nextTransitions.Select (nt => nt.trigger))
            foreach (var t_sentence in trigger.trigger_sentences)
                text += (t_sentence.body_en + "\n");
        this.textArea.text = text;
    }

    public void disableHint () {
        this.textArea.text = "";
    }

    public void playSpeakModel () {
#if UNITY_EDITOR
        Debug.Log ("button: play speak model");
#else
        //[TODO] change icon while speaking
        play (textArea.text);
#endif
    }

    public void play (string text) {
        logWithMethodName ();
        if (SpeechSynthesizerStub.isSpeaking) {
            throw new SpeakProceedingException ();
        } else {
            if (SpeechRecognizerStub.isListening) {
                SpeechRecognizerStub.CancelRecord ();
                SpeechRecognizerStub.isListening = false;
            }
            SpeechSynthesizerStub.isSpeaking = true;
            SpeechSynthesizerStub.Speak (text);
        }
    }
}
