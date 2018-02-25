using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{

    public int successScore;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Target")
        {
            GameManager.GM.scoreManager.TargetHit(successScore);
            GameManager.GM.scoreManager.targetsHit++;
            collision.gameObject.transform.parent.gameObject.SetActive(false);
            GameManager.GM.scoreManager.PackageDelivered();
        }
    }
}
