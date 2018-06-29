using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform m_TargetTrans;

    [SerializeField]
    private float m_fDistance = 12.0f;
    [SerializeField]
    private float m_fHeight = 8.0f;

    [SerializeField]
    private Vector3 m_CameraAngle;

    // cached components ------------------------------------    
    private Transform m_Transform;

    void Awake()
    {
        m_Transform = GetComponent<Transform>();

        InGameGlobal.m_PlayerCamera = this;
    }

    void Start()
    {
    }

    public void SetTargetVehicle(PlayerBase InTarget)
    {
        //m_TargetTrans = InTarget.CameraTarget;
        m_TargetTrans = InTarget.transform;
        if (m_TargetTrans == null)
            return;

        //Vector3 vecExactOffset;

        //vecExactOffset = m_fDistance * -m_TargetTrans.forward + m_fHeight * m_TargetTrans.up;        
        //m_Transform.position = m_Transform.position + vecExactOffset;

        //m_Transform.LookAt(m_TargetTrans.position);

        Quaternion cameraRot = Quaternion.Euler(m_CameraAngle.x, m_CameraAngle.y, m_CameraAngle.z);
        Vector3 newPos = cameraRot * new Vector3(0, m_fHeight, -m_fDistance) + m_TargetTrans.position;

        //Vector3 newPos = m_TargetTrans.position + new Vector3(0, m_fHeight, -m_fDistance);
        m_Transform.rotation = cameraRot;
        m_Transform.position = newPos;
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (!m_TargetTrans)
            return;

        QuarterView();
    }

    //void FixedUpdate()
    //{
    //    if (!m_TargetTrans)
    //        return;

    //    // Calculate the current rotation angles
    //    float wantedRotationAngle = m_TargetTrans.eulerAngles.y;
    //    float wantedHeight = m_TargetTrans.position.y + m_fHeight;

    //    float currentRotationAngle = m_Transform.eulerAngles.y;
    //    float currentHeight = m_Transform.position.y;

    //    // Damp the rotation around the y-axis
    //    currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time. fixedDeltaTime);

    //    // Damp the height
    //    //currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.fixedDeltaTime);
    //    currentHeight = wantedHeight;

    //    // Convert the angle into a rotation
    //    Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

    //    // Set the position of the camera on the x-z plane to:
    //    // distance meters behind the target
    //    m_Transform.position = m_TargetTrans.position;
    //    m_Transform.position -= currentRotation * Vector3.forward * m_fDistance;                
    //    // Set the height of the camera
    //    m_Transform.position = new Vector3(m_Transform.position.x, currentHeight, m_Transform.position.z);

    //    // Always look at the target
    //    m_Transform.LookAt(m_TargetTrans);
    //}

    void QuarterView()
    {
        Quaternion cameraRot = Quaternion.Euler(m_CameraAngle.x, m_CameraAngle.y, m_CameraAngle.z);        
        Vector3 newPos = cameraRot * new Vector3(0, m_fHeight, -m_fDistance) + m_TargetTrans.position;

        //Vector3 newPos = m_TargetTrans.position + new Vector3(0, m_fHeight, -m_fDistance);
        m_Transform.rotation = cameraRot;
        m_Transform.position = newPos;        
    }
}