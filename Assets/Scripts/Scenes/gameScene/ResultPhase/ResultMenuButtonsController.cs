using UnityEngine;

public class ResultMenuButtonsController : MonoBehaviour {
    GameScene gameScene;
    void Start () => gameScene = GameScene.instance;
    public void next () => gameScene.nextGameScene ();
    public void retry () => gameScene.retryGameScene ();
    public void menu () => gameScene.toMenuScene ();
}
