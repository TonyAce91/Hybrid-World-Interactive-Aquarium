using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to create force to seek the target
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class SeekForce : SteeringForce
{

    private GameObject m_target;
    private Vector3 m_vectorTarget;
    private bool m_seekObject = true;
    public float m_rotationSpeed = 0.1f;
    public float forwardSpeed = 1.0f;
    //public GameObject targetVisualiser;

    public override Vector3 GetForce(AgentActor agent)
    {
        Vector3 force = new Vector3(0, 0, 0);

        // This is used to set whether to seek a position or a game object
        if (m_seekObject && m_target != null)
        {
            m_vectorTarget = m_target.tag == "Player" ? m_target.transform.localPosition : m_target.transform.position;
            
            //Vector3 lookAtTarget = m_target.transform.position - agent.transform.position;
            //force = (m_target.transform.position - agent.transform.position).normalized * agent.MaxSpeed;
            //Quaternion angleToPlayer = Quaternion.LookRotation(lookAtTarget, Vector3.up);
            //float lerpSpeed = 180.0f * rotationSpeed / Vector3.Angle(agent.transform.forward, lookAtTarget);
            //agent.gameObject.transform.rotation = Quaternion.Slerp(agent.gameObject.transform.rotation, angleToPlayer, lerpSpeed * Time.fixedDeltaTime);

        }

        agent.targetVisualiser.transform.position = m_vectorTarget;

        // Finds the look at target vector
        Vector3 lookAtTarget = m_vectorTarget - agent.transform.position;

        // Calculate the force needed for the seeking behaviour
        force = (m_vectorTarget - agent.transform.position).normalized * agent.maxSpeed;

        // Add turning force to the fish
        agent.m_body.AddForce(force);

        // Find the rotation to the target
        Quaternion angleToTarget = Quaternion.LookRotation(lookAtTarget, Vector3.up);

        //// keeping the lerpSpeed look constant regardless of the angle difference
        //float lerpSpeed = 180.0f * rotationSpeed / Vector3.Angle(agent.transform.forward, lookAtTarget);

        //// Lerp the current rotation to the target rotation
        //agent.gameObject.transform.rotation = Quaternion.Slerp(agent.gameObject.transform.rotation, angleToTarget, lerpSpeed * Time.fixedDeltaTime);

        // Lerp the current rotation to the target rotation
        Vector3 lookDirection = Vector3.Slerp(agent.transform.forward, agent.m_body.velocity.normalized, m_rotationSpeed/*/agent.m_body.angularDrag*/);

        // Transform forward
        agent.transform.forward = lookDirection;

        //else if (m_target != null)
        //{
        //}
        return (agent.transform.forward * forwardSpeed);
    }

    // This functions sets the game object target
    public void SetTarget(GameObject target)
    {
        m_target = target;
        m_seekObject = true;
    }

    // This functions sets the target as a vector
    public void SetTarget(Vector3 target)
    {
        m_vectorTarget = target;
        m_seekObject = false;
    }


}
