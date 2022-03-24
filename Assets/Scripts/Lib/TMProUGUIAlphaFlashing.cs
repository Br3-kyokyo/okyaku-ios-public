using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMProUGUIAlphaFlashing : MonoBehaviour {

    private TextMeshProUGUI textArea;
    public float maxAlpha;
    public float speed;
    // Start is called before the first frame update

    void Start () {
        textArea = gameObject.GetComponentInChildren<TextMeshProUGUI> ();
    }

    // Update is called once per frame
    void Update () {
        textArea.alpha = maxAlpha * Mathf.Abs (Mathf.Sin (Time.time * speed));
    }
}
