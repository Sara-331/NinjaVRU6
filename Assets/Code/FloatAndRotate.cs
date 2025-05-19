using UnityEngine;

public class FloatAndRotate : MonoBehaviour
{
    public float floatHeight = 0.5f;      
    public float floatSpeed = 1f;        
    public float rotationSpeed = 30f;     

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
     
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

     
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
