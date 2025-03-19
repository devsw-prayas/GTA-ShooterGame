using UnityEngine;

public class damageIndicator : MonoBehaviour
{
    Vector2 dir;
    float speed;
    RectTransform tr;
    float timePassed = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dir.x = Random.value * 2 - 1;
        dir.y = Random.value * 2 - 1;
        speed = Random.Range(50, 100);

        tr = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        tr.anchoredPosition += speed * Time.deltaTime * dir;
        float t = Mathf.Cos(timePassed * Mathf.PI / 2);
        tr.localScale = new Vector3(t, t, t);
        timePassed += Time.deltaTime;
        if (timePassed >= 1) Destroy(gameObject);
    }
}
