using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShooter : MonoBehaviour
{
    public GameObject[] bubblePrefabs;
    public float shootForce = 10f;
    public float maxShootAngle = 70f;

    private GameObject nextBubble;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        PrepareNextBubble();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootBubble();
        }

        // 更新準星位置
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, -maxShootAngle, maxShootAngle);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void ShootBubble()
    {
        if (nextBubble == null) return;

        Vector3 shootDirection = transform.right;
        nextBubble.transform.position = transform.position;
        Rigidbody2D rb = nextBubble.GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
        rb.AddForce(shootDirection * shootForce, ForceMode2D.Impulse);

        nextBubble = null;
        PrepareNextBubble();
    }

    void PrepareNextBubble()
    {
        int randomColor = Random.Range(0, bubblePrefabs.Length);
        nextBubble = Instantiate(bubblePrefabs[randomColor], transform.position, Quaternion.identity);
        nextBubble.GetComponent<Rigidbody2D>().isKinematic = true;
    }
}