
# ios-dialogue-app

第二言語訓練用iOSアプリ

## Theme

- 拡大する第二言語による接遇機会
- 接遇を訓練するスマートフォンアプリを開発
- ユーザー評価や利用率、学習効果を測定

## Storyboard

![IMG_0663](https://user-images.githubusercontent.com/21092061/62443665-6fca0a00-b796-11e9-90f2-9dd577b33498.PNG)

## UMLdiagram

![IMG_0713](https://user-images.githubusercontent.com/21092061/63229947-3935ca00-c241-11e9-890d-70770295b21d.PNG)

## Class Design

### (class)StateMachine : MonoBehaviour
- 現在の状態を保持
- 状態遷移時の動作を定義
### (static class)SpeechRecognizerStub
- 音声認識の入力部分 SwiftSpeechRecognizerPluginの呼び出しスタブ
### (static class)SpeechSynthesizerStub
- 音声出力の入力部分 SwiftSpeechSynthesizerPluginの呼び出しスタブ
### (class)SpeechRecognizerCallbacks : MonoBehaviour
- SwiftPluginが呼び出せるメソッド群
- 音声認識の結果に応じてStateMachineを操作
### (class)ButtonUI : MonoBehaviour
- Viewに配置したボタンの動作を定義
### (class)DebugLog : MonoBehaviour
- UnityのDebug.Logの内容をViewに配置したコンソールウィンドウに表示する処理を記述
- デリゲートメソッドを登録
