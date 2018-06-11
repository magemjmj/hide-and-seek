using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : Singleton<LoadingManager>
{
    public void LoadScene(string InSceneName)
    {
        StartCoroutine(CoroutineLoadScene(InSceneName));
    }
    
    IEnumerator CoroutineLoadScene(string InSceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Loading");
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //yield return StartCoroutine(CoroutineLoadSceneComplete());
        yield return new WaitForSeconds(1.0f); 

        AssetManager.GetManager().LoadLevelAsync("scene", InSceneName, false, null);
    }    
    
    IEnumerator CoroutineLoadSceneComplete()
    {
        GameObject fadeBG = GameObject.Find("Canvas/BG");
        CanvasRenderer cRenderer = fadeBG.GetComponent<CanvasRenderer>();

        float ftime = 1f;
        while(ftime > 0f)
        {
            ftime -= 0.05f;
            if (ftime < 0)
                ftime = 0;

            cRenderer.SetAlpha(ftime);
            yield return null;
        }        
    }
}
