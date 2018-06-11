using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public void OnBackButton()
    {
        LoadingManager.GetManager().LoadScene("Lobby");
    }
}
