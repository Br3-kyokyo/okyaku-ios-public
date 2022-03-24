using System.Diagnostics;
using System.Timers;
using static Lib.Util.StaticMethods;
using System.Collections;
using UnityEngine;

public class SpeechRecognizerTimeKeeper : MonoBehaviour {

    private HumanAgent context;
    private Stopwatch stopwatch;
    private const float TIMER_CALLBACK_INTERVAL_TIME = 0.1f;
    public bool running;

    void Awake () {
        context = FindObjectOfType<HumanAgent> ();
    }

    void Start () {
        stopwatch = new Stopwatch ();
        StartCoroutine ("timeKeeper");
    }

    private IEnumerator timeKeeper () {
        while (true) {
            yield return new WaitForSeconds (TIMER_CALLBACK_INTERVAL_TIME);
            if (stopwatch.ElapsedMilliseconds > 2000) {
                UnityEngine.Debug.Log ("timekeeper callback");
                context.pardon = true;
                context.cancelListening ();
            }
        }
    }

    public void restartTimer () {
        logWithMethodName ("is not running");
        if (running) {
            logWithMethodName (stopwatch.Elapsed.ToString ());
            stopwatch.Restart ();
        }
    }

    public void stopTimer () {
        logWithMethodName (stopwatch.Elapsed.ToString ());
        running = false;
        stopwatch.Reset ();
        stopwatch.Stop ();
    }
}
