using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Lib.Util.StaticMethods;

/// <summary>
/// Swift音声認識とStateMachine、Viewの中継地点
/// </summary>
public partial class HumanAgent : MonoBehaviour {

    public StateMachine state_machine { get; private set; }
    private SpeechRecognizerTimeKeeper timekeeper { get; set; }
    private bool transitionedByHypothesize = false;
    private bool transitioned = false;
    public bool pardon = false;
    public bool modelSpeaking = false;

    void Awake () {
        state_machine = FindObjectOfType<StateMachine> ();
        timekeeper = FindObjectOfType<SpeechRecognizerTimeKeeper> ();
    }

    void Start () {
        SpeechSynthesizerStub.CallbackGameObjectName = gameObject.name;
        SpeechRecognizerStub.CallbackGameObjectName = gameObject.name;
    }

    public void startConversation () {
        logWithMethodName ();
        transitioned = true;
        if (state_machine.start ())
            startSpeaking (state_machine.prev_transition.customer_action.text_en);
    }

    public void startSpeaking (string text) {
        if (SpeechSynthesizerStub.isSpeaking || SpeechRecognizerStub.isListening) {
            if (SpeechSynthesizerStub.isSpeaking) logWithMethodName ("isSpeaking: " + text);
            if (SpeechRecognizerStub.isListening) logWithMethodName ("isListening" + text);
            throw new SpeakProceedingException ();
        }

        logWithMethodName (text);
        SpeechSynthesizerStub.isSpeaking = true;
        GameScene.instance.DialoguePanel.GetComponent<DialoguePanelController> ().textArea.text = text;
        SpeechSynthesizerStub.Speak (text);

    }

    public void afterSpeaking () {

        SpeechSynthesizerStub.isSpeaking = false;

        if (transitioned) {
            transitioned = false;
            if (state_machine.state.is_accept) {
                logWithMethodName ("is accept state, go to next phase");
                GameScene.instance.nextPhase ();
            } else {
                logWithMethodName ("is not accept state, and start record");
                GameScene.instance.UpperPanel.GetComponent<UpperPanelController> ().updateHint ();
                startListening ();
            }
        } else {
            logWithMethodName ("speaking with no transition");
            startListening ();
        }
    }

    public void startListening () {
        if (SpeechSynthesizerStub.isSpeaking || SpeechRecognizerStub.isListening) {
            if (SpeechSynthesizerStub.isSpeaking) logWithMethodName ("isSpeaking");
            if (SpeechRecognizerStub.isListening) logWithMethodName ("isListening");
            throw new SpeakProceedingException ();
        }

        logWithMethodName ();
        SpeechRecognizerStub.isListening = true;
        SpeechRecognizerStub.StartRecord (contextualStrings ().ToArray ());
    }

    public void stopListening () {
        logWithMethodName ();
        timekeeper.stopTimer ();
        SpeechRecognizerStub.StopRecord ();
    }

    public void cancelListening () {
        logWithMethodName ();
        timekeeper.stopTimer ();
        SpeechRecognizerStub.CancelRecord ();
        afterListening ();
    }

    private void afterListening () {
        SpeechRecognizerStub.isListening = false;
        if (transitioned) {
            afterTransition ();
        } else if (pardon) {
            retry ();
        } else {
            logWithMethodName ("unknown state. speaking no text.");
        }
    }

    private void retry () {
        logWithMethodName ("startSpeaking: can you say that again?");
        string text = "Can you say that again?";
        GameScene.instance.failureNum++;
        GameScene.instance.DialoguePanel.GetComponent<DialoguePanelController> ().textArea.text = text;
        startSpeaking (text);
    }

    private void afterTransition () {
        logWithMethodName ("transitioned");
        string text = state_machine.prev_transition.customer_action.text_en;
        startSpeaking (text);
    }

    private List<string> contextualStrings () {
        List<string> contextualStrings = new List<string> ();
        foreach (var transition in state_machine.state.nextTransitions)
            foreach (var sentence in transition.trigger.trigger_sentences)
                contextualStrings.Add (sentence.body_en);
        return contextualStrings;
    }

}
