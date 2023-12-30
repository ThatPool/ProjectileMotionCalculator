using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectMovement : MonoBehaviour
{
    public float baseMoveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float maxMoveSpeed = 20f;
    public float minMoveSpeed = 1f;

    void Update()
    {
        if (!Global.EditMode && !EventSystem.current.currentSelectedGameObject)
        {
            // Adjust movement speed based on scroll input
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            baseMoveSpeed += scrollInput * 5f;
            baseMoveSpeed = Mathf.Clamp(baseMoveSpeed, minMoveSpeed, maxMoveSpeed);

            // Move forward
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * baseMoveSpeed * Time.deltaTime);
            }

            // Move backward
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * baseMoveSpeed * Time.deltaTime);
            }

            // Move left (relative to current forward direction)
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * baseMoveSpeed * Time.deltaTime);
            }

            // Move right (relative to current forward direction)
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * baseMoveSpeed * Time.deltaTime);
            }

            // Move upward
            if (Input.GetKey(KeyCode.Space))
            {
                transform.Translate(Vector3.up * baseMoveSpeed * Time.deltaTime);
            }

            // Move downward
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.down * baseMoveSpeed * Time.deltaTime);
            }

            // Rotate object based on horizontal input
            float horizontalInput = Input.GetAxis("Horizontal");
            //transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        }

    }
}
