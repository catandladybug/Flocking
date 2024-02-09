using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Flocker : Kinematic
{
    public class BehaviorAndWeight
    {
        public SteeringBehavior behavior;
        public float weight;
    }

    Arrive moveType1;
    Separation moveType2;
    LookWhereGoing myRotateType;

    BehaviorAndWeight[] behaviors = new BehaviorAndWeight[2];
    float maxAcceleration;
    float maxRotation;

    // Start is called before the first frame update
    void Start()
    {
        moveType1 = new Arrive();
        moveType1.character = this;
        moveType1.target = myTarget;
        BehaviorAndWeight behaviorWeight1 = new BehaviorAndWeight();
        behaviorWeight1.behavior = moveType1;
        behaviorWeight1.weight = 0.5f;

        moveType2 = new Separation();
        moveType2.character = this;
        List<Kinematic> kinematics = FindObjectsByType<Kinematic>(FindObjectsSortMode.None).ToList();
        kinematics.Remove(this);
        moveType2.targets = kinematics.ToArray();
        BehaviorAndWeight behaviorWeight2 = new BehaviorAndWeight();
        behaviorWeight2.behavior = moveType2;
        behaviorWeight2.weight = 1f;

        myRotateType = new LookWhereGoing();
        myRotateType.character = this;
        myRotateType.target = myTarget;

        behaviors[0] = behaviorWeight1;
        behaviors[1] = behaviorWeight2;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();

        foreach(BehaviorAndWeight b in behaviors)
        {
            Debug.Log(b);
            steeringUpdate.linear += b.weight * b.behavior.getSteering().linear;
            steeringUpdate.angular += b.weight * b.behavior.getSteering().angular;
        }
        //steeringUpdate.linear = myMoveType.getSteering().linear;
        //steeringUpdate.angular = myRotateType.getSteering().angular;
        base.Update();
    }
}
