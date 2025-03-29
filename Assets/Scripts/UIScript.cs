using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public TextMeshProUGUI bulletLabel, magLabel, scoreLabel, timeLabel, healthLabel;
    Gun currentGun;
    public Transform player;
    Health playerHealth;

    int score = 0;
    float timeLeft = 60.0f;
    public RectTransform left, right, up, down; //crosshair
    public float crosshairScaleFactor = 5000;

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
        setCrosshair();
    }

    public void addScore(int s) {
        score += s;
        scoreLabel.text = "Score: " + score.ToString();
    }

    void setCrosshair() {
        float displayInaccuracy = currentGun.inaccuracy * crosshairScaleFactor;
        left.localPosition = new Vector3(-10 - displayInaccuracy, 0, 0);
        right.localPosition = new Vector3(10 + displayInaccuracy, 0, 0);
        up.localPosition = new Vector3(0, -10 - displayInaccuracy, 0);
        down.localPosition = new Vector3(0, 10 + displayInaccuracy, 0);
    }
}
