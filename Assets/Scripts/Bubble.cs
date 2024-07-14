using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Color bubbleColor;

    void Start()
    {
        // 設置泡泡顏色
        Renderer renderer = GetComponent<Renderer>();
        bubbleColor = renderer.material.color;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bubble")
        {
            Bubble otherBubble = collision.gameObject.GetComponent<Bubble>();
            if (otherBubble != null && otherBubble.bubbleColor == bubbleColor)
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}
