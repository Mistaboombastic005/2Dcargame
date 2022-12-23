using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public GameObject wheel;
    public TMP_Text progressText;
    public GameObject loadingScreen;
    private AsyncOperation operation;

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadASync(sceneIndex));
    }

    IEnumerator LoadASync(int sceneIndex)
    {
        operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);


        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            string percentage = progress * 100 + "%";

            progressText.text = percentage;
            Debug.Log(operation.progress);

            wheel.transform.Rotate(0, 0, 10000);

            if (operation.isDone)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            yield return null;
        }
    }
}
