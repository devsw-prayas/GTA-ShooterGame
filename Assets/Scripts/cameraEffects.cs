using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class cameraEffects : MonoBehaviour
{
    public float amount = 0.002f;
    public float frequency = 10.0f;
    public float smooth = 10.0f;
    public float knockbackFactor = 0.08f;
    float shakeAmount = 0;
    public float sustain = 10f;
    public Transform weaponHolder;

    Vector3 startPosition;
    Vector3 weaponRot;
    float orgWeaponPositionY;
    float lastYpos;
    public float jumpEffectIntensity = 10;
    float jumpEffect = 0;
    public playerMovement movement;

    void Start()
    {
        startPosition = transform.localPosition;
        orgWeaponPositionY = weaponHolder.localPosition.y;
        lastYpos = transform.position.y;
    }
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float shk = Random.Range(-1f, 1f) * shakeAmount / 2 * 0.3f;
        jumpEffect = Mathf.Lerp(jumpEffect, movement.velocity.y, 0.4f);
        jumpEffect *= jumpEffectIntensity;
        float k = -shakeAmount + jumpEffect;
        transform.localEulerAngles = new Vector3(k, 0, shk + Mathf.LerpAngle(transform.eulerAngles.z, x * -2, 0.02f));

        weaponHolder.transform.localEulerAngles = new Vector3(shakeAmount, 0, 0);
        shk *= knockbackFactor;
        weaponHolder.localPosition = new Vector3(shk, orgWeaponPositionY + shk, -shakeAmount);

        if (x != 0 || z != 0) {
            headBob();
        } else {
            stopBob();
        }

        shakeAmount = Mathf.Lerp(shakeAmount, 0, sustain * Time.deltaTime);
        lastYpos = transform.position.y;
    }

    void headBob() {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amount * 1.4f, smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.y, Mathf.Cos(Time.time * frequency/2) * amount * 1.6f, smooth * Time.deltaTime);
        transform.localPosition += pos;
    }

    void stopBob() {
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, smooth * Time.deltaTime);
    }

    public void shake(float amount) {
        shakeAmount += amount;
    }
}
