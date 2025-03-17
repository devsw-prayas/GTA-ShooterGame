using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public Transform bulletLabelTr, magLabelTr;
    public TextMeshProUGUI bulletLabel, magLabel;
    Gun currentGun;

    void Start() {
        currentGun = transform.GetChild(0).GetComponent<Gun>();
        bulletLabel = bulletLabelTr.GetComponent<TextMeshProUGUI>();
        magLabel = magLabelTr.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (currentGun.isReloading) {
            bulletLabel.text = "...";
            return;
        }
        bulletLabel.text = currentGun.getBullets().ToString();
        magLabel.text = currentGun.getMags().ToString();
    }
}
