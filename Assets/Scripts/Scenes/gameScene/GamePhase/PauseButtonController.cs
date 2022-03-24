using UnityEngine;

public class PauseButtonController : MonoBehaviour {
    public void retire () {
        GameScene.instance.toMenuScene ();
        SpeechRecognizerStub.StopRecord ();
    }
}
