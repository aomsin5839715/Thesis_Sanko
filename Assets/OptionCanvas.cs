using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionCanvas : MonoBehaviour
{
    public Canvas optionCanvas;

    // Start is called before the first frame update
    void Start()
    {
        optionCanvas = GetComponent<Canvas>();
        optionCanvas.enabled = false;
    }
}
