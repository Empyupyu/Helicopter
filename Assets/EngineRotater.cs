using UnityEngine;

public class EngineRotater : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float angleMultiply;

    [SerializeField, Header("AngleConstraints")] private float maxAngle;
    [SerializeField] private float minAngle;

    public void RotateEngine(Quaternion helicopter)
    {
        var dir = helicopter;
        dir.x = dir.x * angleMultiply;
        dir.y = 0;
        dir.z = 0;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, dir, this.speed * Time.deltaTime);
        ClampRotation();
    }

    private void ClampRotation()
    {
        var clampAngles = transform.localEulerAngles;

        clampAngles.x = (clampAngles.x > 180) ? clampAngles.x - 360 : clampAngles.x;
        clampAngles.x = Mathf.Clamp(clampAngles.x, minAngle, maxAngle);

        transform.localRotation = Quaternion.Euler(clampAngles);
    }
}
