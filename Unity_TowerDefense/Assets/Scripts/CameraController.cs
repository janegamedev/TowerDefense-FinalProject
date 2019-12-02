using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f;

    private void Update()
        {
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate( Vector3.up * rotationSpeed);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate( Vector3.up * -rotationSpeed);
            }
        }
}
