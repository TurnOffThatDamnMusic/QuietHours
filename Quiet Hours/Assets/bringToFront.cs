using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bringToFront : MonoBehaviour {

    void OnEnable()
    {
        transform.SetAsLastSibling();
    }
    public int getIndex()
    {
        return transform.GetSiblingIndex();
    }
}