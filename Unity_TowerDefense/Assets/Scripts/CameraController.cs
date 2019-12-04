using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 30f;

    private void Update()
        {
            transform.position = transform.position  + Vector3.right *  Input.GetAxis("Horizontal") * movementSpeed;
            transform.position = transform.position  + Vector3.forward *  Input.GetAxis("Vertical") * movementSpeed;
            
            /*if (Input.GetAxis("Horizontal") != 0)
            {
                transform.Rotate( Vector3.up * rotationSpeed);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate( Vector3.up * -rotationSpeed);
            }*/
        }
}
