using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnTick : MonoBehaviour {

    public GameObject colliders;
    public GameObject tick;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!colliders.activeSelf)
        {
            tick.SetActive(true);
        }
	}
}
