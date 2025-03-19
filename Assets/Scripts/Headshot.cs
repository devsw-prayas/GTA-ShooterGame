using UnityEngine;

public class Headshot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) //By Anshu
    {
        if (other.CompareTag("Bullet"))
        {
            GetComponentInParent<Target>().HitHead();
            Destroy(other.gameObject);
        }
    }
}

