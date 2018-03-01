using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnTick : MonoBehaviour {

    public GameObject colliders;
    public GameObject tick;
	
	void Update () {

		if (!colliders.activeSelf)
        {
            tick.SetActive(true);
        }
	}
}
