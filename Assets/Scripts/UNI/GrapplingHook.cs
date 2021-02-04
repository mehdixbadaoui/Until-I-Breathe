using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GrapplingHook : MonoBehaviour
{

    public GameObject player;
	//public hook_detector hook;
	public GameObject hook_detector;

	//Objects that will interact with the rope
	private GameObject whatTheRopeIsConnectedTo;
	public Transform whatIsHangingFromTheRope;

	//public ConfigurableJoint ropeJoint;

	public Transform perso;

	public LayerMask surfaces;
	public int maxDistance = 50;

	public bool isGrappling;
	private RigidbodyCharacter rigidbodyCharacter;


	public Vector3 location;

	public float speed = 10;

	//public MovementInput FPC;
	public LineRenderer LR;

	private SpringJoint spring;



	//A list with all rope sections
	public List<Vector3> distToHitPoints = new List<Vector3>();
	public List<Transform> ropePositions = new List<Transform>();
	private RaycastHit hit;

	public KeyCode keyGrapplin;



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
	float winchSpeed = 5f;

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

		// Add the first spring joint
		AddSpringJoint();

		// Add the Transforms to the list of rope positions
		ropePositions.Add(whatTheRopeIsConnectedTo.transform);
		ropePositions.Add(whatIsHangingFromTheRope.transform);

		// Add the distances from the rope nodes to the hit points
		distToHitPoints.Add(Vector3.zero);
        distToHitPoints.Add(Vector3.zero);

        //Init the spring we use to approximate the rope from point a to b
        UpdateRopePositions();

		LR.enabled = true;

	}

	// Add a spring joint
	public void AddSpringJoint()
	{
		//Add the spring joint component
		mainChar.AddComponent<SpringJoint>();
		spring = GetComponent<SpringJoint>();

		spring.connectedBody = whatTheRopeIsConnectedTo.GetComponent<Rigidbody>();
		spring.autoConfigureConnectedAnchor = false;
		spring.anchor = Vector3.zero;
		spring.connectedAnchor = Vector3.zero;

		//Add the value to the spring
		spring.spring = 1000f;
		spring.damper = 70f;

		spring.enableCollision = true;
	}

	// Deplacement du joueur vers le point touche par le grappin
	public void MoveUp()
	{
		ropeLength += winchSpeed * Time.deltaTime;

		// Quand on est trop proche la corde se decroche
		if (Vector3.Distance(whatTheRopeIsConnectedTo.transform.position, whatIsHangingFromTheRope.position) < 1f)
		{
			CutRope();
		}
	}

	// DÃ©placement du joueur vers le point touchÃ© par le grappin
	public void MoveDown()
	{
		ropeLength -= winchSpeed * Time.deltaTime;
	}

	// Décrochage
	public void CutRope()
	{
		isGrappling = false;
		Destroy(spring);
		distToHitPoints.Clear();
		ropePositions.Clear();
		//FPC.Grappling = false;
		LR.enabled = false;
		rigidbodyCharacter.Grappling = false;

	}


	//Update the spring constant and the length of the spring
	private void UpdateRopePositions()
	{
		/*		//Someone said you could set this to infinity to avoid bounce, but it doesnt work
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
		*/




		//Update length of the rope
		spring.maxDistance = ropeLength;
		spring.minDistance = ropeLength;


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


		ropePositions[ropePositions.Count - 1] = whatIsHangingFromTheRope.transform;
		distToHitPoints[distToHitPoints.Count - 1] = Vector3.zero;

		//Just add a line between the start and end position for testing purposes
		Vector3[] positions = new Vector3[distToHitPoints.Count];

		//positions[0] = whatTheRopeIsConnectedTo.transform.position;
		//positions[1] = whatIsHangingFromTheRope.transform.position;

		for (int i = 0; i < distToHitPoints.Count; i++)
		{
			positions[i] = ropePositions[i].position + distToHitPoints[i];
		}
		

		//Add the positions to the line renderer
		LR.positionCount = positions.Length;

		LR.SetPositions(positions);
	}

	// Add a new rope joint when the line touch a rigidbody
	private void AddRopeJoint()
	{

		// Add the transform of the object touched by the raycast
		ropePositions.RemoveAt(ropePositions.Count - 1);
		ropePositions.Add(hit.transform);
		ropePositions.Add(whatIsHangingFromTheRope.transform);

		// Place a hit distance between the transform of the object and the hit point
		distToHitPoints.RemoveAt(distToHitPoints.Count - 1);
		distToHitPoints.Add(hit.point - hit.transform.position);
		distToHitPoints.Add(Vector3.zero);

		//The new joint manage the rigidbody
		spring.connectedBody = hit.rigidbody;

		ropeLength = Vector3.Distance(ropePositions[ropePositions.Count - 1].position, ropePositions[ropePositions.Count - 2].position);

		UpdateRopePositions();


	}


	//Display the rope with a line renderer
	private void DeleteRopeJoint()
	{
		//Remove the joint created before and add again the main character joint
		ropePositions.RemoveAt(ropePositions.Count - 1);
		ropePositions.RemoveAt(ropePositions.Count - 1);
		ropePositions.Add(whatIsHangingFromTheRope.transform);

		//Remove the joint created before and add again the main character joint
		distToHitPoints.RemoveAt(distToHitPoints.Count - 1);
		distToHitPoints.RemoveAt(distToHitPoints.Count - 1);
		distToHitPoints.Add(Vector3.zero);

		// TODO: here the spring is connecting to the first attach point, change it when it's possible with the list of Objects instead of a list of vec3
		spring.connectedBody = ropePositions[ropePositions.Count - 2].gameObject.GetComponent<Rigidbody>();

		ropeLength = Vector3.Distance( ropePositions[ropePositions.Count - 1].position + distToHitPoints[distToHitPoints.Count - 1], ropePositions[ropePositions.Count - 2].position + distToHitPoints[distToHitPoints.Count - 2]);

		UpdateRopePositions();


	}


	private bool TheLineTouch(Vector3 player, Vector3 hook_pos , Transform hook)
	{
		
		bool raycastHits = false;

		

		//Raycast( whatIsHangingFromTheRope.position , Vector3 direction, float maxDistance = Mathf.Infinity, int layerMask = DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal);

		Vector3 dir = hook_pos - player;

		float ray_obj = Vector3.Distance(hook.gameObject.GetComponent<MeshFilter>().sharedMesh.bounds.max, hook.gameObject.GetComponent<MeshFilter>().sharedMesh.bounds.min) / 2;
		raycastHits = Physics.Raycast(player, dir, out hit, dir.magnitude - ray_obj * 1.5f);

		Debug.Log(-dir);


		return raycastHits;
	}


		//Add more/less rope
		private void UpdateWinch()
	{
		bool hasChangedRope = false;

		if (isGrappling)
        {
			Vector3 u_dir = (whatTheRopeIsConnectedTo.transform.position - whatIsHangingFromTheRope.position) / dist_objects;

			if (distToHitPoints.Count >= 3 )
            {
				if ( !TheLineTouch( ropePositions[ropePositions.Count - 1].position + distToHitPoints[distToHitPoints.Count - 1] , ropePositions[ropePositions.Count - 3].position + distToHitPoints[distToHitPoints.Count - 3], ropePositions[ropePositions.Count - 3] ) )
                {
					DeleteRopeJoint();
                }
            }


			if ( TheLineTouch( ropePositions[ropePositions.Count - 1].position + distToHitPoints[distToHitPoints.Count - 1] , ropePositions[ropePositions.Count - 2].position + distToHitPoints[distToHitPoints.Count - 2] , ropePositions[ropePositions.Count - 2] ) )
			{
				//TODO: add a method to update the joints if they are moving with an object (take the Transform instead of a list of vector)

				Debug.Log("AddRopeJoint");

				AddRopeJoint();
            }


			DisplayRope();

		}

		// Send Grapplin
		if (Input.GetKey(keyGrapplin) && isGrappling == false)
		{

            whatTheRopeIsConnectedTo = hook_detector.GetComponent<hook_detector>().nearest_hook;

            if (whatTheRopeIsConnectedTo)
            {
				if ( !Physics.Raycast(whatIsHangingFromTheRope.position, (whatTheRopeIsConnectedTo.transform.position - whatIsHangingFromTheRope.position).normalized , 
					Vector3.Distance( whatTheRopeIsConnectedTo.transform.position , whatIsHangingFromTheRope.position ) - 1.0f )  )
                {
					Grapple();
					dist_objects = Vector3.Distance(whatTheRopeIsConnectedTo.transform.position, whatIsHangingFromTheRope.position);
				}

            }

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

			MoveUp();
			hasChangedRope = true;
		}

		//Less rope
		else if (isGrappling
			&& (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.W))
			&& ropeLength > minRopeLength)
		{
			MoveDown();
			hasChangedRope = true;
		}


		//The rope lenght changed
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
