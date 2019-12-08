using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class specifies the action of robots in game during patrol state
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

public class LimitedWanderBehaviour : SteeringBehaviour
{

    // This is a list of all steering forces that produces this particular steering behaviour
    private SeekForce m_seekForce;
    private ArrivalForce m_arrivalForce;

    private List<SteeringForce> m_forces = new List<SteeringForce>();
    private Vector2 m_middlePoint;
    private float m_maxRadius;
    private float m_maxDepth;
    private Vector3 m_currentTarget;

    private RotationalForce m_rotationalForce;
    private Vector3 m_prevTarget;
    private bool m_aimPrev = false;

	public float nearEnough;


    public void SetLimit(Vector2 middlePoint, float radius, float depth)
    {
        m_middlePoint = middlePoint;
        m_maxRadius = radius;
        m_maxDepth = depth;
		m_currentTarget = m_middlePoint;
    }

    // This is used to add new force for a specific behaviour
    public void SetForce(SeekForce seekForce, RotationalForce rotationalForce, ArrivalForce arrivalForce = null)
    {
        m_seekForce = seekForce;
        m_arrivalForce = arrivalForce;
        m_rotationalForce = rotationalForce;
    }
    public void SetForce(SeekForce seekForce)
    {
        m_seekForce = seekForce;
    }

    // Updates the behaviour
    public override BehaviourResult UpdateBehaviour(AgentActor agent)
    {
        Vector3 agentPos = agent.gameObject.transform.position;

        if ((agentPos - m_currentTarget).sqrMagnitude <= nearEnough)
        {
			Vector2 randomVector = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            randomVector = randomVector.normalized * Random.Range(0, m_maxRadius) + m_middlePoint;
            m_prevTarget = m_currentTarget;
            m_aimPrev = true;
            m_currentTarget = new Vector3(randomVector.x, randomVector.y, Random.Range(-m_maxDepth, m_maxDepth));
        }

        m_seekForce.SetTarget(m_currentTarget);
        agent.AddForce(m_seekForce.GetForce(agent));
		//Debug.Log ("Wander Current Target: " + m_currentTarget);

        //if (m_aimPrev)
        //{
        //    m_rotationalForce.SetTarget(m_prevTarget);
        //    agent.AddForce(m_rotationalForce.GetForce(agent));
        //    if (Vector3.Dot(agent.transform.forward, (agentPos - m_currentTarget)) > 0.8f)
        //        m_aimPrev = false;
        //}
        //else
        //{
        //    m_seekForce.SetTarget(m_currentTarget);
        //    agent.AddForce(m_seekForce.GetForce(agent));
        //    if (m_arrivalForce != null)
        //    {
        //        m_arrivalForce.SetTarget(m_currentTarget);
        //        agent.AddForce(m_arrivalForce.GetForce(agent));
        //    }
        //}


        //foreach (SteeringForce force in m_forces)
        //    agent.AddForce(force.GetForce(agent));

        return BehaviourResult.SUCCESS;
    }
}
