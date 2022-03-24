using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DebugUIController : MonoBehaviour {

    List<string> contextualStrings;

    public void startRecognition () {
        Debug.Log ("start Recognition.");
        contextualStrings = new List<string> ();
        foreach (var transition in GameScene.instance.humanAgent.state_machine.state.nextTransitions)
            foreach (var sentence in transition.trigger.trigger_sentences)
                contextualStrings.Add (sentence.body_en);
        SpeechRecognizerStub.StartRecord (contextualStrings.ToArray ());
    }
    public void stopRecognition () {
        Debug.Log ("stop Recognition.");
        SpeechRecognizerStub.StopRecord ();
    }

    public void next () {
        var smrunner = GameScene.instance.humanAgent.state_machine;
        smrunner.handle (smrunner.state.nextTransitions.First ().id);
        if (smrunner.state.is_accept) GameScene.instance.nextPhase ();
    }
}
