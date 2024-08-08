using UnityEngine;

public class CarRunning : MonoBehaviour
{
    public float speed = 10f;  // Speed at which the car moves forward

    private void Update()
    {
        // Move the car forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
