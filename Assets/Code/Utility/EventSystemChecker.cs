using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemChecker : MonoBehaviour
{
    //public GameObject eventSystem;

	// Use this for initialization
	void Awake ()
	{
	    if(!FindObjectOfType<EventSystem>())
        {
           //Instantiate(eventSystem);
            GameObject Obj = new GameObject("EventSystem");
            Obj.AddComponent<EventSystem>();
            Obj.AddComponent<StandaloneInputModule>().forceModuleActive = true;
        }
	}
}
