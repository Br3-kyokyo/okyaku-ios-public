using UnityEngine;
using UnityEngine.UI;

public class DebugLog : MonoBehaviour {

    public Text LogTextField;

    void OnEnable () {
        Application.logMessageReceived += putLogMessagesOnView;
    }

    void OnDisable () {
        Application.logMessageReceived -= putLogMessagesOnView;
    }

    private void putLogMessagesOnView (string i_logText, string i_stackTrace, LogType i_type) {

        if (string.IsNullOrEmpty (i_logText)) return;

        switch (i_type) {
            case LogType.Error:
            case LogType.Assert:
            case LogType.Exception:
                i_logText = i_logText + "\n" + "<size=25>" + i_stackTrace + "</size>";
                i_logText = string.Format ("<color=red>{0}</color>", i_logText);
                break;
            case LogType.Warning:
                i_logText = i_logText + "\n" + "<size=25>" + i_stackTrace + "</size>";
                i_logText = string.Format ("<color=yellow>{0}</color>", i_logText);
                break;
            default:
                break;
        }

        string processedText = LogTextField.text + i_logText + "\n\n";
        processedText = (processedText.Length > 10000) ? processedText.Substring (processedText.Length - 10000, 10000) : processedText;
        LogTextField.text = processedText;
    }
}
