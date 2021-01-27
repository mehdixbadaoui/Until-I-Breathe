using UnityEngine;
using System.Collections;
// Pour utiliser le FirstPersonController
using UnityStandardAssets.Characters.FirstPerson;

public class GrapplingHook : MonoBehaviour
{

	public Transform perso;
	public RaycastHit hit;

	public LayerMask surfaces;
	public int maxDistance = 50;

	public bool isMoving;
	public Vector3 location;

	public float speed = 10;
	public Transform hook;

	public RigidbodyFirstPersonController FPC;
	public LineRenderer LR;

	void Update()
	{

		// Envois du grappin
		if (Input.GetKey(KeyCode.G))
		{
			Grapple();
		}

		// Si le personnage vole, on l'envoie vers le point d'arrivÃ©e 
		if (isMoving)
		{
			MoveToSpot();
		}

		// Annulation / dÃ©crochage du grappin
		if (Input.GetKey(KeyCode.Space) && isMoving)
		{
			isMoving = false;
			//FPC.CanMove = true;
			FPC.Grappling = true;
			LR.enabled = false;
			gameObject.GetComponent<Rigidbody>().useGravity = true;
			//FPC.m_GravityMultiplier = 2;
		}


	}

	// Lors de l'envois du grappin
	public void Grapple()
	{
		// On crÃ©Ã© un raycast de "maxDistance" unitÃ©s depuis la camÃ©ra vers l'avant.
		// Si ce raycast touche quelque chose c'est que la grappin est utilisable
		if (Physics.Raycast(perso.transform.position, perso.transform.forward, out hit, maxDistance, surfaces))
		{
			System.Console.WriteLine("This is C#");
			isMoving = true;
			location = hit.point;
			//FPC.CanMove = false;
			FPC.Grappling = false;
			gameObject.GetComponent<Rigidbody>().useGravity = false;
			//FPC.m_GravityMultiplier = 1;
			LR.enabled = true;
			LR.SetPosition(1, location);
		}

	}

	// DÃ©placement du joueur vers le point touchÃ© par le grappin
	public void MoveToSpot()
	{
		transform.position = Vector3.Lerp(transform.position, location, speed * Time.deltaTime / Vector3.Distance(transform.position, location));
		LR.SetPosition(0, hook.position);

		// Si on est Ã  - de 1 unitÃ©(s) de la cible final on dÃ©croche le grappin automatiquement
		if (Vector3.Distance(transform.position, location) < 1f)
		{
			isMoving = false;
			//FPC.CanMove = true;
			FPC.Grappling = true;
			LR.enabled = false;
			gameObject.GetComponent<Rigidbody>().useGravity = true;
		}
	}
}
