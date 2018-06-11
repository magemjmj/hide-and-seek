using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Model;
    [SerializeField]
    private GameObject m_DragArea;

    float m_fDelta = 0;
    float m_fTargetYAngle = 0;

    // Use this for initialization
    void Start ()
    {
        InitEvent();
    }

    void InitEvent()
    {
        EventTrigger Trigger = m_DragArea.GetComponent<EventTrigger>();
        EventTrigger.Entry Entry1 = new EventTrigger.Entry();
        Entry1.eventID = EventTriggerType.BeginDrag;
        Entry1.callback.AddListener((data) => { OnBeginDrag((PointerEventData)data); });
        Trigger.triggers.Add(Entry1);

        EventTrigger.Entry Entry2 = new EventTrigger.Entry();
        Entry2.eventID = EventTriggerType.Drag;
        Entry2.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
        Trigger.triggers.Add(Entry2);

        EventTrigger.Entry Entry3 = new EventTrigger.Entry();
        Entry3.eventID = EventTriggerType.EndDrag;
        Entry3.callback.AddListener((data) => { OnEndDrag((PointerEventData)data); });
        Trigger.triggers.Add(Entry3);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_Model == null)
            return;

        m_fDelta = Mathf.Lerp(m_fDelta, m_fTargetYAngle, Time.deltaTime * 5.0f);

        Vector3 EulerAngles = m_Model.transform.rotation.eulerAngles;
        m_Model.transform.rotation = Quaternion.Euler(EulerAngles.x, m_fDelta, EulerAngles.z);
    }

    public void OnBeginDrag(PointerEventData data)
    {
        //Debug.Log("drag start");
    }

    public void OnDrag(PointerEventData data)
    {
        m_fTargetYAngle -= data.delta.x * 0.5f;
    }

    public void OnEndDrag(PointerEventData data)
    {
        //Debug.Log("drag end");
    }

    public void OnStartButton()
    {
        LoadingManager.GetManager().LoadScene("Play");
    }
}
