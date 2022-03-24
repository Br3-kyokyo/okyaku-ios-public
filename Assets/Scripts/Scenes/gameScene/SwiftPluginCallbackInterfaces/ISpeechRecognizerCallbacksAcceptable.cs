interface ISpeechRecognizerCallbacksAcceptable {
    //[TODO] READMEに役割を記載
    void speechRecognitionDidDetectSpeech ();
    void speechRecognitionDidHypothesizeTranscription (string transcription);
    void speechRecognitionDidFinishRecognition (string result);
    void speechRecognitionDidFinishSuccessfully ();
    void speechRecognitionDidFinishWithError ();
    void SpeechRecognizerOnAuthorized ();
    void SpeechRecognizerOnUnauthorized (string description);
    void SpeechRecognizerOnAvailable ();
    void SpeechRecognizerOnUnavailable ();
    void SpeechRecognizerDebugInfo (string description);
}
