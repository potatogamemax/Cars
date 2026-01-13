using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float turnSpeed = 100f;
    [SerializeField] float driftFactor = 0.92f; // Чем меньше, тем сильнее дрифт
    [SerializeField] float driftInputMultiplier = 1.5f; // Усиление дрифта при резком повороте
    [SerializeField] KeyCode driftKey = KeyCode.LeftShift;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal2");
        float verticalInput = Input.GetAxis("Vertical2");

        // Движение вперед/назад
        Vector3 forwardForce = transform.forward * verticalInput * speed;
        rb.AddForce(forwardForce, ForceMode.Acceleration);

        // Поворот
        if (Mathf.Abs(verticalInput) > 0.1f)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, horizontalInput * turnSpeed * Time.fixedDeltaTime, 0));
        }

        // Дрифт: уменьшаем боковую скорость
        Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity);
        float drift = driftFactor;
        if (Mathf.Abs(horizontalInput) > 0.2f && Mathf.Abs(verticalInput) > 0.1f)
        {
            drift /= driftInputMultiplier; // Усиливаем дрифт при резком повороте
        }
        localVelocity.x *= drift;
        rb.linearVelocity = transform.TransformDirection(localVelocity);
    }
}
