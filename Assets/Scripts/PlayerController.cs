using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool hasShot = false;
    public float playerMaxLeft = -6;
    public float playerMaxRight = 6;

    void Update()
    {

        if (GameManager.GM.pauseManager.isPaused == true)
        {
            return;
        }

        if (!hasShot)
        {
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
            //var y = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

            transform.Translate(x, 0, 0);

            if (transform.position.x < playerMaxLeft)
            {
                transform.position = new Vector3(playerMaxLeft, transform.position.y, transform.position.z);
            }

            if (transform.position.x > playerMaxRight)
            {
                transform.position = new Vector3(playerMaxRight, transform.position.y, transform.position.z);
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            hasShot = true;
        }
    }
}