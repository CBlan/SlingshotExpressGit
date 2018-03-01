using UnityEngine;

public class SlingMove : MonoBehaviour
{
    public float moveSpeed;
    private bool hasShot = false;
    public float slingMaxUp = 13;
    public float slingMaxDown = 3;


    void Update()
    {

        if (GameManager.GM.pauseManager.isPaused == true)
        {
            return;
        }


        //if (!hasShot)
        //{
            //var x = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
            var y = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

            transform.Translate(0, y, 0);

            if (transform.position.y < slingMaxDown)
            {
                transform.position = new Vector3(transform.position.x, slingMaxDown, transform.position.z);
            }

            if (transform.position.y > slingMaxUp)
            {
                transform.position = new Vector3(transform.position.x, slingMaxUp, transform.position.z);
            }
        //}

        //if (Input.GetButtonDown("Fire1"))
        //{
        //    hasShot = true;
        //}

        //if (Input.GetButtonUp("Fire1"))
        //{
        //    hasShot = false;
        //}
    }
}