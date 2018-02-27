using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingController : MonoBehaviour
{

    private Rigidbody rB;
    public Transform slingMiddle;
    public GameObject arc;
    public int slingPower;
    public int maxSlingPower;
    public float slingAngle;
    public int slingPowerPerTime = 1;
    public int throwPowerPerTime = 1;
    private bool fireing;

    //Package Settings
    public GameObject package;
    float throwStrength;
    public bool canThrow;

    //ground detection
    private float distToGround;
    Collider m_Collider;

    public AudioClip DrawbackSounds;
    public GameObject drawSound;
    public AudioClip[] flyingSounds;
    private GameObject flySound;

    public GameObject airSpeedEffect;
    private GameObject airSpeed;

    void Start()
    {
        rB = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
        distToGround = m_Collider.bounds.extents.y;
        //slingMiddle = GameManager.GM.levelManager.slingMid;

        if (GameManager.GM != null)
        {
            GameManager.GM.levelManager.ground.tag = "Ground";
        }
    }


    void Update()
    {

        Vector3 pos = transform.position;
        Vector3 dir = (slingMiddle.position - transform.position).normalized;

        var offset = slingMiddle.position.y - transform.position.y;
        Vector3 i = slingMiddle.position - new Vector3(0, offset, 0);

        slingAngle = Vector3.Angle(i - transform.position, slingMiddle.position - transform.position);

        Debug.DrawLine(transform.position, i, Color.blue);
        Debug.DrawLine(transform.position, slingMiddle.position, Color.red);

        //Debug.DrawRay(pos, (slingMiddle.position - transform.position) * 10000, Color.yellow, 0.1f);

        if (GameManager.GM.pauseManager.isPaused == true)
        {
            return;
        }

        if (!canThrow)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                slingPower = 1;
                fireing = true;
                //arc.SetActive(true);
                StartCoroutine("SlingPower");
                drawSound = GameManager.GM.soundManager.PlaySoundLoop(DrawbackSounds, transform.position);
            }

            if (Input.GetButtonUp("Fire1"))
            {
                StopCoroutine("SlingPower");
                fireing = false;
                arc.SetActive(false);
                rB.velocity = dir * slingPower;
                canThrow = true;
                //print(slingPower);
                drawSound.GetComponent<AudioPlayer>().StopSound();
                flySound = GameManager.GM.soundManager.PlaySoundLoop(flyingSounds[Random.Range(0, 4)], transform.position);
                airSpeed = Instantiate(airSpeedEffect, transform);

                GameManager.GM.levelManager.ground.tag = "Untagged";

            }
        }

        if (airSpeed != null)
        {
            airSpeed.transform.rotation = Quaternion.LookRotation(transform.InverseTransformDirection(rB.velocity.normalized));
        }

        if (fireing)
        {
            transform.Translate(0, 0, -slingPowerPerTime * Time.deltaTime);
            if (transform.position.z < 11)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 11);
            }
        }

        if (canThrow && package != null && !IsGrounded())
        {
            if (Input.GetButtonDown("Fire2"))
            {
                throwStrength = 0f;
                StartCoroutine("ThrowPower");
            }

            if (Input.GetButtonUp("Fire2"))
            {
                StopCoroutine("ThrowPower");
                ThrowPackage();
                //Throw package
                //canThrow = false;
            }
        }

    }

    void ThrowPackage()
    {
        var rb = package.GetComponent<Rigidbody>();

        rb.isKinematic = false;
        rb.velocity = rB.velocity;
        package.transform.SetParent(null);
        package.GetComponent<BoxCollider>().isTrigger = false;
        rb.AddForce(Camera.main.transform.forward * throwStrength, ForceMode.Impulse);
        package = null;


    }

    IEnumerator SlingPower()
    {
        slingPower += slingPowerPerTime;
        if (slingPower > maxSlingPower)
        {
            slingPower = maxSlingPower;
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine("SlingPower");
        yield break;
    }

    IEnumerator ThrowPower()
    {
        throwStrength += throwPowerPerTime;

        yield return new WaitForSeconds(0.1f);
        StartCoroutine("ThrowPower");
        yield break;
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (flySound != null)
        {
            flySound.GetComponent<AudioPlayer>().StopSound();
        }

        if (airSpeed != null)
        {
            Destroy(airSpeed);
        }
    }
}
