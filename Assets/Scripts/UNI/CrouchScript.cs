using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchScript : MonoBehaviour
{
    private Rigidbody _body;
    public KeyCode Crouch;
    private CapsuleCollider capsuleCollider;
    private RigidbodyCharacter rigidbodyCharacter;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        rigidbodyCharacter = GetComponent<RigidbodyCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(Crouch) && rigidbodyCharacter.Grappling == false)
        {
            capsuleCollider.height = 0.5f;
            capsuleCollider.center = new Vector3(0, 0.13f, 0.06f); 
        }
        else
        {
            capsuleCollider.height = 1.5f;
            capsuleCollider.center = new Vector3(0, 0.66f, 0.06f);
        }
            

    }
}
