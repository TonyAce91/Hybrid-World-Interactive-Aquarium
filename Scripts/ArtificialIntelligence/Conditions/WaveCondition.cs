using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCondition : Condition {

    public GameObject m_target;

    public override BehaviourResult UpdateBehaviour(AgentActor agent)
    {
        //if (m_target == null)
        //{
        //    Debug.Log("Wave Condition error");
        //    return BehaviourResult.ERROR;
        //}
        m_target = agent.player;
        if (m_target != null)
        {
            Debug.Log("Success wave condition");
            return BehaviourResult.SUCCESS;
        }
        return BehaviourResult.FAILURE;
    }
}
