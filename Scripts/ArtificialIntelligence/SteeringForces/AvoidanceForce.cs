using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidanceForce : SteeringForce
{

    public override Vector3 GetForce(AgentActor agent)
    {
        return new Vector3(0, 0, 0);
    }
}
