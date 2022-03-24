using UnityEngine;
using UnityEngine.UI;

public class DebugTransitionButton : MonoBehaviour {

    [HideInInspector]
    public int id;
    public void onClick () {
        GameScene.instance.humanAgent.state_machine.handle (id);
    }
}
