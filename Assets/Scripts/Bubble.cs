using UnityEngine;

public class Bubble : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bubble")
        {
            Destroy(gameObject);
        }
    }
}
