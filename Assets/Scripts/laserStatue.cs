using UnityEngine;

public class laserStatue : MonoBehaviour
{
    public Transform laserPoint;
    public LineRenderer line;
    bool playerInside;
    Vector3 playerPos;
    Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line.SetPosition(0, laserPoint.position);
        line.SetPosition(1, laserPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInside) {
            playerPos = player.transform.position;
            if (Physics.Raycast(laserPoint.position, playerPos - laserPoint.position, out RaycastHit hitInfo, 30)) {
                if (hitInfo.transform.CompareTag("Player")) {
                    hitInfo.transform.GetComponent<Health>().applyDamage(0.1f);
                    line.SetPosition(1, hitInfo.point);
                } else {
                    line.SetPosition(1, laserPoint.position);
                }
            }
        } else {
            line.SetPosition(1, laserPoint.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")) {
            playerInside = true;
            player = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player")) playerInside = false;
    }
}
