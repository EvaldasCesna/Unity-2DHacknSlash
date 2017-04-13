using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour {

    private bool loadScene;
    [SerializeField]
    private int scene;
    [SerializeField]
    private Text loadingText;

	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyUp(KeyCode.Space) && loadScene == false)
        {
            loadScene = true;
            loadingText.text = "Loading...";
            StartCoroutine(LoadNewScene());
        }
        if (loadScene)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }

	}

    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(5);

        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        while (!async.isDone)
        {
            yield return null;
        }
    }
}
