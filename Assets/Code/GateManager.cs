using UnityEngine;

public class GateManager : MonoBehaviour
{
    public GameObject[] gates; // البوابات الثلاث
    public GameObject[] correctKunais; // الكوناي الصحيح لكل بوابة
    public Transform[] sockets; // أماكن الكوناي

    public GameObject finalKey; // المفتاح النهائي
    public GameObject finalGate; // البوابة الأخيرة
    public Transform finalKeySocket; // مكان المفتاح النهائي

    private bool[] gateUnlocked;

    void Start()
    {
        gateUnlocked = new bool[gates.Length];
        finalKey.SetActive(false); // إخفاء المفتاح بالبداية
    }

    void Update()
    {
        for (int i = 0; i < gates.Length; i++)
        {
            if (!gateUnlocked[i])
            {
                if (IsCorrectKunaiInSocket(i))
                {
                    OpenGate(i);
                }
            }
        }

        if (AllGatesUnlocked() && !finalKey.activeSelf)
        {
            finalKey.SetActive(true); // إظهار المفتاح بعد فتح الثلاث بوابات
        }

        if (IsKeyInFinalSocket())
        {
            OpenFinalGate();
        }
    }

    bool IsCorrectKunaiInSocket(int index)
    {
        if (sockets[index].childCount == 0) return false;
        return sockets[index].GetChild(0).gameObject == correctKunais[index];
    }

    void OpenGate(int index)
    {
        gates[index].SetActive(false); // إخفاء البوابة (كأنها انفتحت)
        gateUnlocked[index] = true;
        Debug.Log($"Gate {index + 1} opened!");
    }

    bool AllGatesUnlocked()
    {
        foreach (bool opened in gateUnlocked)
        {
            if (!opened) return false;
        }
        return true;
    }

    bool IsKeyInFinalSocket()
    {
        if (finalKeySocket.childCount == 0) return false;
        return finalKeySocket.GetChild(0).gameObject == finalKey;
    }

    void OpenFinalGate()
    {
        finalGate.SetActive(false);
        Debug.Log("Final gate opened!");
    }
}
