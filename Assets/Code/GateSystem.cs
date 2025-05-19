using UnityEngine;
using TMPro;

public class GateSystem : MonoBehaviour
{
    public static GateSystem instance;

    public int totalItems = 5;
    private int collectedItems = 0;

    public TMP_Text gateUIText;
    public GameObject gateLight;
    public AudioSource gateOpenSound;
    public GameObject gateObject;

    private Material gateMat;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateGateUI();
        gateLight.SetActive(false);

        if (gateObject != null)
            gateMat = gateObject.GetComponent<Renderer>().material;
    }

    public void CollectItem()
    {
        collectedItems++;
        UpdateGateUI();

        if (collectedItems >= totalItems)
            OpenGate();
    }

    void UpdateGateUI()
    {
        gateUIText.text = $"You need to Collect {totalItems} To Win! You have {collectedItems}  ";
    }

    void OpenGate()
    {
        gateUIText.gameObject.SetActive(false);
        gateLight.SetActive(true);

        if (gateMat != null)
            gateMat.EnableKeyword("_EMISSION");

        if (gateOpenSound != null)
            gateOpenSound.Play();

        if (gateObject != null)
            gateObject.SetActive(false);
    }

    public bool HasCollectedAll()
    {
        return collectedItems >= totalItems;
    }
}
