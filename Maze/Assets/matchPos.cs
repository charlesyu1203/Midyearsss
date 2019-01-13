using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matchPos : MonoBehaviour {
    private Transform t;
    private Transform cam;
    private void Start()
    {
        t = GameObject.Find("Capsule").transform;
        cam = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update () {
        cam = t.transform;
    }
}
