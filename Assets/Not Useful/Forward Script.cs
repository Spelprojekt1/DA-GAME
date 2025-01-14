using UnityEngine;

public class GoForward : MonoBehaviour
{
    public float speed = 5f; // Speed of the object

    void Update()
    {
        // Move the object forward along its local Z-axis
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
