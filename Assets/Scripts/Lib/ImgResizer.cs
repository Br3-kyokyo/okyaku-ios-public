using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImgResizer : MonoBehaviour {

    private Image image;
    private float display;
    private float displayRatio;
    private float imgRatio;
    // Start is called before the first frame update
    void Awake () {
        image = gameObject.GetComponent<Image> ();
        displayRatio = (Screen.currentResolution.width / Screen.currentResolution.height);
        imgRatio = (image.sprite.textureRect.width / image.sprite.textureRect.height);

        Debug.Log (Screen.currentResolution.width);
        Debug.Log (Screen.currentResolution.height);
    }

    // Update is called once per frame
    void Update () {
        if (displayRatio > imgRatio) { }
    }
}
