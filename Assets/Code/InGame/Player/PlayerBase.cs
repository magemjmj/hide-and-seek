using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private NewProj.PlayerType m_ePlayerType = NewProj.PlayerType.LOCAL;


    // external transform components ----------------------------
    protected Transform m_CameraTarget;

    // cached components ------------------------------------    
    protected GameObject m_GameObj;
    protected Transform m_Transform;
    protected Rigidbody m_RigidBody;
    protected CharacterController m_CharacterController;

    protected float m_fCurrentSteer;
    public float m_fCurrentThrottle { get; set; }
    protected float m_fPreSteerAngle;

    protected float m_fSteerIncreaseTime = 0.2f;

    // drive flag --------------------------------------------------
    protected bool m_bDisableThrottleInput = false;
    protected bool m_bDisableSteerInput = false;

    // for script Update control ----------------------------------
    protected float m_UpdateDeltaTime;
    protected float m_FixedUpdateDeltaTime;
    // constants ---------------------------------------------------   
    protected const float m_ConstSteerStart = 0.35f;
    protected const float m_ConstGroundNormalValidHeight = 2.5f;

    private RaycastHit m_GroundHitData;
    private Vector3 m_GroundedNormal = Vector3.up;
    private bool m_bGroundHitSuccess = false;

    private Vector3 m_vPrePos = new Vector3(0, 0, 0);
    protected bool m_IsMove = false;
    public bool IsMove
    {
        get { return m_IsMove; }
        set { m_IsMove = value; }
    }

    #region Get / Set
    public NewProj.PlayerType ePlayerType
    {
        get { return m_ePlayerType; }
        set { m_ePlayerType = value; }
    }

    public Transform CameraTarget
    {
        get { return m_CameraTarget; }   
        private set { m_CameraTarget = value; }
    }

    public RaycastHit GroundHitData
    {
        get { return m_GroundHitData; }
        private set { m_GroundHitData = value; }
    }

    public Vector3 GroundedNormal
    {
        get { return m_GroundedNormal; }
        set { m_GroundedNormal = value; }
    }

    public bool bGroundHitSuccess
    {
        get { return m_bGroundHitSuccess; }
        set { m_bGroundHitSuccess = value; }
    }
#endregion

    protected virtual void Awake()
    {
        m_Transform = transform;
        m_RigidBody = GetComponent<Rigidbody>();
        m_GameObj = gameObject;

        m_CharacterController = GetComponent<CharacterController>();

        m_GroundHitData = new RaycastHit();
    }

    // Use this for initialization
    protected virtual void Start()
    {
        //IDisposable
        //IComparable
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    protected virtual void FixedUpdate()
    {
        
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

    protected void FixedUpdateThrottleBase()
    {
        //Vector3 velocity = new Vector3(0, 0, m_fCurrentThrottle);
        //velocity = transform.TransformDirection(velocity);
        //velocity *= 6;
        //velocity *= m_FixedUpdateDeltaTime;
        //m_Transform.position += velocity;

        //Vector3 vCurpos = m_vPrePos + velocity;
        //m_Transform.localPosition = vCurpos;        
        //m_vPrePos = m_Transform.localPosition;

        Vector3 vForce = m_fCurrentThrottle * m_Transform.forward * 6f * m_FixedUpdateDeltaTime;
        //Vector3 vCurpos = m_vPrePos + vForce;
        //Vector3 vCurpos = m_vPrePos + velocity;

        // 임시 중력
        vForce.y -= 15.0f * m_FixedUpdateDeltaTime;

        if (m_CharacterController)
            m_CharacterController.Move(vForce);
    }

    protected void FixedUpdateSteeringBase()
    {
        if (m_fCurrentSteer == 0.0f)
            return;

        float fTurnDegree = 90f;      
        float fAngle = fTurnDegree * Time.deltaTime * m_fCurrentSteer;
        
        m_Transform.Rotate(m_Transform.up, fAngle);
    }

    // -------------------------------------------------------------------------------------------------
    protected void FixedUpdateGroundHitDataBase()
    {
        Vector3 vecTransformUp;
        vecTransformUp = m_Transform.up;
        
        m_bGroundHitSuccess = Physics.Raycast(m_Transform.position + vecTransformUp, -vecTransformUp, out m_GroundHitData, m_ConstGroundNormalValidHeight);
        m_GroundedNormal = m_bGroundHitSuccess ? m_GroundHitData.normal : Vector3.up;  ////m_GroundHitData.normal = The normal of the surface the ray hit          
    }

    void OnCollisionEnter(Collision InCollisionInfo)
    {

    }

    void OnTriggerEnter(Collider InObject)
    {        
        if (InObject.attachedRigidbody == null || !InObject.isTrigger)
            return;
        
        InObject.gameObject.SetActive(false);
    }

    protected void VerticalMove(float move_value)
    {
        m_fCurrentThrottle = move_value;
    }

    public void EnableThrottle(bool InEnable)
    {
        m_bDisableThrottleInput = !InEnable;
    }

    public void EndState()
    {
        EnableThrottle(false);
        m_fCurrentSteer = 0.0f;
    }
}
