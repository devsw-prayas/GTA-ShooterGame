using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public GunData gunData;
    float fireDelay;
    float timeSinceLastShot = 10;
    int bullets, mags, magSize;
    public bool isReloading = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Transform cam;
    void Start()
    {
        fireDelay = 60 / gunData.rpm;
        bullets = gunData.bullets;
        mags = gunData.magSize;
        magSize = bullets;
        cam = transform.parent.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading) return;

        bool shootInput = gunData.automatic ? Input.GetMouseButton(0): Input.GetMouseButtonDown(0);
        if (shootInput && timeSinceLastShot > fireDelay) {
            if (bullets > 0) shoot();
            else StartCoroutine(reload());
        }
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R)) StartCoroutine(reload());
    }

    void shoot() {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitInfo, gunData.range)) {
            Transform body = hitInfo.transform;
            if (body.CompareTag("Enemy")) {
                body.GetComponent<EnemyScript>().applyDamage(5f * (hitInfo.point - cam.transform.position).normalized, gunData.damage);
            }
        }
        bullets--;
        timeSinceLastShot = 0;
    }

    public int getBullets() {
        return bullets;
    }

    public int getMags() {
        return mags;
    }

    IEnumerator reload() {
        isReloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        int loads = Mathf.Min(mags, magSize);
        bullets = loads;
        mags -= loads;
        isReloading = false;
    }
}
