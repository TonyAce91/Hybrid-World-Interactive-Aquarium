using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTouchedCondition : Condition {

    public BallScript m_ball;
	public BallInteractBehaviour ballBehaviour;

    public override BehaviourResult UpdateBehaviour(AgentActor agent)
    {
        if (!m_ball)
        {
            Debug.Log("Ball Interaction Condition error");
            return BehaviourResult.ERROR;
        }
        if (m_ball.touched)
        {
            Debug.Log("Ball Touched");
			if (!ballBehaviour.initialised) {
				//ballBehaviour.Initialise ();
				agent.m_ballInteractTime = ballBehaviour.m_timer;

				ballBehaviour.initialised = true;
			}
            return BehaviourResult.SUCCESS;
        }
        return BehaviourResult.FAILURE;
    }


}
