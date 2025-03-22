using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public TextMeshProUGUI bulletLabel, magLabel, scoreLabel, timeLabel, healthLabel;
    Gun currentGun;
    public Transform player;
    Health playerHealth;

    int score = 0;
    float timeLeft = 60.0f;

    void Start() {
        currentGun = transform.GetChild(0).GetComponent<Gun>();
        playerHealth = player.GetComponent<Health>();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (currentGun.isReloading) {
            bulletLabel.text = "...";
            return;
        }
        bulletLabel.text = currentGun.getBullets().ToString();
        magLabel.text = currentGun.getMags().ToString();
        timeLabel.text = "Time Left:\n" + Mathf.Round(timeLeft).ToString() + "s";
        healthLabel.text = "Health: " + Mathf.Round(playerHealth.health).ToString();
    }

    public void addScore(int s) {
        score += s;
        scoreLabel.text = "Score: " + score.ToString();
    }
}
