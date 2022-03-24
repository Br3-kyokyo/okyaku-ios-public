//
//  SpeechSynthsizer.swift
//  iOS speech app
//
//  Created by Kyohei Sasaki on 2019/08/05.
//  Copyright © 2019 Kyohei Sasaki. All rights reserved.
//

import Foundation
import AVFoundation
import Speech

class SpeechSynthesizer: NSObject {

    @objc static let sharedInstance: SpeechSynthesizer = SpeechSynthesizer()
    private let speechSynthesizer = AVSpeechSynthesizer()

    private var _unitySendMessageGameObjectName: String = "HumanAgent";
    @objc var unitySendMessageGameObjectName: String {
        get { return _unitySendMessageGameObjectName;}
        set { _unitySendMessageGameObjectName = newValue;}
    }
    func unitySendMessage(_ methodName: String, message: String = "") {
        UnitySendMessage(self.unitySendMessageGameObjectName, methodName, String(describing: type(of: self)) + ": " + message)
    }

    private override init() {
        super.init()
        speechSynthesizer.delegate = self
    }
    
    @objc func speak(_ text: String) {
        try! speaking(text)
    }
    
    private func speaking(_ text: String) throws {
                        
        let utterance = AVSpeechUtterance(string: text)
        utterance.preUtteranceDelay = 0.25;
        utterance.postUtteranceDelay = 0.25;
        utterance.volume = 1.0;
        utterance.pitchMultiplier = 2.0;
        utterance.voice = AVSpeechSynthesisVoice(language: "en-US")
        
        let audioSession = AVAudioSession.sharedInstance()
        try! audioSession.setCategory(AVAudioSession.Category.ambient)
        try! audioSession.setActive(true, options: .notifyOthersOnDeactivation)
        
        unitySendMessage("SpeechRecognizerDebugInfo", message: "swift: speaking")
        speechSynthesizer.speak(utterance)
    }
}

extension SpeechSynthesizer : AVSpeechSynthesizerDelegate{
    func speechSynthesizer(_ synthesizer: AVSpeechSynthesizer, didFinish utterance: AVSpeechUtterance){
        unitySendMessage("SpeechSynthesizerDidFinishSpeech")
    }
}
