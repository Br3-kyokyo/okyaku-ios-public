using UnityEngine;

public class GameStartButtonController : MonoBehaviour {
    public void gameStart () { GameScene.instance.nextPhase (); }
}
