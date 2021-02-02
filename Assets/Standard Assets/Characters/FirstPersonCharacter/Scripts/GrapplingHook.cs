using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GrapplingHook : MonoBehaviour
{

	//Objects that will interact with the rope
	public GameObject whatTheRopeIsConnectedTo;
	public Transform whatIsHangingFromTheRope;

	//public ConfigurableJoint ropeJoint;

	public Transform perso;
	public RaycastHit hit;

	public LayerMask surfaces;
	public int maxDistance = 50;

	public bool isGrappling;
	private RigidbodyCharacter rigidbodyCharacter;


	public Vector3 location;

	public float speed = 10;
	public Transform hook;

	//public MovementInput FPC;
	public LineRenderer LR;

	private SpringJoint spring;


	//A list with all rope sections
	public List<Vector3> ropePositions = new List<Vector3>();

	public KeyCode keyGrapplin;


	//NEw code rope
	private bool distanceSet;
	public Rigidbody ropeHingeAnchorRb;
	public SpriteRenderer ropeHingeAnchorSprite;

	//Rope data
	private float ropeLength;
	private float minRopeLength = 1f;
	private float maxRopeLength = 20f;

	//Mass of what the rope is carrying
	private float loadMass = 3f;

	private float dist_objects;

	// Main Character variables
	private Rigidbody body;
	private GameObject mainChar;

	//How fast we can add more/less rope
	float winchSpeed = 2f;

	void Start()
	{
		mainChar = gameObject;

		//Get the configurable joint
		//ropeJoint = GetComponent<ConfigurableJoint>();

		//Init the line renderer we use to display the rope
		LR = GetComponent<LineRenderer>();


		//Init the Rigidbody
		body = GetComponent<Rigidbody>();

		//Get rigidbodyCharacter component
		rigidbodyCharacter = GetComponent<RigidbodyCharacter>();


	}

	void Update()
	{

		//Add more/less rope
		UpdateWinch();

		//Display the rope with a line renderer
		DisplayRope();



	}

	// Envois du grappin
	public void Grapple()
	{

		isGrappling = true;
		rigidbodyCharacter.Grappling = true;


		//The first rope length is the distance between the two objects
		ropeLength = Vector3.Distance(whatTheRopeIsConnectedTo.transform.position, whatIsHangingFromTheRope.position);


		//Add the weight to what the rope is carrying
		GetComponent<Rigidbody>().mass = loadMass;

		mainChar.AddComponent<SpringJoint>();
		spring = GetComponent<SpringJoint>();

		spring.connectedBody = whatTheRopeIsConnectedTo.GetComponent<Rigidbody>();
		spring.enableCollision = true;

		//Init the spring we use to approximate the rope from point a to b
		UpdateRopePositions();

		LR.enabled = true;

	}

	// Deplacement du joueur vers le point touche par le grappin
	public void MoveUp()
	{
		// Quand on est trop proche la corde se decroche
		if (Vector3.Distance(whatTheRopeIsConnectedTo.transform.position, whatIsHangingFromTheRope.position) < 1f)
		{
			CutRope();
		}
	}

	// DÃ©placement du joueur vers le point touchÃ© par le grappin
	public void MoveDown()
	{

	}

	// Décrochage
	public void CutRope()
	{
		isGrappling = false;
		Destroy(spring);
		//FPC.Grappling = false;
		LR.enabled = false;
		rigidbodyCharacter.Grappling = false;

	}


	//Update the spring constant and the length of the spring
	private void UpdateRopePositions()
	{
		//Someone said you could set this to infinity to avoid bounce, but it doesnt work
		//kRope = float.inf

		//
		//The mass of the rope
		//
		//Density of the wire (stainless steel) kg/m3
		float density = 7750f;
		//The radius of the wire
		float radius = 0.02f;

		float volume = Mathf.PI * radius * radius * ropeLength;

		float ropeMass = volume * density;

		//Add what the rope is carrying
		ropeMass += loadMass;


		//
		//The spring constant (has to recalculate if the rope length is changing)
		//
		//The force from the rope F = rope_mass * g, which is how much the top rope segment will carry
		float ropeForce = ropeMass * 9.81f;

		//Use the spring equation to calculate F = k * x should balance this force, 
		//where x is how much the top rope segment should stretch, such as 0.01m

		//Is about 146000
		float kRope = ropeForce / 0.01f;

		//print(ropeMass);

		//Add the value to the spring
		spring.spring = kRope * 1.0f;
		spring.damper = kRope * 0.8f;

		//Update length of the rope
		spring.maxDistance = ropeLength;


	}

		//Display the rope with a line renderer
		private void DisplayRope()
		{
/*			//This is not the actual width, but the width use so we can see the rope
			float ropeWidth = 0.2f;

			LR.startWidth = ropeWidth;
			LR.endWidth = ropeWidth;


			//Update the list with rope sections by approximating the rope with a bezier curve
			//A Bezier curve needs 4 control points
			Vector3 A = whatTheRopeIsConnectedTo.position;
			Vector3 D = whatIsHangingFromTheRope.position;

			//Upper control point
			//To get a little curve at the top than at the bottom
			Vector3 B = A + whatTheRopeIsConnectedTo.up * (-(A - D).magnitude * 0.1f);
			//B = A;

			//Lower control point
			Vector3 C = D + whatIsHangingFromTheRope.up * ((A - D).magnitude * 0.5f);

			//Get the positions
			BezierCurve.GetBezierCurve(A, B, C, D, ropePositions);


			//An array with all rope section positions
			Vector3[] positions = new Vector3[ropePositions.Count];

			for (int i = 0; i < ropePositions.Count; i++)
			{
				positions[i] = ropePositions[i];
			}*/


			//Just add a line between the start and end position for testing purposes
			Vector3[] positions = new Vector3[2];

			positions[0] = whatTheRopeIsConnectedTo.transform.position;
			positions[1] = whatIsHangingFromTheRope.transform.position;


			//Add the positions to the line renderer
			LR.positionCount = positions.Length;

			LR.SetPositions(positions);
		}
	

	//Add more/less rope
	private void UpdateWinch()
	{
		bool hasChangedRope = false;
		dist_objects = Vector3.Distance(whatTheRopeIsConnectedTo.transform.position, whatIsHangingFromTheRope.position);

		if (isGrappling)
        {
			//int K = 1000;
			//RigidbodyCharacter._isGrappling = true;
			Vector3 u_dir = (whatTheRopeIsConnectedTo.transform.position - whatIsHangingFromTheRope.position) / dist_objects;
			/*			SoftJointLimit softJointLimit = new SoftJointLimit();
						softJointLimit.limit = ropeLength;
						ropeJoint.linearLimit = softJointLimit;
			*/

			// spring.maxDistance = ropeLength;
			// spring.minDistance = ropeLength;

		}

		// Envoi du grappin
		if (Input.GetKey(keyGrapplin) && isGrappling == false)
		{

			Grapple();
		}

		// Retrait du grappin
		else if ((Input.GetKey(KeyCode.Space)) && isGrappling == true)
		{
			CutRope();
		}


		//More rope
		if (isGrappling
			&& (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
			&& ropeLength < maxRopeLength)
		{
			ropeLength += winchSpeed * Time.deltaTime;

			hasChangedRope = true;

			MoveUp();
		}

		else if (isGrappling
			&& (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.W))
			&& ropeLength > minRopeLength)
		{
			ropeLength -= winchSpeed * Time.deltaTime;

			hasChangedRope = true;

			MoveDown();
		}


		if (hasChangedRope)
		{
			ropeLength = Mathf.Clamp(ropeLength, minRopeLength, maxRopeLength);

			//Need to recalculate the k-value because it depends on the length of the rope
			UpdateRopePositions();
		}
	}

	/*
	void OnDrawGizmos()
    {

    }
	*/

}
