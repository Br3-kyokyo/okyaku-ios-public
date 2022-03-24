using System;
using System.Linq;
using Lib.Util;
using static Lib.Util.StaticMethods;

public partial class HumanAgent : ISpeechSynthesizerCallbacksAcceptable {

    public void SpeechSynthesizerDidFinishSpeech () {
        afterSpeaking ();
    }
    public void SpeechSynthesizerDebugInfo (string description) { logWithMethodName (description); }
}

public partial class HumanAgent : ISpeechRecognizerCallbacksAcceptable {
    public void speechRecognitionDidDetectSpeech () {
        logWithMethodName ();
        timekeeper.restartTimer ();
    }

    //音声認識中はstopwatchはrestartしたい 音声認識していない場合restartしない
    //最初は　restartでスタートする

    public void speechRecognitionDidHypothesizeTranscription (string transcription) {
        if (SpeechSynthesizerStub.isSpeaking) return;
        logWithMethodName (transcription);

        //応急処置 hypothesizeTranscriotionがくるたびにrunningをtrueに設定している（本来ならば初回のみでOKだがどれが最初のTranscriptionなのか判定することが困難。DidDetectSpeechがなぜか動かない）
        timekeeper.running = true;
        timekeeper.restartTimer ();

        if (state_machine.handle (new RecognitionResult (transcription))) {
            logWithMethodName ("transition successful");
            stopListening ();
            transitionedByHypothesize = true;
            transitioned = true;
        } else {
            logWithMethodName ("transition failed");
        }

    }

    public void speechRecognitionDidFinishRecognition (string result) {
        if (SpeechSynthesizerStub.isSpeaking) return;

        timekeeper.stopTimer ();

        //遷移済みの場合と、resultで遷移判定する場合で場合分け
        if (transitionedByHypothesize) {
            logWithMethodName ("transitionedByHypithesize:" + result);
            transitionedByHypothesize = false;
        } else if (state_machine.handle (new RecognitionResult (result))) {
            logWithMethodName ("transition successfull :" + result);
            transitioned = true;
        }
    }

    public void speechRecognitionDidFinishSuccessfully () {
        logWithMethodName ();
        afterListening ();
    }

    public void speechRecognitionDidFinishWithError () {
        logWithMethodName ();
        afterListening ();
    }
    public void SpeechRecognizerOnError (string description) { logWithMethodName (description); }
    public void SpeechRecognizerOnAuthorized () { logWithMethodName (); }
    public void SpeechRecognizerOnUnauthorized (string description) { logWithMethodName (description); }
    public void SpeechRecognizerOnAvailable () { logWithMethodName (); }
    public void SpeechRecognizerOnUnavailable () { logWithMethodName (); }
    public void SpeechRecognizerDebugInfo (string description) { logWithMethodName (description); }

}
