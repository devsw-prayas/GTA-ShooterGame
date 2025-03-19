using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Rendering.HighDefinition;
using Unity.VisualScripting;

public class Gun : MonoBehaviour
{
    public GunData gunData;
    float fireDelay;
    float timeSinceLastShot = 10;
    int bullets, mags, magSize;
    public bool isReloading = false;
    public GameObject damageIndicator;
    public Transform canvas;
    public TrailRenderer trail;
    public float inaccuracyFactor = 10;

    Vector3 firePoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Transform cam;
    UIScript uIScript;

    void Start()
    {
        fireDelay = 60 / gunData.rpm;
        bullets = gunData.bullets;
        mags = gunData.magSize;
        magSize = bullets;
        cam = transform.parent.parent;
        firePoint = transform.GetChild(0).position;
        uIScript = transform.GetComponentInParent<UIScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading) return;
        
        firePoint = transform.GetChild(0).position;

        bool shootInput = gunData.automatic ? Input.GetMouseButton(0): Input.GetMouseButtonDown(0);
        if (shootInput && timeSinceLastShot > fireDelay) {
            if (bullets > 0) shoot();
            else StartCoroutine(reload());
        }
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R)) StartCoroutine(reload());
    }

    void shoot() {
        for (int i = 0; i < gunData.shotCount; i++) {
            float k = (1 - gunData.accuracy) * inaccuracyFactor;
            float delx = Random.Range(-k, k);
            float dely = Random.Range(-k, k);
            Vector3 inaccurateShot = cam.transform.forward + cam.transform.right * delx + cam.transform.up * dely;
            if (Physics.Raycast(cam.transform.position, inaccurateShot, out RaycastHit hitInfo, gunData.range)) {
                Transform body = hitInfo.transform;
                float dmg = gunData.damage;
                TrailRenderer trailRenderer = Instantiate(trail, firePoint, Quaternion.identity);
                StartCoroutine(SpawnTrail(trailRenderer, hitInfo.point));
                if (body.CompareTag("Enemy")) {
                    Vector2 point = cam.GetComponent<Camera>().WorldToScreenPoint(hitInfo.point);
                    GameObject indicator = Instantiate(damageIndicator, canvas);
                    indicator.GetComponent<RectTransform>().position = point;
                    indicator.GetComponent<TextMeshProUGUI>().text = dmg.ToString();
                    uIScript.addScore(10);
                    body.GetComponentInParent<EnemyScript>().applyDamage(5f * (hitInfo.point - cam.transform.position).normalized, dmg, false);
                } else if (body.CompareTag("Head")) {
                    dmg *= 2;
                    Vector2 point = cam.GetComponent<Camera>().WorldToScreenPoint(hitInfo.point);
                    GameObject indicator = Instantiate(damageIndicator, canvas);
                    indicator.GetComponent<RectTransform>().position = point;
                    indicator.GetComponent<TextMeshProUGUI>().text = dmg.ToString();
                    uIScript.addScore(30);
                    body.GetComponentInParent<EnemyScript>().applyDamage(5f * (hitInfo.point - cam.transform.position).normalized, dmg, true);
                }
            } else {
                TrailRenderer trailRenderer = Instantiate(trail, firePoint, Quaternion.identity);
                StartCoroutine(SpawnTrail(trailRenderer, inaccurateShot * gunData.range));
            }
        }
        cam.GetComponentInParent<cameraEffects>().shake(gunData.kickback);
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

    IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 finalPos) {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1) {
            Trail.transform.position = Vector3.Lerp(startPosition, finalPos, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }

        Trail.transform.position = finalPos;
        Destroy(Trail.gameObject, Trail.time);
    }
}
