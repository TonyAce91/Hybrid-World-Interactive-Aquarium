﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a superclass for all agents (or AI) in the game
/// This is a monoBehaviour subclass to allow all its subclass to be attachable to game objects in inspector
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class AgentActor : MonoBehaviour {

    // When we want to make a prefab, we can't reference any specific object in a scene
    // This is because it could be used in multiple scenes

    // A list of behaviours that the agent will have
    protected List<IBehaviour> m_behaviours;

    [HideInInspector] public Rigidbody m_body;
    [HideInInspector] public GameObject player;
    public GameObject targetVisualiser;


    // Physics
    protected Vector3 m_velocity;
    protected Vector3 m_acceleration;
    protected Vector3 m_force;

    // Maximum speed of agent
    public float maxSpeed = 150.0f;
    public float rotationMaxSpeed = 1.0f;

    [Header("Hand Seek Behaviour")]
    public float handSeekSpeed = 10.0f;

    [Header("Ball Seek Behaviour")]
    public float ballSeekSpeed = 10.0f;
	[HideInInspector] public float m_ballInteractTime;


    // Updates behaviour and physics of agent
    protected void UpdateBehaviours()
    { 
        // Resets all the forces on the agent
        m_force = Vector3.zero;

        // Updates all behaviours of the agent
        foreach (IBehaviour behaviour in m_behaviours)
        {
            behaviour.UpdateBehaviour(this);
        }

        m_body.AddForce(m_force);
        //// Calculates resultant velocity due to forces
        //m_body.velocity += m_force * Time.fixedDeltaTime;
        
        //// Moves the agent in relation to the velocity on them
        //transform.position += m_velocity * Time.fixedDeltaTime;
    }

    // Use to add forces applied on the agent
    public void AddForce(Vector3 force)
    {
        m_force += force;
    }
    

    //-----------------------------------------------------------
    // Properties

    // Velocity property
    public Vector3 Velocity
    {
        get
        {
            return m_velocity;
        }
        set
        {
            if (value != null)
                m_velocity = value;
        }
    }
}
