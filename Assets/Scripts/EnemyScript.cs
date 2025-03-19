using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float health = 200.0f;
    Rigidbody rbHead;
    Rigidbody rbBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbHead = transform.GetChild(0).GetComponent<Rigidbody>();
        rbBody = transform.GetChild(1).GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void applyDamage(Vector3 kickback, float damage, bool isHead) {
        if (isHead) rbHead.AddForce(kickback, ForceMode.Impulse);
        else rbBody.AddForce(kickback, ForceMode.Impulse);
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
}
