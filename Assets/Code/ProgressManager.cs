using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    public GameObject[] gates;
    public GameObject[] kunais;
    public Transform[] sockets;
    public int currentStage = 0;

    public GameObject finalGate;
    public GameObject finalKey;
    public Transform finalKeySocket;

    private void Start()
    {
        UpdateKunaisVisibility();
        finalKey.SetActive(false);
    }

    void Update()
    {
        for (int i = 0; i < gates.Length; i++)
        {
            if (i == currentStage && IsCorrectKunaiInSocket(i))
            {
                OpenGate(i);
            }
        }

        if (currentStage >= 3 && !finalKey.activeSelf)
        {
            finalKey.SetActive(true);
        }

        if (IsKeyInFinalSocket())
        {
            finalGate.SetActive(false);
        }
    }

    bool IsCorrectKunaiInSocket(int index)
    {
        if (sockets[index].childCount == 0) return false;
        return sockets[index].GetChild(0).gameObject == kunais[index];
    }

    void OpenGate(int index)
    {
        gates[index].SetActive(false);
        SceneManager.LoadScene($"Stage{index + 1}");
    }

    public void ReturnFromStage()
    {
        currentStage++;
        SceneManager.LoadScene("Lobby");
    }

    void UpdateKunaisVisibility()
    {
        for (int i = 0; i < kunais.Length; i++)
        {
            kunais[i].SetActive(i == currentStage);
        }
    }

    bool IsKeyInFinalSocket()
    {
        if (finalKeySocket.childCount == 0) return false;
        return finalKeySocket.GetChild(0).gameObject == finalKey;
    }
}
