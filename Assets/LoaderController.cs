using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoaderController : MonoBehaviour {

    public static LoaderController main;

    public Image bg;
    public Image bar;
    public Image border;
    float fakePercent = 0;

    public void LoadScene(string sceneToLoad)
    {
        //Scene scene = SceneManager.GetSceneByName(sceneToLoad);
        //if (scene != null) return; // it's already loaded...

        gameObject.SetActive(true);
        bar.fillAmount = 0;
        StartCoroutine(LoadIt(sceneToLoad));
    }
    void Start()
    {
        if (main)
        {
            Destroy(gameObject);
        }
        else
        {
            main = this;
            DontDestroyOnLoad(gameObject);
        }
    }
	IEnumerator LoadIt(string sceneToLoad)
    {
        while (bg.color.a < 1) // fade in BG
        {
            bg.color = FadeTo(bg.color, 1);
            yield return null;
        }
        while (border.color.a < 1) // fade in border
        {
            border.color = FadeTo(border.color, 1);
            yield return null;
        }

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneToLoad); // LOAD
        
        while (fakePercent < 1.0f) // progress bar
        {
            if (fakePercent < op.progress) fakePercent += Time.deltaTime * .5f;
            bar.fillAmount = fakePercent;
            yield return null;
        }
        while (border.color.a > 0) // fade out bar
        {
            border.color = FadeTo(border.color, 0, 2);
            bar.color = border.color;
            yield return null;
        }
        while (bg.color.a > 0) // fade out bg
        {
            bg.color = FadeTo(bg.color, 0, 2);
            yield return null;
        }
        gameObject.SetActive(false);
    }
    Color FadeTo(Color color, float target, float speed = 1)
    {
        if (target < color.a) speed = -1;
        color.a += Time.deltaTime * speed;
        return color;
    }
}
