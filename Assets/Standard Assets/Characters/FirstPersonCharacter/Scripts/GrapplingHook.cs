using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;


public class GrapplingHook : MonoBehaviour
{

	//Objects that will interact with the rope
	public Transform whatTheRopeIsConnectedTo;
	public Transform whatIsHangingFromTheRope;


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


	//A list with all rope sections
	public List<Vector3> allRopeSections = new List<Vector3>();

	public KeyCode keyGrapplin;

	//Rope data
	private float ropeLength = 1f;
	private float minRopeLength = 1f;
	private float maxRopeLength = 20f;

	//Mass of what the rope is carrying
	private float loadMass = 100f;

	//How fast we can add more/less rope
	float winchSpeed = 2f;


	//The joint we use to approximate the rope
	SpringJoint springJoint;


	void Start()
	{
		//Init the line renderer we use to display the rope
		LR = GetComponent<LineRenderer>();
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
		// RayCast de "maxDistance" unite depuis le personnage vers _____.
		// Si ce raycast touche quelque chose c'est que la grappin est utilisable
		/*		if (Physics.Raycast(perso.transform.position, perso.transform.up, out hit, maxDistance, surfaces))
				{

					isGrappling = true;
					location = hit.point;
					//FPC.Grappling = true;
					//gameObject.GetComponent<Rigidbody>().useGravity = false;

					LR.SetPosition(1, location);
					LR.enabled = true;
				}*/

		isGrappling = true;

		//gameObject.GetComponent<Rigidbody>().useGravity = false;

		springJoint = whatTheRopeIsConnectedTo.GetComponent<SpringJoint>();

		//Init the spring we use to approximate the rope from point a to b
		UpdateSpring();

		//Add the weight to what the rope is carrying
		GetComponent<Rigidbody>().mass = loadMass;

		LR.enabled = true;

	}

	// Deplacement du joueur vers le point touche par le grappin
	public void MoveUp()
	{
		/*		characterController.SimpleMove( Vector3.Lerp(transform.position, location, speed * Time.deltaTime / Vector3.Distance(transform.position, location)));
				LR.SetPosition(0, transform.position);
				LR.SetPosition(1, location);*/

		// Quand on est trop proche la corde se decroche
		if (Vector3.Distance(whatTheRopeIsConnectedTo.position, whatIsHangingFromTheRope.position) < 1f)
		{
			CutRope();
		}
	}

	// DÃ©placement du joueur vers le point touchÃ© par le grappin
	public void MoveDown()
	{
		/*		transform.position = transform.position - Vector3.Lerp(transform.position, location, 1-(speed * Time.deltaTime / Vector3.Distance(transform.position, location)));
				LR.SetPosition(0, transform.position);
				LR.SetPosition(1, location);*/

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
	private void UpdateSpring()
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
		springJoint.spring = kRope * 1.0f;
		springJoint.damper = kRope * 0.8f;

		//Update length of the rope
		springJoint.maxDistance = ropeLength;
	}

	//Display the rope with a line renderer
	private void DisplayRope()
	{
		//This is not the actual width, but the width use so we can see the rope
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
		BezierCurve.GetBezierCurve(A, B, C, D, allRopeSections);


		//An array with all rope section positions
		Vector3[] positions = new Vector3[allRopeSections.Count];

		for (int i = 0; i < allRopeSections.Count; i++)
		{
			positions[i] = allRopeSections[i];
		}

		//Just add a line between the start and end position for testing purposes
		//Vector3[] positions = new Vector3[2];

		//positions[0] = whatTheRopeIsConnectedTo.position;
		//positions[1] = whatIsHangingFromTheRope.position;


		//Add the positions to the line renderer
		LR.positionCount = positions.Length;

		LR.SetPositions(positions);
	}

	//Add more/less rope
	private void UpdateWinch()
	{
		bool hasChangedRope = false;


		// Envois du grappin
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
			UpdateSpring();
		}
	}

}
