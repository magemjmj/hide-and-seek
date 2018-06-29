using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocal : PlayerBase// , IEnumerable
{
    protected override void Awake()
    {
        base.Awake();

        m_ListPlayerBehaviour.Add(new PlayerLocalTouch(GetComponent<PlayerBase>()));
        m_ListPlayerBehaviour.Add(new PlayerMove(GetComponent<PlayerBase>()));
    }

    protected override void FixedUpdate()
    {        

        base.FixedUpdate();                          
    }
}
