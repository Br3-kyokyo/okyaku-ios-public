using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CategoryPanelController : MonoBehaviour {

    public GameObject CategoryButtonPrefab;

    // Start is called before the first frame update
    void Start () {
        var scenariocategories = MenuScene.instance.staticDBManager.Query<Models.StaticDB.scenario_categories> ("SELECT * FROM scenario_categories");
        foreach (var category in scenariocategories.Select ((value, index) => new { value, index })) {

            GameObject button = Instantiate (CategoryButtonPrefab, gameObject.transform);
            button.GetComponent<CategoryButton> ().category_id = category.value.id;

            button.transform.localPosition += new Vector3 (0, -250 * category.index, 0);
            button.GetComponentInChildren<TextMeshProUGUI> ().text = category.value.info;
        }
    }
}
