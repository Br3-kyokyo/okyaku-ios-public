#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

using System.Linq;
using UnityEngine;
/// <summary>
/// SwiftPluginとの通信スタブ 音声認識の入力部分
/// </summary>
public static class SpeechRecognizerStub {

        private static bool _isListening = false;
        public static bool isListening {
                get { return _isListening; }
                set {
                        _isListening = value;
                        GameScene.instance.MiddlePanelTextArea.SetActive (value);
                }
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport ("__Internal")]
        private static extern void _sr_requestRecognizerAuthorization ();
        [DllImport ("__Internal")]
        private static extern bool _sr_startRecord (string[] contextualStrings, int size);
        [DllImport ("__Internal")]
        private static extern bool _sr_stopRecord ();
        [DllImport ("__Internal")]
        private static extern bool _sr_cancelRecord ();
        [DllImport ("__Internal")]
        private static extern void _sr_setSpeechRecognizerCallbackGameObjectName (string callbackGameObjectName);
#endif

        public static void RequestRecognizerAuthorization () {
#if UNITY_IOS && !UNITY_EDITOR
                _sr_requestRecognizerAuthorization ();
#endif
        }

        public static bool StartRecord (string[] contextualStrings) {
#if UNITY_IOS && !UNITY_EDITOR
                return _sr_startRecord (contextualStrings, contextualStrings.Length);
#else
                return false;
#endif
        }

        public static bool StopRecord () {
#if UNITY_IOS && !UNITY_EDITOR
                return _sr_stopRecord ();
#else
                return false;
#endif
        }

        public static bool CancelRecord () {
#if UNITY_IOS && !UNITY_EDITOR
                return _sr_cancelRecord ();
#else
                return false;
#endif
        }

        public static string CallbackGameObjectName {
#if UNITY_IOS && !UNITY_EDITOR
                set { _sr_setSpeechRecognizerCallbackGameObjectName (value); }
#else
                set { }
#endif
        }
}
