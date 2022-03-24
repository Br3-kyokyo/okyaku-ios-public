using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Lib.Scene {
    public static class SceneManagerEx {
        static public void LoadSceneWithArg (
            string sceneName,
            LoadSceneMode mode,
            params object[] arguments) {
            UnityAction<UnityEngine.SceneManagement.Scene, LoadSceneMode> sceneLoaded = default;
            Action removeHandler = () => {
                SceneManager.sceneLoaded -= sceneLoaded;
            };

            sceneLoaded = (loadedScene, sceneMode) => {
                removeHandler ();
                foreach (var root in loadedScene.GetRootGameObjects ()) {
                    ExecuteEvents.Execute<ISceneWasLoaded> (root, null, (receiver, e) => receiver.OnSceneWasLoaded (arguments));
                }
            };

            SceneManager.sceneLoaded += sceneLoaded;
            SceneManager.LoadScene (sceneName, mode);
        }
    }
}
