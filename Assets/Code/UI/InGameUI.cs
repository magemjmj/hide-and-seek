using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    private PlayerBase m_LocalPlayer;

    void Start()
    {
        if (m_LocalPlayer == null)
            m_LocalPlayer = InGameGlobal.m_LocalPlayer;
    }

    void Update()
    {
            
    }

    public void OnBackButton()
    {
        LoadingManager.GetManager().LoadScene("Lobby");
    }
}
