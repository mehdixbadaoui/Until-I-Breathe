using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GrapplingHook : MonoBehaviour
{

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

	public KeyCode keyGrapplin;

	void Update()
	{

		// Envois du grappin
		if (Input.GetKey(keyGrapplin) && isGrappling == false)
		{
			Grapple();
		}

		// Retrait du grappin
		else if ( (Input.GetKey(KeyCode.Space)) )
		{
			CutRope();
		}

		// On remonte la corde
		else if (isGrappling && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) )
		{
			MoveUp();
		}

		// On descend la corde
		else if (isGrappling && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
		{
			MoveDown();
		}

		// On descend la corde
		else if (isGrappling )
		{
			LR.SetPosition(0, transform.position);
			LR.SetPosition(1, location);

		}



	}

	// Envois du grappin
	public void Grapple()
	{
		// RayCast de "maxDistance" unite depuis le personnage vers _____.
		// Si ce raycast touche quelque chose c'est que la grappin est utilisable
		if (Physics.Raycast(perso.transform.position, perso.transform.up, out hit, maxDistance, surfaces))
		{
			isGrappling = true;
			location = hit.point;
			//FPC.Grappling = true;
			gameObject.GetComponent<Rigidbody>().useGravity = false;

			LR.SetPosition(1, location);
			LR.enabled = true;
		}

	}

	// Deplacement du joueur vers le point touche par le grappin
	public void MoveUp()
	{
		transform.position = Vector3.Lerp(transform.position, location, speed * Time.deltaTime / Vector3.Distance(transform.position, location));
		LR.SetPosition(0, transform.position);
		LR.SetPosition(1, location);

		// Quand on est trop proche la corde se decroche
		if (Vector3.Distance(transform.position, location) < 1f)
		{
			CutRope();
		}
	}

	// DÃ©placement du joueur vers le point touchÃ© par le grappin
	public void MoveDown()
	{
		transform.position = Vector3.Lerp(transform.position, location, -(speed * Time.deltaTime / Vector3.Distance(transform.position, location)));
		LR.SetPosition(0, transform.position);
		LR.SetPosition(1, location);

	}

	// Décrochage
	public void CutRope()
	{
		isGrappling = false;
		//FPC.Grappling = false;
		LR.enabled = false;
		gameObject.GetComponent<Rigidbody>().useGravity = true;
	}
}
