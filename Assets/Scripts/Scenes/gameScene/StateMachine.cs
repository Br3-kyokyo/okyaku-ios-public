using System.Collections.Generic;
using System.Linq;
using Lib.Util;
using Models.StaticDB;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 状態遷移を行なってそれに対応する出力を行う
/// </summary>
public class StateMachine : MonoBehaviour {

    private SimpleSQL.SimpleSQLManager staticDBManager;
    public state_machines state_machine { get; private set; } //[TODO] update veriable name - confused
    public states state { get; private set; }
    public transitions prev_transition { get; private set; }
    private bool isInitialized = false;

    //Start()の代わり GameSceneの読み込み後に初期化を行っている
    public StateMachine initializer (int category_id, int position) {
        if (!isInitialized) {
            staticDBManager = GameScene.instance.staticDBManager;
            // [TODO] テーブル名を指定したら勝手にJOINしてくれるようにしたいね…
            var state_machines = staticDBManager.Query<Models.StaticDB.state_machines> ("SELECT SM.* FROM state_machines SM WHERE scenario_category_id = ? ORDER BY position ASC", category_id);
            state_machine = state_machines[position];
            state_machine.states = staticDBManager.Query<states> ("SELECT S.* FROM states S WHERE S.state_machine_id = ?", state_machine.id);
            foreach (var state in state_machine.states) {
                state.nextTransitions = staticDBManager.Query<transitions> ("SELECT T.* FROM transitions T WHERE T.prev_state_id = ?", state.id);
                buildTransition (state.nextTransitions);
                state.prevTransitions = staticDBManager.Query<transitions> ("SELECT T.* FROM transitions T WHERE T.next_state_id = ?", state.id);
                buildTransition (state.prevTransitions);
            }
            state_machine.transitions = staticDBManager.Query<transitions> ("SELECT T.* FROM transitions T WHERE T.state_machine_id = ?", state_machine.id);
            buildTransition (state_machine.transitions);　
            this.state = state_machine.states.Find (s => s.is_init);
            isInitialized = true;
            return this;
        } else {
            return this;
        }
    }

    private void buildTransition (List<transitions> transitions) {
        foreach (var transition in transitions) {
            transition.customer_action = staticDBManager.Query<customer_actions> ("SELECT CA.* FROM customer_actions CA WHERE CA.transition_id = ?", transition.id).First ();
            transition.trigger = staticDBManager.Query<triggers> ("SELECT T.* FROM triggers T WHERE T.transition_id = ?", transition.id).FirstOrDefault ();
            if (transition.trigger != null) {
                transition.trigger.trigger_keywords = staticDBManager.Query<trigger_keywords> ("SELECT TK.* FROM trigger_keywords TK WHERE TK.trigger_id = ?", transition.trigger.id);
                transition.trigger.trigger_sentences = staticDBManager.Query<trigger_sentences> ("SELECT TS.* FROM trigger_sentences TS WHERE TS.trigger_id = ?", transition.trigger.id);
            }
        }
    }

    /// <summary>
    /// init state -> start state の遷移 / init state と start steteは必須
    /// </summary>
    /// <returns>startできた場合true, できなかった場合falseを返す.</returns>
    public bool start () {
        if (state is null || state?.nextTransitions is null) {
            GameScene.instance.nextPhase ();
            return false;
        } else {
            transition (state.nextTransitions.First ());
            FindObjectOfType<DebugTransitionButtons> ()?.updateContent (this.state.nextTransitions.Select ((t) => t.id).ToArray ());
            return true;
        }
    }

    public bool handle (int transition_id) {
        transitions match_transition;
        if ((match_transition = state.nextTransitions.Find (nt => nt.id == transition_id)) is null) {
            return false;
        } else {
            transition (match_transition);
            FindObjectOfType<DebugTransitionButtons> ().updateContent (this.state.nextTransitions.Select ((t) => t.id).ToArray ());
            return true;
        }
    }

    /// <summary>
    /// 入力された文字列に応じてStateMachineを遷移する.
    /// 正常に遷移した場合true, 入力にマッチする遷移がなかった場合, falseを返す.
    /// </summary>
    /// <param name="recognitionResult">音声の認識結果</param>
    /// <returns>遷移が行われた場合true, 行われなかった場合falseを返す.</returns>
    public bool handle (RecognitionResult result) {
        transitions match_transition = getMatchedTransition (result.getKeywords ());
        if (match_transition is null) {
            return false;
        } else {
            prev_transition = match_transition;
            transition (match_transition);
            return true;
        }
    }

    private void transition (transitions transition) {
        prev_transition = transition;
        state = state_machine.states.Find (state => state.id == transition.next_state_id);
        foreach (var id in state.nextTransitions.Select (nt => nt.id))
            Debug.Log ("nt:" + id);
    }
    private transitions getMatchedTransition (Keywords recognized) {
        return state.nextTransitions
            .FirstOrDefault (t => {
                Keywords triggerWords = new Keywords (t.trigger.trigger_keywords.Select (tk => tk.word).ToArray ());
                return recognized.include (triggerWords);
            });
    }
}
