#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

/// <summary>
/// SwiftPluginとの通信スタブ 音声認識の入力部分
/// </summary>
public static class SpeechSynthesizerStub {

        private static bool _isSpeaking = false;
        public static bool isSpeaking {
                get { return _isSpeaking; }
                set {
                        _isSpeaking = value;
                        GameScene.instance.HumanAgentAnimationController.SetBool ("isSpeaking", value);
                }
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport ("__Internal")]
        private static extern void _sr_speak (string text);
        [DllImport ("__Internal")]
        private static extern void _sr_setSpeechSynthesizerCallbackGameObjectName (string callbackGameObjectName);
#endif

        public static void Speak (string text) {
#if UNITY_IOS && !UNITY_EDITOR
                _sr_speak (text);
#endif
        }

        public static string CallbackGameObjectName {
#if UNITY_IOS && !UNITY_EDITOR
                set { _sr_setSpeechSynthesizerCallbackGameObjectName (value); }
#else
                set { }
#endif
        }

}
