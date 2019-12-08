using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the main class used to control the enemy AI
/// 
/// Code written by Antoine Kenneth Odi in 2017.
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class FishActor : AgentActor {


    //[HideInInspector] public Vector3 vectorTarget;

    // Steering behaviour of the enemy AI
    private SteeringBehaviour seekBehaviour;

    // Forces that controls the steering behaviours
    private SeekForce m_seekForce;

	[SerializeField] private float turnSpeed = 1.0f;
	[SerializeField] private Animator m_animator;
	[SerializeField] private float m_moveFowardForce = 10;
	[SerializeField] private float m_waypointTolerance = 50;



	[Header("Ball Interaction Behaviour")]
	[SerializeField] private Vector3 middleBallTarget;
	[SerializeField] private BallScript ball = null;


	[Header("Castle Interaction Behaviour")]
	[SerializeField] public List<Transform> waypoints = new List<Transform>();
    [SerializeField] private float speed = 0;

	[Header("Seek Behaviour")]
    [SerializeField] private Vector3 HandTransform;

	[Header("Wander Behaviour")]
	[SerializeField] private Vector2 middlePoint;
	[SerializeField] private float maxRadius;
	[SerializeField] private float maxDepth;



    private List<Vector3> waypointVectors = new List<Vector3>();
	CastleScript m_castleScript;

    // Use this for initialization
    void Start () {
        // Initialise behaviour list
		m_castleScript = FindObjectOfType<CastleScript>();
        m_behaviours = new List<IBehaviour>();

        m_body = GetComponent<Rigidbody>();
		if (!m_animator)
			m_animator = GetComponentInChildren<Animator> ();
	

		//-----------------------------------------------------------------
		// The Castle Interaction Sequence

		// Set up the seek force and seek force parameter
		SeekForce m_castleSeekForce = new SeekForce();
		m_castleSeekForce.m_rotationSpeed = turnSpeed;

		// Set waypoints
		foreach(Transform waypoint in waypoints)
		{
			waypointVectors.Add(waypoint.position);
		}

		// Set up the castle interaction behaviour
		CastleScript castle = FindObjectOfType<CastleScript>();
		CastleInteractBehaviour castleInteract = new CastleInteractBehaviour();
		castleInteract.SetParameters(m_castleSeekForce, castle);

		// Set up condition for castle interaction sequence
		CastleTouchedCondition castleCondition = new CastleTouchedCondition();
		castleCondition.castleScript = castle;

		//Set up chase sequence
		Sequence castleSequence = new Sequence();
		castleSequence.addBehaviour(castleCondition);
		castleSequence.addBehaviour(castleInteract);

        //-----------------------------------------------------------------
        // The Ball Interaction Sequence

        // Set up the seek force and seek force parameter
        SeekForce m_ballSeekForce = new SeekForce();
		m_ballSeekForce.m_rotationSpeed = turnSpeed;

        // Set up the ball interaction behaviour
		if (!ball)
			ball = FindObjectOfType<BallScript> ();
		BallInteractBehaviour ballInteract = new BallInteractBehaviour();
		ballInteract.SetParameters(ball, m_ballSeekForce, 10.0f, middleBallTarget);

        // Set up condition for ball interaction sequence
		BallTouchedCondition ballCondition = new BallTouchedCondition();
		ballCondition.m_ball = ball;
		ballCondition.ballBehaviour = ballInteract;

        //Set up chase sequence
        Sequence ballSequence = new Sequence();
		ballSequence.addBehaviour(ballCondition);
		ballSequence.addBehaviour(ballInteract);



        //-----------------------------------------------------------------
        // The Chase Sequence

        // Set up the seek force and seek force parameter
        /*SeekForce seekChase*/ m_seekForce = new SeekForce();
        m_seekForce.SetTarget(player);

        // Set up the seek behaviour
        seekBehaviour = new SteeringBehaviour();
        seekBehaviour.Constructor();
        seekBehaviour.AddNewForce(m_seekForce);

        // Set up condition for chase sequence
        WaveCondition waveCondition = new WaveCondition();
        //waveCondition.m_target = player;

        // Set up chase sequence
        Sequence chaseSequence = new Sequence();
        chaseSequence.addBehaviour(waveCondition);
        chaseSequence.addBehaviour(seekBehaviour);


        //----------------------------------------------------------------
        // The Wander Sequence

        // Set up seek force and seek parameter
        SeekForce seekWander = new SeekForce();
        seekWander.m_rotationSpeed = turnSpeed;

        // Set up rotational force and arrival parameter
        RotationalForce rotationWander = new RotationalForce();

        // Set up arrival force and arrival parameter
        ArrivalForce arrivalWander = new ArrivalForce();
        arrivalWander.SetParameter();

        // Set up wander behaviour
        LimitedWanderBehaviour wanderBehaviour = new LimitedWanderBehaviour();
        wanderBehaviour.SetLimit(middlePoint, maxRadius, maxDepth);
        wanderBehaviour.SetForce(seekWander, rotationWander, arrivalWander);
		wanderBehaviour.nearEnough = m_waypointTolerance;

        //----------------------------------------------------------------
        // The Main Selector

        // Set up main selector
        Selector mainSelector = new Selector();
		mainSelector.addBehaviour(castleSequence);
		mainSelector.addBehaviour (ballSequence);
        mainSelector.addBehaviour(chaseSequence);
        mainSelector.addBehaviour(wanderBehaviour);

        // Add all sequences to behaviour list
        m_behaviours.Add(mainSelector);

        // Setting the forward direction
        transform.forward = new Vector3(0, 0, 1);


    }

    //Vector3 m_currentTarget;

	// Update is called once per frame
	void FixedUpdate () {
		UpdateBehaviours ();
		speed = m_body.velocity.magnitude;
		player = GameObject.FindGameObjectWithTag ("Player Hand");
		if (player != null) {
			HandTransform = player.transform.position;
			m_seekForce.SetTarget (player.transform.position);
		}
		if (!m_castleScript.castleTouched) {
			m_body.AddForce (transform.forward * m_moveFowardForce);
		} else {
			m_body.AddForce (transform.forward * (m_moveFowardForce/2));
		}
	}

	void Update(){
		//m_animator.SetFloat("Speed", );
//		if (Vector3.Dot (m_body.velocity, transform.forward) >= 0.9f) {
//			m_animator.SetFloat ("Turn", 0);
//			//m_animator.speed = m_body
//		}


		if (m_ballInteractTime > 0)
		{
			m_ballInteractTime -= Time.fixedDeltaTime;
		}
		//Debug.Log ("Timer: " + m_ballInteractTime);

	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(middlePoint, new Vector3(maxRadius * 2, maxRadius * 2, maxDepth * 2));
	}


}
