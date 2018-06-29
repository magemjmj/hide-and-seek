using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : PlayerBase
{
    protected override void Awake()
    {
        base.Awake();

        m_ListPlayerBehaviour.Add(new PlayerAuto(GetComponent<PlayerBase>()));
        m_ListPlayerBehaviour.Add(new PlayerMove(GetComponent<PlayerBase>()));
    }


}
