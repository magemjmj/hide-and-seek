using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameState : MonoBehaviour
{
    public enum States
    {
        INIT,
        WAITING,
        PLAY,
        END,
    }

    private States m_eState;
    private Dictionary<int, PlayerBase> m_dicPlayers;

    private GameObject m_PlayerRoot;
    private List<Transform> m_ListStartPoint;    
    
    private const string m_ConstNameStartPoints = "@StartPointRoot";
    private const float m_ConstPlayUpdateInterval = 0.1f;
    private const float m_ConstGameRestartTime = 3f;

    public Dictionary<int, PlayerBase> dicPlayers
    {
        get { return m_dicPlayers; }
        private set { m_dicPlayers = value; }
    }

    protected virtual void Awake()
    {
    }

    // Use this for initialization
    protected virtual void Start ()
    {
        SetNextState(States.INIT);
        NextState();
	}

    // Update is called once per frame
    protected virtual void Update ()
    {
#if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(SpawnPlayer());
        }
#endif
        switch(m_eState)
        {
            case States.PLAY: PLAY_Update(); return;
        }
    }

    #region INIT
    protected virtual IEnumerator INIT_STATE()
    {
        Debug.Log("INIT_STATE");

        yield return StartCoroutine(InstantiateActor());
        FindStartPoints();
        yield return StartCoroutine(InitPlayer());

        yield return null;

        SetNextState(States.WAITING);
        NextState();
    }

    IEnumerator InstantiateActor()
    {
        if (m_PlayerRoot != null)
            yield break;

        m_PlayerRoot = new GameObject("@PLAYER");

        yield return StartCoroutine(AssetManager.GetManager().InstantiateAsset<GameObject>("prefabs", "PlayerCamera", Vector3.zero, Quaternion.identity, null, (GameObject obj) =>
        {
            Debug.Log("Instantiate Success! => " + obj.ToString());
        }));

        yield return StartCoroutine(AssetManager.GetManager().InstantiateAsset<GameObject>("prefabs", "InGameUI", Vector3.zero, Quaternion.identity, null, (GameObject obj) =>
        {
            Debug.Log("Instantiate Success! => " + obj.ToString());
        }));         
    }

    void FindStartPoints()
    {
        if (m_ListStartPoint != null && m_ListStartPoint.Count > 0)
            return;

        m_ListStartPoint = new List<Transform>();
        var startObj = GameObject.Find(m_ConstNameStartPoints);
        if(startObj == null)
        {
            Debug.LogError("Cannot find [Start Points] Game Object!!!");
            return;
        }

        m_ListStartPoint.Add(startObj.transform);
    }

    IEnumerator InitPlayer()
    {
        int iCreatPlayerNum = 0;
        while (true)
        {
            if (iCreatPlayerNum == 1)
                break;

            yield return StartCoroutine(InstantiatePlayer(iCreatPlayerNum));
            iCreatPlayerNum++;
        }
    }

    IEnumerator InstantiatePlayer(int InPlayerNum)
    {
        if (m_dicPlayers != null && m_dicPlayers.Count > 0)
        {
            SetPlayerStartPosition(m_dicPlayers[InPlayerNum], InPlayerNum);
            yield break;
        }

        m_dicPlayers = new Dictionary<int, PlayerBase>();
        GameObject PlayerObj = null;

        yield return StartCoroutine(Factory.CreatePlayer(NewProj.PlayerType.LOCAL, (GameObject obj) => 
        {
            PlayerObj = obj;
            PlayerObj.transform.parent = m_PlayerRoot.transform;
            PlayerObj.name = string.Format("@PLAYER_{0}", InPlayerNum.ToString());
        }));
        
        var playerBase = PlayerObj.GetComponent<PlayerLocal>() as PlayerBase;
        InGameGlobal.m_LocalPlayer = playerBase;
        InGameGlobal.m_PlayerCamera.SetTargetVehicle(playerBase);

        SetPlayerStartPosition(playerBase, InPlayerNum);

        m_dicPlayers[InPlayerNum] = playerBase;
    }

    void SetPlayerStartPosition(PlayerBase InVehicle, int InNum)
    {
        if (InNum == (int)NewProj.PlayerType.LOCAL)
        {
            InVehicle.transform.position = m_ListStartPoint[0].position;
            InVehicle.transform.rotation = m_ListStartPoint[0].rotation;
        }
    }
    #endregion

    #region WAITING
    protected virtual IEnumerator WAITING_STATE()
    {
        Debug.Log("WAITING_STATE");

        yield return null;

        SetNextState(States.PLAY);
        NextState();
    }
    #endregion

    #region PLAY
    protected virtual IEnumerator PLAY_STATE()
    {
        Debug.Log("PLAY_STATE");

        // 자동차 주행 시작 
        foreach (PlayerBase APlayer in m_dicPlayers.Values)
        {
            if (APlayer)
            {
            }
        }


        while (m_eState == States.PLAY)
        {
            yield return new WaitForSeconds(m_ConstPlayUpdateInterval);
        }

        NextState();
    }

    protected virtual void PLAY_Update()
    {       
    }

    public void CheckEnd(PlayerBase InPlayer)
    {
        if (InGameGlobal.m_LocalPlayer == InPlayer)
        {
            SetNextState(States.END);
            return;
        }

        // AI 처리 코드
        if (m_dicPlayers.ContainsValue(InPlayer))
        {
        }
    }
    #endregion

    #region END
    protected virtual IEnumerator END_STATE()
    {
        Debug.Log("END_STATE");

        InGameGlobal.m_LocalPlayer.EndState();

        yield return new WaitForSeconds(m_ConstGameRestartTime);

        GameRestart();
    }    
    #endregion

    #region ETC
    public void GameRestart()
    {
        SetNextState(States.INIT);
        NextState();
    }

    IEnumerator SpawnPlayer()
    {
        float fXpos = Random.Range(-40, 40);
        float fZpos = Random.Range(40, -40);

        Vector3 vecSpawnPos = new Vector3(fXpos, 0.5f, fZpos);

        yield return StartCoroutine(AssetManager.GetManager().InstantiateAsset<GameObject>("prefabs", "Player", vecSpawnPos, Quaternion.identity, null, (GameObject obj) =>
        {
            Debug.Log("Instantiate Success! => " + obj.ToString());
        }));
    }

    public bool IsGameState(States InState)
    {
        return InState == m_eState;
    }

    protected void NextState()
    {        
        string MethodName = m_eState.ToString() + "_STATE";
        System.Reflection.MethodInfo MethodInfo = GetType().GetMethod(MethodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        StartCoroutine((IEnumerator)MethodInfo.Invoke(this, null));
    }    
    
    protected void SetNextState(States InNext)
    {
        m_eState = InNext;
    }
    #endregion
}
