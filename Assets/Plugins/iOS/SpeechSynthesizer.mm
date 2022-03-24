#import <Foundation/Foundation.h>
#import <Speech/Speech.h>
#import "unityswift-Swift.h"    // Required
// This header file is generated automatically when Xcode build runs.

#pragma mark - C interface

extern "C" {
    void _sr_speak(const char *text){
        [[SpeechSynthesizer sharedInstance] speak:[NSString stringWithUTF8String:text]];
    }

    void _sr_setSpeechSynthesizerCallbackGameObjectName(const char *callbackGameObjectName) {
        [[SpeechSynthesizer sharedInstance] setUnitySendMessageGameObjectName:[NSString stringWithUTF8String:callbackGameObjectName]];
    }
}
