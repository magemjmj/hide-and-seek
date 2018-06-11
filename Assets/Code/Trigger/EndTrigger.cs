using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == null)
            return;

        var PlayerBase = InGameGlobal.GetPlayerBase(other.attachedRigidbody.gameObject);
        if (PlayerBase == null)
            return;

        //// End 루틴 처리
        InGameGlobal.GetInGameState().CheckEnd(PlayerBase);
    }
}
