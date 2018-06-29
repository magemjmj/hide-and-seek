using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerLocalTouch : PlayerBehaviour
{
    public PlayerLocalTouch(PlayerBase PlayerBase) : base(PlayerBase)
    {
    }

    public override void Update()
    {
        UpdateInput();
    }

    void UpdateInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        PlayerBase.m_IsMove = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);

        PlayerBase.m_fCurrentThrottle = 0f;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            PlayerBase.m_fCurrentThrottle = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            PlayerBase.m_fCurrentThrottle = -1f;            
        }

        bool IsLeftSteer = Input.GetKey(KeyCode.LeftArrow);
        bool IsRightSteer = Input.GetKey(KeyCode.RightArrow);

        if (IsLeftSteer && !IsRightSteer)
        {
            PlayerBase.m_fCurrentDir = -1;

        }
        else if (!IsLeftSteer && IsRightSteer)
        {
            PlayerBase.m_fCurrentDir = 1;
        }
        else if (IsLeftSteer && IsRightSteer)
        {
            PlayerBase.m_fCurrentDir = 0;
        }

        bool ConditionSteer = !IsRightSteer && !IsLeftSteer;
        if (ConditionSteer)
        {
            PlayerBase.m_fCurrentDir = 0;
        }

        PlayerBase.m_fCurrentDir = Mathf.Clamp(PlayerBase.m_fCurrentDir, -1.0f, 1.0f);
#endif
    }
}
