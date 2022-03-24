#import <Foundation/Foundation.h>
#import <Speech/Speech.h>
#import "unityswift-Swift.h"    // Required
// This header file is generated automatically when Xcode build runs.

#pragma mark - C interface

extern "C" {
    void _sr_requestRecognizerAuthorization() {
        [[SpeechRecognizer sharedInstance] requestRecognizerAuthorization];
    }

    BOOL _sr_startRecord(const char **contextualStrings, int size) {
        NSMutableArray *array = [NSMutableArray array];
        for(int i=0; i<size; i++)
            [array addObject:[NSString stringWithUTF8String:contextualStrings[i]]];
        return [[SpeechRecognizer sharedInstance] startRecordWithContextualStrings:(NSArray *)array];
    }
    
    BOOL _sr_stopRecord() {
        return [[SpeechRecognizer sharedInstance] stopRecord];
    }

    BOOL _sr_cancelRecord() {
        return [[SpeechRecognizer sharedInstance] cancelRecord];
    }

    void _sr_setSpeechRecognizerCallbackGameObjectName(const char *callbackGameObjectName) {
        [[SpeechRecognizer sharedInstance] setUnitySendMessageGameObjectName:[NSString stringWithUTF8String:callbackGameObjectName]];
    }
}
