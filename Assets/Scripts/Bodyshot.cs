using UnityEngine;

public class Bodyshot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) //By Anshu
    {
        if (other.CompareTag("Bullet"))
        {
            GetComponentInParent<Target>().HitBody();
            Destroy(other.gameObject); 
        }
    }
}

