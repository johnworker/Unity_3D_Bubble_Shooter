using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform bubbleParent;
    public float bubbleSpeed = 5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootBubble();
        }
    }

    void ShootBubble()
    {
        GameObject bubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity, bubbleParent);
        Rigidbody rb = bubble.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bubbleSpeed;
    }
}
