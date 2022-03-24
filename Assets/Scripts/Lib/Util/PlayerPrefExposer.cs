using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefExposer : MonoBehaviour {

    public string input;
    public int intOutput;
    public float floatOutput;
    public string stringOutput;

    void Update () {
        intOutput = PlayerPrefs.GetInt (input);
        floatOutput = PlayerPrefs.GetFloat (input);
        stringOutput = PlayerPrefs.GetString (input);
    }
}
