using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class specifies the action of robots in game during patrol state
/// 
/// Code written by Antoine Kenneth Odi in 2018.
/// </summary>

public class BallInteractBehaviour : SteeringBehaviour
{

    // This is a list of all steering forces that produces this particular steering behaviour

	private BallScript m_ball = null;
    private SeekForce m_seekForce;
    public float m_timer = 10.0f;
	public float m_time;
    private Vector3 m_middlePoint = new Vector3(0, 0, 0);
    private float m_radius = 400f;
    private float m_waypointDistance = 100f;
	private Vector3 currentTarget;
	public bool m_waypointTarget = true;
	public bool initialised = false;


    public void SetParameters(BallScript ball, SeekForce seek, float timer, Vector3 middlePoint, float waypointDistance = 100f, float radius = 400f)
    {
        m_ball = ball;
        m_timer = timer;
        m_seekForce = seek;
        m_middlePoint = middlePoint;
        m_radius = radius;
        m_waypointDistance = waypointDistance;
		m_time = m_timer;
    }

	public void Initialise()
	{
		currentTarget = -(m_ball.gameObject.transform.position - m_middlePoint).normalized * m_waypointDistance;
		m_waypointTarget = true;
	}

    // Updates the behaviour
    public override BehaviourResult UpdateBehaviour(AgentActor agent)
    {

		if (agent.m_ballInteractTime <= 0) {
			initialised = false;
			m_ball.touched = false;
			Debug.Log ("Suppose to disengage");
			return BehaviourResult.FAILURE;
		}

		m_seekForce.SetTarget(m_ball.gameObject.transform.position);
		agent.AddForce(m_seekForce.GetForce(agent));


		//currentTarget = -(m_ball.gameObject.transform.position - m_middlePoint).normalized * m_waypointDistance;
		//
        //Vector3 agentPos = agent.gameObject.transform.position;
		//
		//if (m_waypointTarget) {
		//	m_ball.gameObject.GetComponent<Collider> ().enabled = false;
		//	if ((agent.transform.position - currentTarget).sqrMagnitude < 100f)
		//	{
		//		m_waypointTarget = false;
		//		m_ball.gameObject.GetComponent<Collider> ().enabled = true;
		//	}
		//
		//	Debug.Log ("Waypoint Target");
		//
		//	m_seekForce.SetTarget(currentTarget);
		//	agent.AddForce(m_seekForce.GetForce(agent));
		//
		//}
		//
		//if (!m_waypointTarget)
		//{
		//	m_seekForce.SetTarget(m_ball.gameObject.transform.position);
		//	agent.AddForce(m_seekForce.GetForce(agent));
		//
		//	Debug.Log ("Ball Target");
		//
		//	if ((m_ball.gameObject.transform.position - m_middlePoint).sqrMagnitude < 10f) {
		//		
		//		initialised = false;
		//		m_ball.touched = false;
		//		return BehaviourResult.FAILURE;
		//	}
		//}



//
//        if ((m_ball.transform.position - m_middlePoint).sqrMagnitude > m_radius)
//        {
//        }
//        else
//        {
//            Bop(agent);
//        }
        return BehaviourResult.SUCCESS;
    }

    public void ResetTime()
    {
        m_time = m_timer;
    }

 
//    private void seekWaypoint(AgentActor agent)
//    {
//    }
//
//    private void Bop(AgentActor agent)
//    {
//        m_seekForce.SetTarget(m_ball);
//        agent.AddForce(m_seekForce.GetForce(agent));
//    }
}
