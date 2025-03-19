using System;
using Unity.VisualScripting;
using UnityEngine;

public class cameraEffects : MonoBehaviour
{
    public float amount = 0.002f;
    public float frequency = 10.0f;
    public float smooth = 10.0f;
    float shakeAmount = 0;
    public float sustain = 10f;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.eulerAngles = new Vector3(-shakeAmount, transform.eulerAngles.y, Mathf.LerpAngle(transform.eulerAngles.z, x * -2, 0.02f));

        if (x != 0 || z != 0) {
            headBob();
        } else {
            stopBob();
        }

        shakeAmount = Mathf.Lerp(shakeAmount, 0, sustain * Time.deltaTime);
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
