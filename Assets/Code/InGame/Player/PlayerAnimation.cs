using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerBase m_Player;
    private Animator m_Animator;

    void Awake()
    {
        m_Player = GetComponent<PlayerBase>();
        m_Animator = GetComponent<Animator>();
    }

    void Start ()
    {
		
	}
		
	void Update ()
    {
        if (m_Player.m_IsMove)
            m_Animator.SetBool("Walk", true);
        else
            m_Animator.SetBool("Walk", false);
    }
}
