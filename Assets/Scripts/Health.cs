using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void applyDamage(float damage) {
        health -= damage;
    }
}
