using UnityEngine;

public class RotatingLightG : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created public Transform centerPoint;
    public Transform centerPoint;
    public float rotationSpeed = 30f;

    void Update()
    {
        if (centerPoint != null)
            transform.RotateAround(centerPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}