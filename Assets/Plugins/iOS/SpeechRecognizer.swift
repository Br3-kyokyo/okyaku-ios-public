//
//  SpeechRecognizer.swift
//  iOS speech app
//
//  Created by Kyohei Sasaki on 2019/07/03.
//  Copyright © 2019 Kyohei Sasaki. All rights reserved.
//

import Foundation
import Speech

class SpeechRecognizer: NSObject {
    
    @objc static let sharedInstance: SpeechRecognizer = SpeechRecognizer()
    
    private var _unitySendMessageGameObjectName: String = "HumanAgent";
    @objc var unitySendMessageGameObjectName: String {
        get {
            return _unitySendMessageGameObjectName;
        }
        set {
            _unitySendMessageGameObjectName = newValue;
        }
    }
    
    private let speechRecognizer = SFSpeechRecognizer(locale: Locale(identifier: "en-JP"))!
    private var recognitionRequest: SFSpeechAudioBufferRecognitionRequest?
    private var recognitionTask: SFSpeechRecognitionTask?
    private let audioEngine = AVAudioEngine()
    
    private override init() {
        super.init()
        speechRecognizer.delegate = self
    }
    
    @objc func requestRecognizerAuthorization() {
        
        // Make the authorization request
        SFSpeechRecognizer.requestAuthorization { authStatus in
            
            // The authorization status results in changes to the
            // app’s interface, so process the results on the app’s
            // main queue.
            OperationQueue.main.addOperation {
                switch authStatus {
                case .authorized:
                    self.unitySendMessage("SpeechRecognizerOnAuthorized")
                    
                case .denied:
                    self.unitySendMessage("SpeechRecognizerOnUnauthorized", message: "denied")
                    
                case .restricted:
                    self.unitySendMessage("SpeechRecognizerOnUnauthorized", message: "restricted")
                    
                case .notDetermined:
                    self.unitySendMessage("SpeechRecognizerOnUnauthorized", message: "notDertermined")

                @unknown default:
                    self.unitySendMessage("SpeechRecognizerOnUnauthorized", message: "unknownError")
                }
            }
        }
    }
    
    @objc func startRecord(contextualStrings: NSArray) -> Bool{
        if audioEngine.isRunning {
            return false
        }else{
            try! startRecording(contextualStrings: contextualStrings)
            return true
        }
    }
    
    @objc func stopRecord() -> Bool{
        if !audioEngine.isRunning{
            unitySendMessage("SpeechRecognizerDebugInfo", message: "swift: recognizer stop failed")
            return false
        }else{
            audioEngine.stop()
            recognitionRequest?.endAudio()
            recognitionTask?.finish()
            return true
        }
    }
    
    @objc func cancelRecord() -> Bool{
        if !audioEngine.isRunning{
            unitySendMessage("SpeechRecognizerDebugInfo", message: "swift: recognizer cancel failed")
            return false
        }else{
            audioEngine.stop()
            recognitionRequest?.endAudio()
            recognitionTask?.cancel()
            audioEngine.inputNode.removeTap(onBus: 0)
            return true
        }
    }
    
    private func startRecording(contextualStrings: NSArray) throws{
        
        // Cancel the previous task if it's running.
        recognitionTask?.cancel()
        self.recognitionTask = nil

        let audioSession = AVAudioSession.sharedInstance()
        try! audioSession.setCategory(AVAudioSession.Category.playAndRecord)
        try! audioSession.setMode(AVAudioSession.Mode.measurement)
        try! audioSession.setActive(true, options: .notifyOthersOnDeactivation)
        
        //マイクからの入力を受け取るinputNode
        let inputNode = audioEngine.inputNode

        //音声を認識するためのリクエストを作成
        self.recognitionRequest = SFSpeechAudioBufferRecognitionRequest()
        guard let recognitionRequest = recognitionRequest else { fatalError("Unable to created a SFSpeechAudioBufferRecognitionRequest object") }
        recognitionRequest.shouldReportPartialResults = true
        recognitionRequest.contextualStrings = contextualStrings as! [String]

        //音声をRecognizeした時の処理を設定、開始
        recognitionTask = speechRecognizer.recognitionTask(with: recognitionRequest, delegate: self)
        
        //inputNode(マイク)から入力された音声をrecognitionRequestに格納する処理を定義
        let recordingFormat = inputNode.outputFormat(forBus: 0)
        inputNode.installTap(onBus: 0, bufferSize: 1024, format: recordingFormat) { (buffer: AVAudioPCMBuffer, when: AVAudioTime) in
            self.recognitionRequest?.append(buffer);
        }

        //オーディオエンジンを開始
        audioEngine.prepare()
        try! audioEngine.start()
        
        self.unitySendMessage("SpeechRecognizerDebugInfo", message: "(Go ahead, I'm listening)");
    }

    func unitySendMessage(_ methodName: String, message: String = "") {
        UnitySendMessage(self.unitySendMessageGameObjectName, methodName, message)
    }
}

extension SpeechRecognizer : SFSpeechRecognizerDelegate {
    func speechRecognizer(_ speechRecognizer: SFSpeechRecognizer, availabilityDidChange available: Bool) {
        if available {
            unitySendMessage("SpeechRecognizerOnAvailable")
        } else {
            unitySendMessage("SpeechRecognizerOnUnavailable")
        }
    }
}

extension SpeechRecognizer : SFSpeechRecognitionTaskDelegate {
    
    func speechRecognitionDidDetectSpeech(_ task: SFSpeechRecognitionTask){
        self.unitySendMessage("speechRecognitionDidDetectSpeech")
    }
    
    func speechRecognitionTask(_ task: SFSpeechRecognitionTask, didHypothesizeTranscription transcription: SFTranscription) {
        self.unitySendMessage("speechRecognitionDidHypothesizeTranscription", message: transcription.formattedString)
    }
    
    func speechRecognitionTask(_ task: SFSpeechRecognitionTask, didFinishRecognition recognitionResult: SFSpeechRecognitionResult) {
        self.unitySendMessage("speechRecognitionDidFinishRecognition", message: recognitionResult.bestTranscription.formattedString)
    }

    func speechRecognitionTask(_ task: SFSpeechRecognitionTask, didFinishSuccessfully successfully: Bool) {
        let inputNode = audioEngine.inputNode
        self.audioEngine.stop()
        inputNode.removeTap(onBus: 0)
        self.recognitionRequest = nil
        self.recognitionTask = nil
        
        if(successfully){
            self.unitySendMessage("speechRecognitionDidFinishSuccessfully")
        }else{
            self.unitySendMessage("speechRecognitionDidFinishWithError")
        }
    }
}
