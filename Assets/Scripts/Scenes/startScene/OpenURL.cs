using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenURL : MonoBehaviour {
    public void openURL (string url) {
#if UNITY_EDITOR
        Application.OpenURL (url);
#elif UNITY_WEBGL
        Application.ExternalEval (string.Format ("window.open('{0}','_blank')", url));
#else
        Application.OpenURL (url);
#endif
    }
}
