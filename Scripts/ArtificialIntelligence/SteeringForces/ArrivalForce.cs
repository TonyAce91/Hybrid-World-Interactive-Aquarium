using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalForce : SteeringForce
{
    public float m_weight = 1.0f;
    private GameObject m_target;
    public Vector3 m_vectorTarget;
    //private bool m_seekObject = true;
    //public float rotationSpeed = 1.0f;
    public float m_radius = 50.0f;

    public void SetParameter(float radius = 50.0f, float weight = 0.5f)
    {
        m_radius = radius;
        m_weight = weight;
    }

    public override Vector3 GetForce(AgentActor agent)
    {
        Vector3 force = m_vectorTarget - agent.transform.position.normalized * agent.maxSpeed;
        float distance = Vector3.Distance(m_vectorTarget, agent.transform.position);
        if (distance < agent.maxSpeed)
            force *= distance / m_radius;
        return (force - agent.m_body.velocity) * m_weight;
    }

    public void SetTarget(Vector3 vectorTarget)
    {
        m_vectorTarget = vectorTarget;
    }
}
