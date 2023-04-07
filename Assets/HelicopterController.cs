using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    public float thrust = 600f; // ���� �������
    public float tiltSpeed = 10f; // �������� ������� ���������
    public float moveSpeed = 10f; // �������� �������� � �������������� ���������
    public float maxTiltAngle = 30f; // ������������ ���� ������� 
    public float minX = -1f; // ����������� ���������� X
    public float maxX = 70f; // ������������ ���������� X
    public float minY = -18f; // ����������� ���������� y
    public float maxY = 19f; // ������������ ���������� y
    public float momentOfInertia = 1f; // ������ �������
    public KeyCode liftKey = KeyCode.Space; // ������� ��� �������

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
        // ������ ���������
        if (Input.GetKey(liftKey))
        {
            rb.AddRelativeForce(Vector3.up * thrust, ForceMode.Force);
        }

        // �������������� ���������� ���������� 2D
        // float horizontalInput = Input.GetAxis("Horizontal");
        // Vector3 movement = new Vector3(horizontalInput * moveSpeed, 0f, 0f);
        // transform.Translate(movement * Time.deltaTime);

        // �������������� ���������� ���������� � 2.5D
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput * moveSpeed, 0f, 0f);
        transform.Translate(movement * Time.deltaTime, Space.World);

        // ����������� ������
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;

        // ������ ��������� ��� �������� �����/������
        //tiltAngle = Mathf.Lerp(-maxTiltAngle, maxTiltAngle, (horizontalInput + 1f) / 2f);
        //Quaternion targetRotation = Quaternion.Euler(0f, -90f, -tiltAngle);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);

        // ������ ��������� ��� �������� �����/������
        tiltAngle = Mathf.Lerp(-maxTiltAngle, maxTiltAngle, (horizontalInput + 1f) / 2f);
        Quaternion targetRotation = Quaternion.Euler(-tiltAngle, -90f, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);

        // ������ ��������� ��� �������� �����/������
        //tiltAngle = Mathf.Lerp(-maxTiltAngle, maxTiltAngle, (horizontalInput + 1f) / 2f);
        //Quaternion targetRotation = Quaternion.Euler(0f, 0f, -tiltAngle);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);

        // ������ ��������� ��� �������� �����/������
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
