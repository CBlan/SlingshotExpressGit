using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public CameraCollision col;
    public Transform zoomPoint;
    public SmoothMouseLook mouseLook;

    private void Start()
    {
        //zoomPoint = GameManager.GM.levelManager.zoom;
    }

    void Update () {
		
        if (col.playerCollision)
        {
            if (transform.childCount > 0)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                //Destroy(transform.GetChild(0));
            }
            
            transform.SetParent(null);
            mouseLook.enabled = false;
            //col.gameObject.SetActive(false);
            transform.LookAt(col.colPoint);
            transform.position = Vector3.Lerp(transform.position, zoomPoint.position, 0.05f);

        }

	}
}
