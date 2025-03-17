using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float health = 200.0f;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void applyDamage(Vector3 kickback, float damage) {
        rb.AddForce(kickback, ForceMode.Impulse);
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
}
