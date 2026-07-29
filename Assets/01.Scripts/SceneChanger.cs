using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;
    public CanvasGroup loading;
    private float duration;

    public int nowStage;

    private void Awake()
    {
        nowStage = 2;
        duration = 1f;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(int i)
    {
        StartCoroutine(LoadSceneRoutine(i));
    }

    private IEnumerator LoadSceneRoutine(int sceanNum)
    {
        loading.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceanNum);

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(FadeOut());
        loading.gameObject.SetActive(false);
    }


    private IEnumerator FadeIn()
    {
        float timer = 0f;
        loading.alpha = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            loading.alpha = Mathf.Lerp(0f, 1f, timer / duration);
            yield return null;
        }

        loading.alpha = 1f;
    }
    private IEnumerator FadeOut()
    {
        float timer = 0f;
        loading.alpha = 1f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            loading.alpha = Mathf.Lerp(1f, 0f, timer / duration);
            yield return null;
        }

        loading.alpha = 0f;
    }
}
