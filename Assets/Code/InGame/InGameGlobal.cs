using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewProj
{
    public enum GameMode
    {
        NONE = -1,
        STANDALONE = 0,
        PRACTICE
    };

    public enum PlayerType
    {
        LOCAL,
        AI,
        END
    }
}

public class InGameGlobal
{
    public static InGameManager m_InGameGM;
    public static PlayerCamera m_PlayerCamera;
    public static PlayerBase m_LocalPlayer;

    
    public static InGameState GetInGameState()
    {
        return m_InGameGM.m_GameState;
    }

    public static PlayerBase GetPlayerBase(GameObject InObject)
    {
        return InObject.GetComponent<PlayerBase>() as PlayerBase;
    }
}
