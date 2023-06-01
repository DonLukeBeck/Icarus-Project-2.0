using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

[ExecuteInEditMode]
public class HUD_focal : MonoBehaviour
{
    [Tooltip("Target camera to retrieve physical data from.")]
    public Camera HDRPCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Camera targetCamera;

        if (HDRPCamera == null)
            targetCamera = Camera.main;
        else
            targetCamera = HDRPCamera;

        if (targetCamera == null)
            return;

        var camInfo1 = targetCamera;
        var text     = gameObject.GetComponent<Text>();

        if (camInfo1 != null  && text != null)
        {
            float f = camInfo1.focalLength;

            text.text = "";

            text.text += f;
            if (Mathf.Floor(f) == f) text.text += ".0";

            text.text += " mm ";
        }
    }
}
