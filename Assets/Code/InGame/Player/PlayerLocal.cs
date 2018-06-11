using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerLocal : PlayerBase// , IEnumerable
{
    protected override void Start()
    {
        base.Start();

        EnableThrottle(false);
    }

    protected override void Update()
    {
        UpdateDeltaTimeBase();

        base.Update();
    }

    protected override void FixedUpdate()
    {
        if (InGameGlobal.m_InGameGM == null || !InGameGlobal.GetInGameState().IsGameState(InGameState.States.PLAY))
            return;
        
        m_fCurrentThrottle = m_bDisableThrottleInput ? 0f : 1f;

        FixedUpdateDeltaTimeBase();
        base.FixedUpdate();
        
        FixedUpdateInput();
        FixedUpdateInputMove();

        //FixedUpdateSteeringBase();
        //FixedUpdateThrottleBase();
    }
}
