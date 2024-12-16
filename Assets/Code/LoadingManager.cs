using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;

    public Image fillBar;
    public TMP_Text progressText;
    public GameObject loadSection;
    public GameObject menu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        loadSection.SetActive(false);
        menu.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        loadSection.SetActive(true);
        menu.SetActive(false);
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (fillBar != null)
                fillBar.fillAmount = progress;

            if (progressText != null)
                progressText.text = $"{(progress * 100):0}%";

            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

        loadSection.SetActive(false);
    }
}
