using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GrapplingHook : MonoBehaviour
{

	//Objects that will interact with the rope
	public Transform whatTheRopeIsConnectedTo;
	public Transform whatIsHangingFromTheRope;

	public ConfigurableJoint ropeJoint;

	public Transform perso;
	public RaycastHit hit;

	public LayerMask surfaces;
	public int maxDistance = 50;

	public bool isGrappling;
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


		//The first rope length is the distance between the two objects
		ropeLength = Vector3.Distance(whatTheRopeIsConnectedTo.position, whatIsHangingFromTheRope.position);

		//Init the spring we use to approximate the rope from point a to b
		UpdateRopePositions();

		//Add the weight to what the rope is carrying
		GetComponent<Rigidbody>().mass = loadMass;

		//mainChar.AddComponent<SpringJoint>();
		//spring = GetComponent<SpringJoint>();


		LR.enabled = true;

	}

	// Deplacement du joueur vers le point touche par le grappin
	public void MoveUp()
	{
		// Quand on est trop proche la corde se decroche
		if (Vector3.Distance(whatTheRopeIsConnectedTo.position, whatIsHangingFromTheRope.position) < 1f)
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
		//FPC.Grappling = false;
		LR.enabled = false;
		//gameObject.GetComponent<Rigidbody>().useGravity = true;
	}


	//Update the spring constant and the length of the spring
	private void UpdateRopePositions()
	{
/*
		// 2
		LR.positionCount = ropePositions.Count + 1;

		// 3
		for (var i = LR.positionCount - 1; i >= 0; i--)
		{
			if (i != LR.positionCount - 1) // if not the Last point of line renderer
			{
				LR.SetPosition(i, ropePositions[i]);

				// 4
				if (i == ropePositions.Count - 1 || ropePositions.Count == 1)
				{
					var ropePosition = ropePositions[ropePositions.Count - 1];
					if (ropePositions.Count == 1)
					{
						ropeHingeAnchorRb.transform.position = ropePosition;
						if (!distanceSet)
						{
							SoftJointLimit softJointLimit = new SoftJointLimit();
							softJointLimit.limit = Vector2.Distance(transform.position, ropePosition);
							ropeJoint.linearLimit = softJointLimit;
							distanceSet = true;
						}
					}
					else
					{
						ropeHingeAnchorRb.transform.position = ropePosition;
						if (!distanceSet)
						{
							SoftJointLimit softJointLimit = new SoftJointLimit();
							softJointLimit.limit = Vector2.Distance(transform.position, ropePosition);
							ropeJoint.linearLimit = softJointLimit;
							distanceSet = true;
						}
					}
				}

				// 5
				else if (i - 1 == ropePositions.IndexOf(ropePositions.Last()))
				{
					var ropePosition = ropePositions.Last();
					ropeHingeAnchorRb.transform.position = ropePosition;
					if (!distanceSet)

					{
						SoftJointLimit softJointLimit = new SoftJointLimit();
						softJointLimit.limit = Vector2.Distance(transform.position, ropePosition);
						ropeJoint.linearLimit = softJointLimit;
						distanceSet = true;
					}
				}


			}
			else
			{
				// 6
				LR.SetPosition(i, transform.position);
			}
		}
*/

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

			positions[0] = whatTheRopeIsConnectedTo.position;
			positions[1] = whatIsHangingFromTheRope.position;


			//Add the positions to the line renderer
			LR.positionCount = positions.Length;

			LR.SetPositions(positions);
		}
	

	//Add more/less rope
	private void UpdateWinch()
	{
		bool hasChangedRope = false;
		dist_objects = Vector3.Distance(whatTheRopeIsConnectedTo.position, whatIsHangingFromTheRope.position);

		if (isGrappling)
        {
			//int K = 1000;
			//RigidbodyCharacter._isGrappling = true;
			Vector3 u_dir = (whatTheRopeIsConnectedTo.position - whatIsHangingFromTheRope.position) / dist_objects;
/*			SoftJointLimit softJointLimit = new SoftJointLimit();
			softJointLimit.limit = ropeLength;
			ropeJoint.linearLimit = softJointLimit;
*/


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
