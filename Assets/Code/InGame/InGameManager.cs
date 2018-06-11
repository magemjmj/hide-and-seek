using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [SerializeField]
    private NewProj.GameMode m_eGameMode = NewProj.GameMode.NONE;
    private InGameState GameState = null;

    public InGameState m_GameState
    {
        get { return GameState; }
        private set { GameState = value; }
    }

    void Awake()
    {
        InGameGlobal.m_InGameGM = this;        
    }
    
    void Start ()
    {
        switch (m_eGameMode)
        {
            case NewProj.GameMode.STANDALONE:
                m_GameState = gameObject.AddComponent<InGameState_STANDALONE>();
                break;
            default:
                m_GameState = gameObject.AddComponent<InGameState>();
                break;
        }
    }
		
	void Update ()
    {
	}
}
