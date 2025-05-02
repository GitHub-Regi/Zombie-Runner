using UnityEngine;

public class PickupMovement : MonoBehaviour
{
    [SerializeField] float xRotation;
    [SerializeField] float yRotation;
    [SerializeField] float zRotation;

    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed;

    Vector3 startPosition;
    Vector3 endPosition;
    float movementFactor = 0f;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;   
    }

    void Update()
    {
        RotatePickup();
        UpAndDown();

    }

    void RotatePickup()
    {
        Vector3 rotation = new Vector3(xRotation, yRotation, zRotation);
        transform.Rotate(rotation * Time.deltaTime);
    }

    void UpAndDown()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
    }
}
