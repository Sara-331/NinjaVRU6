using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Slider detectionSlider;
    public float detectionTime = 5f;

    float currentTime = 0f;
    bool isDetecting = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        detectionSlider.gameObject.SetActive(false);
        detectionSlider.maxValue = detectionTime;
        detectionSlider.value = 0;
    }

    void Update()
    {
        if (isDetecting)
        {
            currentTime += Time.deltaTime;
            detectionSlider.value = currentTime;

            if (currentTime >= detectionTime)
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime * 2;
                detectionSlider.value = currentTime;

                if (currentTime <= 0)
                {
                    detectionSlider.gameObject.SetActive(false);
                }
            }
        }
    }

    public void StartDetection()
    {
        isDetecting = true;
        detectionSlider.gameObject.SetActive(true);
    }

    public void StopDetection()
    {
        isDetecting = false;
    }
}
