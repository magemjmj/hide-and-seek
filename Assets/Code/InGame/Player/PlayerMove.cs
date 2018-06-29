using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerBehaviour
{
    public PlayerMove(PlayerBase PlayerBase) : base(PlayerBase)
    {        
    }

    public override void Update()
    {
        UpdateInputMove();
    }

    void UpdateInputMove()
    {
        Vector3 curForward = new Vector3(PlayerBase.m_fCurrentDir, 0, PlayerBase.m_fCurrentThrottle);
        if (curForward == Vector3.zero)
            return;

        PlayerBase.m_Transform.forward = curForward;

        Vector3 velocity = new Vector3(0, 0, 1f);
        velocity = PlayerBase.m_Transform.TransformDirection(velocity);
        Vector3 vForce = velocity * 6f * PlayerBase.m_FixedUpdateDeltaTime;

        vForce.y -= 15.0f * PlayerBase.m_FixedUpdateDeltaTime;

        if (PlayerBase.m_CharacterController)
            PlayerBase.m_CharacterController.Move(vForce);
    }
}
