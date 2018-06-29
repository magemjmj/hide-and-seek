using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class PlayerBase : MonoBehaviour
{
    public NewProj.PlayerType m_ePlayerType { get; set; }

    // cached components ------------------------------------        
    public GameObject m_GameObj { get; set; }
    public Transform m_Transform { get; set; }
    public Rigidbody m_RigidBody { get; set; }
    public CharacterController m_CharacterController { get; set; }

    public float m_fCurrentThrottle { get; set; }

    // for script Update control ----------------------------------
    public float m_UpdateDeltaTime { get; set; }
    public float m_FixedUpdateDeltaTime { get; set; }
    // constants ---------------------------------------------------   
    protected const float m_ConstGroundNormalValidHeight = 2.5f;

    private RaycastHit m_GroundHitData;
    private Vector3 m_GroundedNormal = Vector3.up;
    private bool m_bGroundHitSuccess = false;

    protected List<PlayerBehaviour> m_ListPlayerBehaviour;    

    public bool m_IsMove { get; set; }
    public float m_fCurrentDir { get; set; }

    protected virtual void Awake()
    {
        m_Transform = transform;
        m_RigidBody = GetComponent<Rigidbody>();
        m_GameObj = gameObject;
        m_CharacterController = GetComponent<CharacterController>();
        m_GroundHitData = new RaycastHit();
        m_ListPlayerBehaviour = new List<PlayerBehaviour>();        
    }

    // Use this for initialization
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (InGameGlobal.m_InGameGM == null || !InGameGlobal.GetInGameState().IsGameState(InGameState.States.PLAY))
            return;

        UpdateDeltaTimeBase();

        foreach (PlayerBehaviour ABehavior in m_ListPlayerBehaviour)
        {
            ABehavior.Update();
        }        
    }

    protected virtual void FixedUpdate()
    {
        if (InGameGlobal.m_InGameGM == null || !InGameGlobal.GetInGameState().IsGameState(InGameState.States.PLAY))
            return;

        FixedUpdateDeltaTimeBase();

        foreach (PlayerBehaviour ABehavior in m_ListPlayerBehaviour)
        {
            ABehavior.FixedUpdate();
        }
    }

    protected virtual void LateUpdate()
    {
        foreach (PlayerBehaviour ABehavior in m_ListPlayerBehaviour)
        {
            ABehavior.LateUpdate();
        }
    }

    // -------------------------------------------------------------------------------------------------
    protected void UpdateDeltaTimeBase()
    {
        m_UpdateDeltaTime = Time.deltaTime;
    }

    // -------------------------------------------------------------------------------------------------
    protected void FixedUpdateDeltaTimeBase()
    {
        m_FixedUpdateDeltaTime = Time.fixedDeltaTime;
    }

    // -------------------------------------------------------------------------------------------------
    protected void FixedUpdateGroundHitDataBase()
    {
        Vector3 vecTransformUp;
        vecTransformUp = m_Transform.up;
        
        m_bGroundHitSuccess = Physics.Raycast(m_Transform.position + vecTransformUp, -vecTransformUp, out m_GroundHitData, m_ConstGroundNormalValidHeight);
        m_GroundedNormal = m_bGroundHitSuccess ? m_GroundHitData.normal : Vector3.up;  ////m_GroundHitData.normal = The normal of the surface the ray hit          
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {

    }

    void OnTriggerEnter(Collider InObject)
    {        
        if (InObject.attachedRigidbody == null || !InObject.isTrigger)
            return;
        
        InObject.gameObject.SetActive(false);
    }

    public void EndState()
    {
        m_fCurrentDir = 0.0f;
    }
}
