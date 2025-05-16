using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 2f;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRange)
        {
            GameManager.instance.StartDetection();
        }
        else
        {
            GameManager.instance.StopDetection();
        }
    }
}
