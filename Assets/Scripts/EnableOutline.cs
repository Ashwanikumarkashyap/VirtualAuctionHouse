using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOutline : MonoBehaviour
{
    public GameObject gameObject;
    void Start()
    {
        var outline = gameObject.GetComponent<Outline>();
        outline.enabled = false;
    }
    public void enableOutline()
    {
        var outline = gameObject.GetComponent<Outline>();
        outline.enabled = true;
    }

    public void disableOutline()
    {
        var outline = gameObject.GetComponent<Outline>();
        outline.enabled = false;
    }
}