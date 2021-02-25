using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
	[Header("Components")]


	public GameObject hook_detector;

	//Objects that will interact with the rope
	private GameObject hookObject;
	private Transform objectHanging;

	// Spring joint prefab
	public GameObject springjoint_rb_pref;


	public int timeLever;
	private bool isGrappling;


	//public float speed = 10;

	[Header("Rope")]


	public float lengthRopeMax = 50;
	public float lengthRopeMin = 2;


	[Header("Variables")]

	public LayerMask surfaces;

	//Movement script
	private Movement movements;

	//Line renderer
	private LineRenderer LR;

	private SpringJoint spring;

	private GameObject springJointRB;

	//A list with all rope sections
	private List<Vector3> distToHitPoints = new List<Vector3>();
	private List<Transform> ropePositions = new List<Transform>();

	//Hit point between the grapplin joint and the character
	private RaycastHit hit;

	//Hit point where the grapplin is attached
	private RaycastHit hitAttachedToGrapplin;

	public KeyCode keyGrapplin;
	private int countGrapplin;


	private bool hasChangedRope = false;
	private bool changeHook = false;
	private bool detachHook = false;
	private bool attachHook = false;


	//Rope data
	private float ropeLength;
	private float beginLengthMin = 2f;

	//Mass of what the rope is carrying
	private float loadMass = 7f;
	private float originalMass;

	private float dist_objects;

	// Main Character variables
	private Rigidbody body;
	private GameObject mainChar;

	//How fast we can add more/less rope
	float winchSpeed = 3f;

	void Start()
	{
		mainChar = gameObject;

		//Init the line renderer we use to display the rope
		LR = GetComponent<LineRenderer>();

		//Get the object transform
		objectHanging = transform;

		//Init the Rigidbody
		body = GetComponent<Rigidbody>();

		//Get rigidbodyCharacter component
		movements = GetComponent<Movement>();

		//Original mass of the body
		originalMass = body.mass;

	}

	void Update()
	{

		if (Input.GetKeyDown(keyGrapplin))
			attachHook = true;

		if (isGrappling)
		{
			Vector3 u_dir = (hookObject.transform.position - objectHanging.position) / dist_objects;

			Movement.distToHook = (hookObject.transform.position.y - objectHanging.position.y ) / ropeLength ;



			if (distToHitPoints.Count >= 3)
			{

				//TODO Toujours un pb avec les coins de rectangles ca se colle un peu, a regler a cause du *1.00001f qui est fait pour les surfaces incurvees type sphere
				for (int ropeId = 2; ropeId < ropePositions.Count; ropeId++)
				{
					if (!TheLineTouch(ropePositions[ropeId].position + distToHitPoints[ropeId] * 1.0001f, ropePositions[ropeId - 2].position + distToHitPoints[ropeId - 2], ropePositions[ropeId - 2]))
					{
						if (hit.transform != hookObject.transform && hit.transform != objectHanging)
						{
							//Debug.Log(ropePositions[ropePositions.Count - 3]);
							DeleteRopeJoint(ropeId);
						}
					}
				}

			}


			if (TheLineTouch(ropePositions[ropePositions.Count - 1].position + distToHitPoints[distToHitPoints.Count - 1], ropePositions[ropePositions.Count - 2].position + distToHitPoints[distToHitPoints.Count - 2], ropePositions[ropePositions.Count - 2]))
			{
				//TODO: add a method to update the joints if they are moving with an object (take the Transform instead of a list of vector)

				if (hit.transform != hookObject.transform && hit.transform != objectHanging && Vector3.Distance(hit.point, ropePositions[ropePositions.Count - 2].position + distToHitPoints[distToHitPoints.Count - 2]) > 0.2f)
					AddRopeJoint();

			}
			/*            else
						{
							float ray_obj = Vector3.Distance(hook.gameObject.GetComponent<MeshFilter>().sharedMesh.bounds.max, hook.gameObject.GetComponent<MeshFilter>().sharedMesh.bounds.min) / 2;
							raycastHits = Physics.Raycast(player, dir, out hitAttachedToGrapplin, dir.magnitude - ray_obj * 1.5f);
						}
			*/


			if (hookObject.tag == "hook")
				springJointRB.transform.position = ropePositions[ropePositions.Count - 2].position + distToHitPoints[distToHitPoints.Count - 2];


			DisplayRope();

			if (Input.GetKeyDown(KeyCode.Space))
				detachHook = true;

		}


		//The rope lenght changed
		if (hasChangedRope)
		{
			ropeLength = Mathf.Clamp(ropeLength, 1, lengthRopeMax);

			//Need to recalculate the k-value because it depends on the length of the rope
			UpdateRopePositions();
		}

	}

    private void FixedUpdate()
    {

		// Send Grapplin
		if ((attachHook || changeHook) && isGrappling == false && countGrapplin>10 )
		{

			attachHook = false;
			hookObject = hook_detector.GetComponent<hook_detector>().nearest_hook;


			if (hookObject && GetComponent<Movement>().enabled == true)
			{
				if (!Physics.Raycast( mainChar.GetComponent<Collider>().bounds.center , (hookObject.transform.position - mainChar.GetComponent<Collider>().bounds.center).normalized, out hit,
					Vector3.Distance(hookObject.transform.position, mainChar.GetComponent<Collider>().bounds.center)) &&
					!Physics.Raycast(hookObject.transform.position, (mainChar.GetComponent<Collider>().bounds.center - hookObject.transform.position).normalized, out hit,
					Vector3.Distance(hookObject.transform.position, mainChar.GetComponent<Collider>().bounds.center)))
				{
					Grapple();
					countGrapplin = 0;
					dist_objects = Vector3.Distance(hookObject.transform.position, objectHanging.position);
				}

			}

		}

		changeHook = false;

		// Retrait du grappin
		if ( detachHook && isGrappling == true)
		{
			CutRope();
			movements.JumpAfterGrapplin();
			detachHook = false;
		}


		// Retrait du grappin si on veut changer de crochet
		if (  attachHook && hookObject != hook_detector.GetComponent<hook_detector>().nearest_hook && isGrappling == true )  
		{
			attachHook = false;
			changeHook = true;
			CutRope();
			//movements.JumpAfterGrapplin();
		}

		//Comportements quand il y a un crochet détecté
		if (hookObject != null)
		{
			// Retrait du grappin au bout d'un certain temps sur le levier
			if (hookObject.tag == "lever" && countGrapplin > timeLever && isGrappling == true)
			{
				CutRope();
			}


			//When you grab the hook, the first behaviour of the rope is not a rigid line, only when you reach the end of the rope
			if (hookObject.tag != "lever")
			{
				if (isGrappling && countGrapplin < 5 && Vector3.Distance(ropePositions[ropePositions.Count - 2].position + distToHitPoints[distToHitPoints.Count - 2], objectHanging.position) > spring.minDistance && Vector3.Distance(ropePositions[ropePositions.Count - 2].position + distToHitPoints[distToHitPoints.Count - 2], objectHanging.position) < spring.maxDistance)
				{
					if (hookObject.tag == "hook")
						beginLengthMin = ropeLength - Vector3.Distance(ropePositions[ropePositions.Count - 2].position + distToHitPoints[distToHitPoints.Count - 2], objectHanging.position);
				}
			}
		}





		//Less rope
		if (isGrappling
			&& ((Input.GetAxisRaw("Vertical")==1 || Input.GetKey(KeyCode.W)) && (ropeLength > lengthRopeMin || ( hookObject.tag == "movable_hook" || hookObject.tag == "lever") ))
			&& ropeLength >= lengthRopeMin)
		{

			MoveUp();
			hasChangedRope = true;
		}

		//More rope
		else if (isGrappling
			&& ((Input.GetAxisRaw("Vertical") == -1 || Input.GetKey(KeyCode.W)) && ropeLength < lengthRopeMax && (Movement.isGrounded == false || (hookObject.tag == "movable_hook" || hookObject.tag == "lever" ) ))
			&& ropeLength <= lengthRopeMax)
		{
			MoveDown();
			hasChangedRope = true;
		}

		countGrapplin += 1;

	}

	// Envois du grappin
	public void Grapple()
	{
		beginLengthMin = 2f;

		isGrappling = true;

		if (hookObject.tag == "hook")
			Movement.isGrapplin = true;

		body.mass = loadMass;

		//rigidbodyCharacter.Grappling = true;

		//The first rope length is the distance between the two objects
		ropeLength = Vector3.Distance(hookObject.transform.position, objectHanging.position);


		//Add the weight to what the rope is carrying
		//GetComponent<Rigidbody>().mass = loadMass;


		// Add the Transforms to the list of rope positions
		ropePositions.Add(hookObject.transform);
		ropePositions.Add(objectHanging.transform);

		// Add the distances from the rope nodes to the hit points
		distToHitPoints.Add((objectHanging.transform.position - hookObject.transform.position).normalized * 0.2f /*whatTheRopeIsConnectedTo.GetComponent<SphereCollider>().radius*/ );
        //distToHitPoints.Add(Vector3.zero);
		distToHitPoints.Add(Vector3.zero);


		if (hookObject.tag == "hook")
			// Add the first spring joint
			AddSpringJoint();

		if (hookObject.tag == "movable_hook")
			// Add the first spring joint
			AddMovableSpringJoint();


		if (hookObject.tag == "lever")
			hookObject.transform.Rotate(-90,0,0);

		//Init the spring we use to approximate the rope from point a to b
		UpdateRopePositions();


		//Display the rope
		DisplayRope();

		LR.enabled = true;




	}

	// Add a spring joint
	public void AddSpringJoint()
	{
		//Add the spring joint component
		mainChar.AddComponent<SpringJoint>();
		spring = GetComponent<SpringJoint>();

		springJointRB = Instantiate(springjoint_rb_pref, ropePositions[0].position , Quaternion.identity);

		spring.connectedBody = springJointRB.GetComponent<Rigidbody>();
		spring.autoConfigureConnectedAnchor = false;
		spring.anchor = Vector3.zero;
		spring.connectedAnchor = Vector3.zero;

		//Add the value to the spring
		//spring.tolerance = 0.01f;
		spring.spring = 1000000000000f;
		spring.damper = 70f;

		spring.enableCollision = false;
	}

	public void AddMovableSpringJoint()
	{
		//Add the spring joint component
		mainChar.AddComponent<SpringJoint>();
		spring = GetComponent<SpringJoint>();

		spring.connectedBody = hookObject.GetComponent<Rigidbody>();
		spring.autoConfigureConnectedAnchor = false;
		spring.anchor = Vector3.zero;
		spring.connectedAnchor = Vector3.zero;

		//Add the value to the spring
		//spring.tolerance = 0.01f;
		spring.spring = 1000f;
		spring.damper = 70f;

		spring.enableCollision = true;
	}

	// Deplacement du joueur vers le point touche par le grappin
	public void MoveDown()
	{
		ropeLength += winchSpeed * Time.deltaTime;

/*		// Quand on est trop proche la corde se decroche
		if (Vector3.Distance(whatTheRopeIsConnectedTo.transform.position, whatIsHangingFromTheRope.position) < 1f)
		{
			CutRope();
		}*/
	}

	// DÃ©placement du joueur vers le point touchÃ© par le grappin
	public void MoveUp()
	{
		ropeLength -= winchSpeed * Time.deltaTime;
	}

	// Décrochage
	public void CutRope()
	{
		isGrappling = false;

		Destroy(spring);
		if (hookObject.tag == "hook")
			Destroy(springJointRB);
		distToHitPoints.Clear();
		ropePositions.Clear();
		LR.enabled = false;

		if (hookObject.tag == "hook")
        {
			Movement.isGrapplin = false;
			if (!changeHook /*&& body.velocity.y<1*/ && countGrapplin > 10 && !Movement.isGrounded)
				body.AddForce(new Vector3(0, movements.jump_force, 0), ForceMode.Impulse);
		}


		if (hookObject.tag == "lever")
			hookObject.transform.Rotate(90, 0, 0);


	}


	//Update the spring constant and the length of the spring
	private void UpdateRopePositions()
	{

		if (hookObject.tag == "hook")
		{
			//Update length of the rope
			spring.maxDistance = ropeLength;
			spring.minDistance = ropeLength - beginLengthMin;
		}

		if (hookObject.tag == "movable_hook")
		{
			//Update length of the rope
			spring.maxDistance = ropeLength;
			spring.minDistance = 1f;
		}

		//The rope changed
		hasChangedRope = false;


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



		ropePositions[ropePositions.Count - 1] = objectHanging.transform;
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

		positions[distToHitPoints.Count - 1] += new Vector3(0f, 1f, 0f);

		LR.SetPositions(positions);
	}

	// Add a new rope joint when the line touch a rigidbody
	private void AddRopeJoint()
	{


		// Add the transform of the object touched by the raycast
		ropePositions.RemoveAt(ropePositions.Count - 1);
		ropePositions.Add(hit.transform);
		ropePositions.Add(objectHanging.transform);

		// Place a hit distance between the transform of the object and the hit point
		distToHitPoints.RemoveAt(distToHitPoints.Count - 1);
		distToHitPoints.Add(hit.point - hit.transform.position);
		distToHitPoints.Add(Vector3.zero);


		ropeLength -= Vector3.Distance(ropePositions[ropePositions.Count - 2].position + distToHitPoints[distToHitPoints.Count - 2], ropePositions[ropePositions.Count - 3].position + distToHitPoints[distToHitPoints.Count - 3]);

		//The new joint manage the rigidbody
		//spring.connectedBody = hit.rigidbody;


		ropeLength = Vector3.Distance(ropePositions[ropePositions.Count - 1].position + distToHitPoints[distToHitPoints.Count - 1], ropePositions[ropePositions.Count - 2].position + distToHitPoints[distToHitPoints.Count - 2]);

		UpdateRopePositions();


	}


	//Display the rope with a line renderer
	private void DeleteRopeJoint( int ropeId )
	{

		//ropeLength += Vector3.Distance(ropePositions[ropePositions.Count - 2].position + distToHitPoints[distToHitPoints.Count - 2], ropePositions[ropePositions.Count - 3].position + distToHitPoints[distToHitPoints.Count - 3]) ;

		//Remove the joint created before and add again the main character joint
		ropePositions.RemoveAt(ropeId-1);

		//Remove the joint created before and add again the main character joint
		distToHitPoints.RemoveAt(ropeId - 1);

		// TODO: here the spring is connecting to the first attach point, change it when it's possible with the list of Objects instead of a list of vec3
		// spring.connectedBody = ropePositions[ropePositions.Count - 2].gameObject.GetComponent<Rigidbody>();

		//springJointRB.transform.position = ropePositions[ropePositions.Count - 2].position + distToHitPoints[distToHitPoints.Count - 2];


		ropeLength = Vector3.Distance( ropePositions[ropePositions.Count - 1].position + distToHitPoints[distToHitPoints.Count - 1], ropePositions[ropePositions.Count - 2].position + distToHitPoints[distToHitPoints.Count - 2]);

		UpdateRopePositions();


	}


	private bool TheLineTouch(Vector3 player, Vector3 hook_pos , Transform hook)
	{
		
		bool raycastHits = false;

		

		//Raycast( whatIsHangingFromTheRope.position , Vector3 direction, float maxDistance = Mathf.Infinity, int layerMask = DefaultRaycastLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal);

		Vector3 dir = hook_pos - player;

		float ray_obj = Vector3.Distance(hook.gameObject.GetComponent<MeshFilter>().sharedMesh.bounds.max, hook.gameObject.GetComponent<MeshFilter>().sharedMesh.bounds.min) / 2;
		raycastHits = Physics.Raycast(player, dir, out hit, dir.magnitude  /*- ray_obj * 1.5f*/);


		return raycastHits;
	}




	
	void OnDrawGizmos()
    {
		Gizmos.color = Color.yellow;

		if (distToHitPoints.Count >= 3)
			Gizmos.DrawSphere(ropePositions[ropePositions.Count - 3].position + distToHitPoints[distToHitPoints.Count - 3], 0.2f);

		if (distToHitPoints.Count >= 2)
			Gizmos.DrawSphere(ropePositions[ropePositions.Count - 2].position + distToHitPoints[distToHitPoints.Count - 2], 0.2f ) 
			/*Vector3.Distance(ropePositions[ropePositions.Count - 2].gameObject.GetComponent<MeshFilter>().sharedMesh.bounds.max, ropePositions[ropePositions.Count - 2].gameObject.GetComponent<MeshFilter>().sharedMesh.bounds.min) / 2)*/;

		Gizmos.color = Color.red;
		if (springJointRB!= null)
			Gizmos.DrawSphere(springJointRB.transform.position, 0.2f);
	}
	

}
