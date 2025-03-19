using UnityEngine;

public class Target : MonoBehaviour
{
    public int maxHealth = 100; //By Anshu
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void HitHead()
    {
        TakeDamage(50); 
    }

    public void HitBody()
    {
        TakeDamage(20); 
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
