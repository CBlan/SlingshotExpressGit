using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingLine : MonoBehaviour {

    public GameObject player;
    public GameObject slingEnd;
    private LineRenderer line;
    public Color col;
    private SlingController slingC;

    float colValue;

    void Start ()
    {
        line = GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        line.SetPosition(1, player.transform.position);
        line.SetPosition(2, slingEnd.transform.position);
        slingC = player.GetComponent<SlingController>();
    }
	
	void Update ()
    {
        if(player != null)
        {
            if (player.transform.position.z < transform.position.z)
            {
                Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y - 0.6f, player.transform.position.z - 0.6f);
                line.SetPosition(1, pos);
            }

            else
            {
                line.SetPosition(1, (transform.position + slingEnd.transform.position) * 0.5f);
            }
        }
        if(player == null)
        {
            player = GameManager.GM.player;
            slingC = GameManager.GM.player.GetComponent<SlingController>();
        }
        line.SetPosition(0, transform.position);
        line.SetPosition(2, slingEnd.transform.position);

        col = Color.HSVToRGB((0.5f - ((float)slingC.slingPower / 100)) / 2, 1, 1);
        line.startColor = col;
        line.endColor = col;
    }



}
