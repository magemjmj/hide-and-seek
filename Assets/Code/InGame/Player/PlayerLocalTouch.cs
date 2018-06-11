using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class PlayerLocal : PlayerBase
{
    public enum TOUCH_STATE
    {
        LEFT,
        RIGHT,
        END
    }
    
    private BitArray m_TouchState = new BitArray((int)TOUCH_STATE.END, false);    

    //[Conditional("UNITY_EDITOR"), Conditional("UNITY_STANDALONE_WIN")]
    public void FixedUpdateInputMove()
    {
        if (!m_IsMove)
            return;
        
        m_Transform.forward = new Vector3(m_fCurrentSteer, 0, m_fCurrentThrottle);

        Vector3 velocity = new Vector3(0, 0, 1f);
        velocity = transform.TransformDirection(velocity);
        Vector3 vForce = velocity * 6f * m_FixedUpdateDeltaTime;
        
        vForce.y -= 15.0f * m_FixedUpdateDeltaTime;

        if (m_CharacterController)
            m_CharacterController.Move(vForce);
    }

    public void FixedUpdateInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        m_IsMove = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);

        if(Input.GetKey(KeyCode.UpArrow))
        {
            VerticalMove(1f);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            VerticalMove(-1f);
        }        
#endif
        bool IsLeftSteer = Input.GetKey(KeyCode.LeftArrow);
        bool IsRightSteer = Input.GetKey(KeyCode.RightArrow);

        if (IsLeftSteer && !IsRightSteer)
        {
            //m_fCurrentSteer = m_fCurrentSteer > m_ConstSteerStart ? m_ConstSteerStart : m_fCurrentSteer;
            //m_fCurrentSteer -= m_FixedUpdateDeltaTime / m_fSteerIncreaseTime;
            m_fCurrentSteer = -1;

        }
        else if (!IsLeftSteer && IsRightSteer)
        {
            //m_fCurrentSteer = m_fCurrentSteer < -m_ConstSteerStart ? -m_ConstSteerStart : m_fCurrentSteer;
            //m_fCurrentSteer += m_FixedUpdateDeltaTime / m_fSteerIncreaseTime;
            m_fCurrentSteer = 1;
        }
        else if (IsLeftSteer && IsRightSteer)
        {
            if (m_fCurrentSteer < 0) m_fCurrentSteer += m_FixedUpdateDeltaTime / m_fSteerIncreaseTime;
            if (m_fCurrentSteer > 0) m_fCurrentSteer -= m_FixedUpdateDeltaTime / m_fSteerIncreaseTime;
        }

        bool ConditionSteer = !IsRightSteer && !IsLeftSteer;
        if (ConditionSteer)
        {
            //m_fCurrentSteer = m_fCurrentSteer * 0.2f;
            m_fCurrentSteer = 0;
        }

        m_fCurrentSteer = Mathf.Clamp(m_fCurrentSteer, -1.0f, 1.0f);
    }
}
