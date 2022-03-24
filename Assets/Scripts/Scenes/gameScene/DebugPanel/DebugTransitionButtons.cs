using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DebugTransitionButtons : MonoBehaviour {

    public GameObject DebugTransitionButtonPrefab;
    public void updateContent (int[] nextTransitionIds) {

        if (nextTransitionIds.Count () == 0) {
            GameScene.instance.nextPhase ();
            return;
        }

        foreach (Transform n in gameObject.transform) {
            GameObject.Destroy (n.gameObject);
        }
        foreach (var id in nextTransitionIds.Select ((value, index) => new { value, index })) {
            GameObject button = Instantiate (DebugTransitionButtonPrefab, gameObject.transform);
            button.GetComponent<DebugTransitionButton> ().id = id.value;
            button.transform.localPosition += new Vector3 (150 * id.index, 0, 0);
        }
    }
}
