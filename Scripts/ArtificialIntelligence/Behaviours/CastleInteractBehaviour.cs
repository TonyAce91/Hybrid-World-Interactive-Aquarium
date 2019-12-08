using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class specifies the action of robots in game during patrol state
/// 
/// Code written by Antoine Kenneth Odi in 2018.
/// </summary>

public class CastleInteractBehaviour : SteeringBehaviour
{

    private List<Vector3> m_waypoints = new List<Vector3>();

    // This is a list of all steering forces that produces this particular steering behaviour
    private SeekForce m_seekForce;

    private int m_currentWaypointNumber = 0;
    private Vector3 m_currentTarget;
    private CastleScript m_castle = null;
	private float distanceDetection = 2000.0f;


    public void SetParameters(SeekForce seekForce, CastleScript castle)
    {
        
        foreach (Transform waypoint in castle.waypoints)
        {
            m_waypoints.Add(waypoint.position);
        }
        m_seekForce = seekForce;
        m_currentWaypointNumber = 0;
        m_castle = castle;
    }

    // Updates the behaviour
    public override BehaviourResult UpdateBehaviour(AgentActor agent)
    {
        Vector3 agentPos = agent.gameObject.transform.position;
		//distanceDetection = agent.maxSpeed / 2f;
        //Debug.Log("agent Pos: " + agentPos);
        if ((agentPos - m_waypoints[m_currentWaypointNumber]).sqrMagnitude <= distanceDetection && m_currentWaypointNumber < m_waypoints.Count - 1)
        {
            m_currentWaypointNumber++;
            m_currentTarget = m_waypoints[m_currentWaypointNumber];
            //Debug.Log("Current Target: " + m_waypoints[m_currentWaypointNumber]);
        }
        else if (m_currentWaypointNumber >= m_waypoints.Count - 1)
        {
            if (m_castle != null)
                m_castle.castleTouched = false;
            m_currentWaypointNumber = 0;
            return BehaviourResult.FAILURE;
        }


        m_seekForce.SetTarget(m_waypoints[m_currentWaypointNumber]);
        agent.AddForce(m_seekForce.GetForce(agent));

        return BehaviourResult.SUCCESS;
    }
}
