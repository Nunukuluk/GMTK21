using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLogic : MonoBehaviour
{
    private int currentScene = 0;
    public GameObject userScreen;
    private GameObject canvas;
    private bool fading = false;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    public void ChangeScene()
    {
        StartCoroutine(ChangeSceneCR());
    }

    IEnumerator ChangeSceneCR()
    {
        Debug.Log("Changing Scene...");
        currentScene += 1;
        if (canvas != null)
            canvas.SetActive(false);
        StartCoroutine(FadeTo(1f, 1f));
        while (fading) yield return null;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(currentScene);
        StartCoroutine(FadeTo(0f, 1f));
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        fading = true;
        float alpha = userScreen.GetComponent<Renderer>().material.color.a;
        Color color = userScreen.GetComponent<Renderer>().material.color;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(color.r, color.b, color.g, Mathf.Lerp(alpha, aValue, t));
            userScreen.GetComponent<Renderer>().material.color = newColor;
            yield return null;
        }
        fading = false;
    }

}
