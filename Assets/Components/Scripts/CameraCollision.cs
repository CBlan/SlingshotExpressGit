using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{

    public bool playerCollision;
    public Vector3 colPoint;
    public GameObject blood;
    public GameObject[] hitParticle;
    public int hospitalBill;
    public int successsPoints;
    public GameObject[] bloodProjectors;
    public AudioClip[] impactClips;
    public AudioClip[] squelchClips;
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (!playerCollision)
        {
            if (collision.gameObject.transform.tag != "Target" && collision.gameObject.transform.tag != "Ground" && collision.gameObject.transform.tag != "Package")
            {
                playerCollision = true;
                rend.enabled = false;
                colPoint = collision.contacts[0].point;
                Instantiate(blood, colPoint, Quaternion.identity);
                Instantiate(hitParticle[Random.Range(0,2)], new Vector3(colPoint.x, colPoint.y, colPoint.z -2), Quaternion.identity);
                GameManager.GM.soundManager.PlayAndDestroy(impactClips[Random.Range(0, 9)], transform.position);
                GameManager.GM.soundManager.PlayAndDestroy(squelchClips[Random.Range(0, 2)], transform.position);
                Instantiate(bloodProjectors[Random.Range(0,2)], new Vector3(colPoint.x, colPoint.y, colPoint.z - 5), Quaternion.LookRotation(colPoint));
                GameManager.GM.scoreManager.FailedTask(hospitalBill);
            }

            if (collision.gameObject.tag == "Target")
            {
                playerCollision = true;
                GameManager.GM.scoreManager.TargetHit(successsPoints);
                if (gameObject.GetComponent<SlingController>().package != null)
                {
                    GameManager.GM.scoreManager.TargetHit(successsPoints);
                    collision.gameObject.transform.parent.gameObject.SetActive(false);
                    GameManager.GM.scoreManager.PackageDelivered();
                }

                
            }
        }

    }
}
