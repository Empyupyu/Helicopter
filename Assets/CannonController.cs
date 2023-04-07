using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private float turningSpeed;
    [SerializeField] private float lookAtClickPositionTime;

    [SerializeField, Header("AngleConstraints")] private float maxAngle;
    [SerializeField] private float minAngle;

    private Vector3 clickPosition;
    private Quaternion directionRotate;
    private Quaternion originalRotate;
    private Camera camera;

    private bool lokingOnClickPoint;
    private bool hooldTargetPoint;
    private float lookAtClickPositionTimePassed;

    private void Awake()
    {
        originalRotate = transform.localRotation;
        camera = Camera.main;
    }

    private void Update()
    {
        GetClickPoint();
        HoldTargetPosition();
        LookAtPoint();
        ClampRotation();
        DelayBeforeTransitionToOriginalRotate();
    }

    private void GetClickPoint()
    {
        if (Input.GetMouseButton(0))
        {
            var mousePos = Input.mousePosition;
            var mousePositionDistanceToCannon = new Vector3(mousePos.x, mousePos.y, transform.position.z - camera.transform.position.z);

            clickPosition = camera.ScreenToWorldPoint(mousePositionDistanceToCannon);

            hooldTargetPoint = true;
            lokingOnClickPoint = true;

            lookAtClickPositionTimePassed = 0f;
        }
    }

    private void HoldTargetPosition()
    {
        if (hooldTargetPoint)
        {
            var lookDirection = clickPosition - transform.position;

            directionRotate = Quaternion.LookRotation(lookDirection.normalized);

            var eulerAngle = directionRotate.eulerAngles;

            eulerAngle.y = originalRotate.eulerAngles.y;

            directionRotate = Quaternion.Euler(eulerAngle);
        }
    }

    private void LookAtPoint()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, directionRotate, turningSpeed * Time.deltaTime);
    }

    private void ClampRotation()
    {
        var clampAngles = transform.localEulerAngles;

        clampAngles.x = (clampAngles.x > 180) ? clampAngles.x - 360 : clampAngles.x;
        clampAngles.x = Mathf.Clamp(clampAngles.x, minAngle, maxAngle);

        transform.localRotation = Quaternion.Euler(clampAngles);
    }

    private void DelayBeforeTransitionToOriginalRotate()
    {
        if (lokingOnClickPoint)
        {
            lookAtClickPositionTimePassed += Time.deltaTime;

            if (lookAtClickPositionTimePassed >= lookAtClickPositionTime)
            {
                directionRotate = originalRotate;

                hooldTargetPoint = false;
                lokingOnClickPoint = false;
            }
        }
    }
}
