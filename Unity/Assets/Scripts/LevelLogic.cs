using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LevelLogic : MonoBehaviour
{
    private int currentScene = 0;
    public GameObject userScreen;
    private GameObject canvas;
    private bool fading = false;
    public int numScenes = 3;
    public GameObject Background;
    private VideoPlayer videoPlayer;
    private void Start()
    {
        Background = GameObject.Find("Background");
        videoPlayer = Background.GetComponent<VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Editor[23].mp4");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Exit();
        }
    }

    public void ReloadScene()
    {
        StartCoroutine(ChangeSceneCR(currentScene));
    }

    public void ChangeScene()
    {
        currentScene += 1;
        StartCoroutine(ChangeSceneCR(currentScene));
    }

    IEnumerator ChangeSceneCR(int scene)
    {
        Debug.Log("Changing Scene...");
        GameObject[] startCanvas = GameObject.FindGameObjectsWithTag("StartUI");
        if (startCanvas != null)
            foreach(GameObject go in startCanvas)
                go.SetActive(false);
        GameObject UI = GameObject.FindGameObjectWithTag("GoalUI");
        if (UI != null)
            Destroy(UI);
        StartCoroutine(FadeTo(1f, 1f));
        while (fading) yield return null;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
        if (scene == numScenes + 1 && videoPlayer != null)
        {
            videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Editor[23].mp4");
        }
        setLevelNum(scene);
        StartCoroutine(FadeTo(0f, 1f));
    }

    public void setLevelNum(int scene)
    {
        GameObject levelText = GameObject.FindWithTag("LevelText");
        GameObject levelNumText = GameObject.FindWithTag("Level");
        if (levelNumText == null || levelText == null)
            return;

        if (scene == numScenes + 1)
        {
            levelText.GetComponent<TMPro.TextMeshProUGUI>().text = " ";
            levelNumText.GetComponent<TMPro.TextMeshProUGUI>().text = " ";
        }
        else
        {
            levelText.GetComponent<TMPro.TextMeshProUGUI>().text = "Level ";
            levelNumText.GetComponent<TMPro.TextMeshProUGUI>().text = scene.ToString() + "/" + numScenes.ToString();
        }
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
