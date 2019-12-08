//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

///// <summary>
///// This is used to create force to seek the target
///// 
///// Code written by Antoine Kenneth Odi in 2017.
///// </summary>

//public class SeekForce : SteeringForce
//{

//    private GameObject m_target;
//    private Vector3 m_vectorTarget;
//    private bool m_seekObject = true;
//    public float m_rotationSpeed = 1.0f;
//    private float m_weight = 0.7f;

//    public void SetParameter(float rotationSpeed = 1.0f, float weight = 0.7f)
//    {
//        m_rotationSpeed = rotationSpeed;
//        m_weight = weight;

//    }

//    public override Vector3 GetForce(AgentActor agent)
//    {
//        Vector3 force = new Vector3(0, 0, 0);

//        // This is used to set whether to seek a position or a game object
//        if (!m_seekObject)
//        {
//            // Finds the look at target vector
//            Vector3 lookAtTarget = m_vectorTarget - agent.transform.position;

//            // Calculate the force needed for the seeking behaviour
//            force = (m_vectorTarget - agent.transform.position).normalized * agent.MaxSpeed;

//            // Find the rotation to the target
//            Quaternion angleToTarget = Quaternion.LookRotation(lookAtTarget, Vector3.up);

//            // keeping the lerpSpeed look constant regardless of the angle difference
//            float lerpSpeed = 180.0f * m_rotationSpeed / Vector3.Angle(agent.transform.forward, lookAtTarget);

//            // Lerp the current rotation to the target rotation
//            agent.gameObject.transform.rotation = Quaternion.Slerp(agent.gameObject.transform.rotation, angleToTarget, lerpSpeed * Time.fixedDeltaTime);
//        }
//        else if (m_target != null)
//        {
//            Vector3 lookAtTarget = m_target.transform.position - agent.transform.position;
//            force = (m_target.transform.position - agent.transform.position).normalized * agent.MaxSpeed;
//            Quaternion angleToPlayer = Quaternion.LookRotation(lookAtTarget, Vector3.up);
//            float lerpSpeed = 180.0f * m_rotationSpeed / Vector3.Angle(agent.transform.forward, lookAtTarget);
//            agent.gameObject.transform.rotation = Quaternion.Slerp(agent.gameObject.transform.rotation, angleToPlayer, lerpSpeed * Time.fixedDeltaTime);
//        }
//        return (force - agent.m_body.velocity) * m_weight;
//    }

//    // This functions sets the game object target
//    public void SetTarget(GameObject target)
//    {
//        m_target = target;
//        m_seekObject = true;
//    }

//    // This functions sets the target as a vector
//    public void SetTarget(Vector3 target)
//    {
//        m_vectorTarget = target;
//        m_seekObject = false;
//    }


//}
