using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    public float thrust = 600f; // сила подъема
    public float tiltSpeed = 10f; // скорость наклона вертолета
    public float moveSpeed = 10f; // скорость движени€ в горизонтальной плоскости
    public float maxTiltAngle = 30f; // максимальный угол наклона 
    public float minX = -1f; // минимальна€ координата X
    public float maxX = 70f; // максимальна€ координата X
    public float minY = -18f; // минимальна€ координата y
    public float maxY = 19f; // максимальна€ координата y
    public float momentOfInertia = 1f; // момент инерции
    public KeyCode liftKey = KeyCode.Space; // клавиша дл€ подъема

    private Rigidbody rb;
    private float tiltAngle = 0f;

    [SerializeField] private List<EngineRotater> engineRotaters;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.inertia = momentOfInertia;
    }

    private void FixedUpdate()
    {
        // ѕодъем вертолета
        if (Input.GetKey(liftKey))
        {
            rb.AddRelativeForce(Vector3.up * thrust, ForceMode.Force);
        }

        // √оризонтальное управление вертолетом 2D
        // float horizontalInput = Input.GetAxis("Horizontal");
        // Vector3 movement = new Vector3(horizontalInput * moveSpeed, 0f, 0f);
        // transform.Translate(movement * Time.deltaTime);

        // √оризонтальное управление вертолетом в 2.5D
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput * moveSpeed, 0f, 0f);
        transform.Translate(movement * Time.deltaTime, Space.World);

        // ќграничение экрана
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;

        // Ќаклон вертолета при движении влево/вправо
        //tiltAngle = Mathf.Lerp(-maxTiltAngle, maxTiltAngle, (horizontalInput + 1f) / 2f);
        //Quaternion targetRotation = Quaternion.Euler(0f, -90f, -tiltAngle);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);

        // Ќаклон вертолета при движении влево/вправо
        tiltAngle = Mathf.Lerp(-maxTiltAngle, maxTiltAngle, (horizontalInput + 1f) / 2f);
        Quaternion targetRotation = Quaternion.Euler(-tiltAngle, -90f, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);

        // Ќаклон вертолета при движении влево/вправо
        //tiltAngle = Mathf.Lerp(-maxTiltAngle, maxTiltAngle, (horizontalInput + 1f) / 2f);
        //Quaternion targetRotation = Quaternion.Euler(0f, 0f, -tiltAngle);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);

        // Ќаклон вертолета при движении влево/вправо
        //tiltAngle = Mathf.Lerp(-maxTiltAngle, maxTiltAngle, (horizontalInput + 1f) / 2f);
        //Quaternion targetRotation = Quaternion.Euler(0f, -tiltAngle, 0f);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);


    }

    private void Update()
    {
        for (int i = 0; i < engineRotaters.Count; i++)
        {
            engineRotaters[i].RotateEngine(transform.localRotation);
        }
    }
}
