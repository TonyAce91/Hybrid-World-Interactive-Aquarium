using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleTouchedCondition : Condition {

    public CastleScript castleScript;

    public override BehaviourResult UpdateBehaviour(AgentActor agent)
    {
        if (castleScript == null)
        {
            Debug.Log("Castle Interaction Condition error");
            return BehaviourResult.ERROR;
        }
        if (castleScript.castleTouched)
        {
            Debug.Log("Castle Touched");
            return BehaviourResult.SUCCESS;
        }
        return BehaviourResult.FAILURE;
    }

}
