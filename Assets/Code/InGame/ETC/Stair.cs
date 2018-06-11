using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField]
    private int m_MaxCount = 0;

    [SerializeField]
    private float m_YScale = 0f;
    [SerializeField]
    private float m_ZScale = 0f;
    [SerializeField]
    private float m_YPos = 0f;
    [SerializeField]
    private float m_ZPos = 0f;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(InstantiateStair());        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator InstantiateStair()
    {
        var vecInitPos = transform.position;        
        int iCount = 0;

        while(m_MaxCount > iCount)
        {
            yield return StartCoroutine(AssetManager.GetManager().InstantiateAsset<GameObject>("prefabs", "Stair", vecInitPos, Quaternion.identity, null, (GameObject obj) =>
            {
                //Debug.Log("Instantiate Success! => " + obj.ToString());
                obj.transform.localScale = new Vector3(5, m_YScale, m_ZScale);
                obj.transform.position = new Vector3(vecInitPos.x, vecInitPos.y + m_YPos * iCount, vecInitPos.z + m_ZPos * iCount);
                obj.transform.parent = transform;
            }));

            iCount++;
        }
                
    }
}
