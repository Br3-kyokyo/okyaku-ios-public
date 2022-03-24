using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour {

    [HideInInspector]
    public int category_id; //initialize by CategoryPanelController#Start
    public TMPro.TextMeshProUGUI progressBarTextArea;
    public Image progressBar;

    void Start () {
        var cateogry_scenarios = MenuScene.instance.staticDBManager.Query<Models.StaticDB.state_machines> ("SELECT SM.* FROM state_machines SM WHERE SM.scenario_category_id = ?", category_id);
        var denominator = cateogry_scenarios.Count;

        var cleared_scenario_statuses = MenuScene.instance.dynamicDBManager.Query<Models.DynamicDB.ScenarioStatus> (SQLWhereInCommandBuilder ("SS.*", "ScenarioStatus SS", "SS.state_machine_id", cateogry_scenarios.Select (t => t.id), "AND SS.is_cleared = 1"));
        var numerator = cleared_scenario_statuses.Count;
        progressBarTextArea.text = numerator + "/" + denominator;
        progressBar.fillAmount = (float)numerator / (float)denominator;
    }

    public void onClick () {
        MenuScene.instance.ScenarioPanel.GetComponent<ScenarioPanelController> ().initialize (category_id);
        MenuScene.instance.pageGoto (MenuScene.Pages.Scenario);
    }

    private string SQLWhereInCommandBuilder (string select, string from, string where, IEnumerable<int> ids, string rest) {
        var query = new StringBuilder ();
        query.Append ("SELECT ");
        query.Append (select);
        query.Append (" FROM ");
        query.Append (from);
        query.Append (" WHERE ");
        query.Append (where);
        query.Append (" IN(");
        query.Append (string.Join (",", ids));
        query.Append (") ");
        query.Append (rest);

        Debug.Log (query.ToString ());
        return query.ToString ();
    }
}
