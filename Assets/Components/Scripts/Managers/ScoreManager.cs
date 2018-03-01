using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public GameObject retryPanel;
    public GameObject resultsPanel;
    public GameObject targetHitUI;


    //Results Panel
    public GameObject goodJobPanel;
    public GameObject firedPanel;

    public Text packagesDelivered;
    public int playerDeathCount;
    public Text deathCount;
    public Text moneyMade;

    //scoring
    public Text scoreText;
    public GameObject earningsTxt;
    public int score;
    bool success;

    public int targetsToHit;
    public int targetsHit;

    public void Start()
    {
        scoreText.text = score.ToString();
    }

    private void Update()
    {

        if (firedPanel.activeSelf == true || goodJobPanel.activeSelf == true || retryPanel.activeSelf == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (targetsHit == targetsToHit)
        {
            firedPanel.SetActive(false);
            goodJobPanel.SetActive(true);
            Finish();
            //print("Passed Level");
        }
    }

    public void FailedTask(int points)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        playerDeathCount++;
        GameManager.GM.googleAnalytics.LogEvent("Player Events", "Player Death", "Death", 1);
        score -= points;
        scoreText.text = score.ToString();

        if (score < 0)
        {
            firedPanel.SetActive(true);
            goodJobPanel.SetActive(false);
            Finish();
        }
        else
        {
            retryPanel.SetActive(true);
        }
        //Retry panel
    }

    public void DeactivateUI()
    {
        retryPanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void TargetHit(int points)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        score += points;
        GameManager.GM.googleAnalytics.LogEvent("Player Events", "Player Score", "Packages Delivered", 1);
        scoreText.text = score.ToString();

        retryPanel.SetActive(true);

        success = true;

        if(targetsHit == targetsToHit)
        {
            firedPanel.SetActive(false);
            goodJobPanel.SetActive(true);
            Finish();
            print("Passed Level");
        }
        //Next Level
    }

    public void Finish()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        scoreText.gameObject.SetActive(false);
        earningsTxt.SetActive(false);
        retryPanel.SetActive(false);
        resultsPanel.SetActive(true);
        packagesDelivered.text = "Packages Delivered: " + targetsHit.ToString() + "/" + targetsToHit.ToString();
        deathCount.text = "Death Count: " + playerDeathCount.ToString();
        moneyMade.text = "Money Made: $" + score.ToString();
    }

    public void PackageDelivered()
    {
        //print("package delivered");
        targetHitUI.gameObject.SetActive(true);
        StartCoroutine("Timer");

    }


    IEnumerator Timer()
    {
        //print("timing");
        yield return new WaitForSeconds(1);
        targetHitUI.gameObject.SetActive(false);
        yield break;
    }


}
