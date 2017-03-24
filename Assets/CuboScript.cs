using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboScript : MonoBehaviour {

    static CuboScript instance;

	void Start () {
        instance = this;
        print("cubo");
    }

    // Update is called once per frame
    void Update () {
        float dist = Vector3.Distance(transform.position, EsferaScript.instance.gameObject.transform.position);
        print(dist);
        if (dist < 2f) {
            GetComponent<Renderer>().material.color = Color.green;
        }
	}
}
