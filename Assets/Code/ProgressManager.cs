using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager instance;

    public GameObject[] gates;
    public GameObject[] kunais;
    public int currentStage = 0;

    public GameObject finalGate;
    public GameObject finalKey;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateProgress();
    }

    void Update()
    {
        if (currentStage >= 2 && finalKey != null && !finalKey.activeSelf)
        {
            finalKey.SetActive(true);
        }
    }

    public void ReturnFromStage()
    {
        currentStage++;
        SceneManager.LoadScene("Lobby");
    }

    public void UpdateProgress()
    {
        foreach (var gate in gates)
        {
            gate.SetActive(true);
        }

        if (currentStage < gates.Length)
        {
            gates[currentStage].SetActive(false);
        }

        kunais[0].SetActive(true);

        if (currentStage >= 1)
        {
            kunais[1].SetActive(true);
            kunais[2].SetActive(true);
        }
        else
        {
            kunais[1].SetActive(false);
            kunais[2].SetActive(false);
        }

        if (finalKey != null)
        {
            finalKey.SetActive(currentStage >= 2);
        }
    }
}
