using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public TextMeshProUGUI bulletLabel, magLabel, scoreLabel, timeLabel, healthLabel;
    Gun currentGun;
    public Transform player;
    public Transform panel;
    Health playerHealth;

    bool started = false;

    int score = 0;
    float timeLeft = 60.0f;
    public RectTransform left, right, up, down; //crosshair
    public float crosshairScaleFactor = 5000;

    public GameObject pistol;
    public GameObject chainGun;
    public GameObject shotGun;

    void Start() {
        playerHealth = player.GetComponent<Health>();
    }

    void Update()
    {
        if (!started || currentGun == null) return;
        timeLeft -= Time.deltaTime;
        if (currentGun.isReloading)
        {
            bulletLabel.text = "RLDNG";
            return;
        }
        bulletLabel.text = currentGun.getBullets().ToString();
        magLabel.text = currentGun.getMags().ToString();
        timeLabel.text = "Time Left:\n" + Mathf.Round(timeLeft).ToString() + "s";
        healthLabel.text = "Health: " + Mathf.Round(playerHealth.health).ToString();
        setCrosshair();
        if (timeLeft <= 0) started = false;
    }

    public void start(int GunID)
    {
        GameObject g = pistol;
        switch (GunID)
        {
            case 0:
                g = pistol;
                break;
            case 1:
                g = chainGun;
                break;
            case 2:
                g = shotGun;
                break;
        }

        GameObject gun = Instantiate(g, transform);
        currentGun = transform.GetChild(0).GetComponent<Gun>();
        Cursor.lockState = CursorLockMode.Locked;
        started = true;
        Destroy(panel.gameObject);
    }

    public void addScore(int s)
    {
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
