using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitAndLoadScene : MonoBehaviour
{
    public string nextSceneName = "NextScene"; // Set this in the Inspector

    void Start()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(22f);
        SceneManager.LoadScene(nextSceneName);
    }
}
