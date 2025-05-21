using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject instructionUI;

    private void Start()
    {
        instructionUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            instructionUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            instructionUI.SetActive(false);
        }
    }
}