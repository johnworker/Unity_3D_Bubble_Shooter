using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] bubblePrefabs;
    public Transform bubbleParent;
    public float bubbleSpeed = 10f;
    public ScoreManager scoreManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootBubble();
        }
    }

    void ShootBubble()
    {
        int randomIndex = Random.Range(0, bubblePrefabs.Length);
        GameObject bubblePrefab = bubblePrefabs[randomIndex];
        GameObject bubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity, bubbleParent);
        Rigidbody rb = bubble.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bubbleSpeed;
    }
}
