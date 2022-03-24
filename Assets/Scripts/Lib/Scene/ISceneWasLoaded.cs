using UnityEngine.EventSystems;

namespace Lib.Scene {
    public interface ISceneWasLoaded : IEventSystemHandler {
        void OnSceneWasLoaded (params object[] arguments);
    }
}
