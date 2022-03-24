using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScenarioPanelController : MonoBehaviour {
    public GameObject ScenarioButtonPrefab;

    public void initialize (int category_id) {

        foreach (Transform n in gameObject.transform) {
            GameObject.Destroy (n.gameObject);
        }

        var scenarios = MenuScene.instance.staticDBManager.Query<Models.StaticDB.state_machines> ("SELECT SM.* FROM state_machines SM WHERE scenario_category_id = ? ORDER BY position ASC", category_id);

        foreach (var scenario in scenarios.Select ((value, index) => new { value, index })) {

            GameObject button = Instantiate (ScenarioButtonPrefab, gameObject.transform);

            var scenarioButton = button.GetComponent<ScenarioButton> ();
            scenarioButton.category_id = category_id;
            scenarioButton.position = scenario.index;
            scenarioButton.category_scenario_num = scenarios.Count;

            button.transform.localPosition += new Vector3 (0, -250 * scenario.index, 0);
            button.GetComponentInChildren<TextMeshProUGUI> ().text = scenario.value.name;
        }
    }
}
