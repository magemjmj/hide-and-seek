using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory
{
    public delegate void Load_complete(GameObject parm);

    public static IEnumerator CreatePlayer(NewProj.PlayerType InPlayerType, Load_complete fn)
    {
        GameObject PlayerObj = null;

        if (InPlayerType == NewProj.PlayerType.LOCAL)
        {
            yield return AssetManager.GetManager().StartCoroutine(AssetManager.GetManager().InstantiateAsset<GameObject>
                         ("prefabs", "FemalePlayer", Vector3.zero, Quaternion.identity, null, (GameObject obj) =>
            {
                Debug.Log("Instantiate Success! => " + obj.ToString());
                PlayerObj = obj;
                PlayerObj.AddComponent<PlayerLocal>();
            }));
        }
        else if (InPlayerType == NewProj.PlayerType.AI)
        {
        }

        PlayerObj.AddComponent<PlayerAnimation>();
        PlayerObj.AddComponent<PlayerEffect>();
        fn(PlayerObj);
    }
}
