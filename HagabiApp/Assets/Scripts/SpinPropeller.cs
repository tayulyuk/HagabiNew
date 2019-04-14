using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPropeller : MonoBehaviour
{
    public bool isTurn = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(isTurn)
		transform.Rotate(0,0,150 * Time.deltaTime);
	}
}
