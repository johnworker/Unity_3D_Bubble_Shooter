using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Color bubbleColor;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        bubbleColor = renderer.material.color;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            Bubble otherBubble = collision.gameObject.GetComponent<Bubble>();
            if (otherBubble != null && otherBubble.bubbleColor == bubbleColor)
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
                FindObjectOfType<ScoreManager>().AddScore(10);
            }
        }
    }
}
