using UnityEngine;
using System.Collections;
using TMPro;

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
    public float inaccuracy;
    float currentInaccuracy;

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
        currentInaccuracy = (1 - gunData.accuracy) * inaccuracyFactor;
        if (Input.GetKey(KeyCode.LeftControl)) currentInaccuracy = (1 - gunData.crouchAccuracy) * inaccuracyFactor;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        currentInaccuracy *= 1 + x*x * 0.5f + z*z;
        
        inaccuracy = Mathf.Lerp(inaccuracy, currentInaccuracy, 10 * Time.deltaTime);
        
        if (isReloading) return;
        
        firePoint = transform.GetChild(0).position;

        bool shootInput = gunData.automatic ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0);
        if (shootInput && timeSinceLastShot > fireDelay) {
            if (bullets > 0) shoot();
            else StartCoroutine(reload());
        }
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R)) StartCoroutine(reload());
    }

    void shoot() {
        for (int i = 0; i < gunData.shotCount; i++) {
            inaccuracy += currentInaccuracy;
            float delx = Random.Range(-inaccuracy, inaccuracy);
            float dely = Random.Range(-inaccuracy, inaccuracy);
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
                    EnemyScript parent = body.GetComponentInParent<EnemyScript>();
                    if (parent) parent.applyDamage(5f * (hitInfo.point - cam.transform.position).normalized, dmg, false);
                } else if (body.CompareTag("Head")) {
                    dmg *= 4;
                    Vector2 point = cam.GetComponent<Camera>().WorldToScreenPoint(hitInfo.point);
                    GameObject indicator = Instantiate(damageIndicator, canvas);
                    indicator.GetComponent<RectTransform>().position = point;
                    indicator.GetComponent<TextMeshProUGUI>().text = dmg.ToString();
                    uIScript.addScore(30);
                    EnemyScript parent = body.GetComponentInParent<EnemyScript>();
                    if (parent) parent.applyDamage(5f * (hitInfo.point - cam.transform.position).normalized, dmg, true);
                }
            } else {
                TrailRenderer trailRenderer = Instantiate(trail, firePoint, Quaternion.identity);
                StartCoroutine(SpawnTrail(trailRenderer, cam.transform.position + inaccurateShot * gunData.range));
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
        inaccuracy = 0;
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
