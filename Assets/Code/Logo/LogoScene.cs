using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScene : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return StartCoroutine(AssetManager.GetManager().Initialize());
    }

    public void NextScene()
    {
        LoadingManager.GetManager().LoadScene("Lobby");
    }
}
