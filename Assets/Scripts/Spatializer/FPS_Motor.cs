using UnityEngine;
using System.Collections;

[RequireComponent( typeof( CharacterController ) )]
public class FPS_Motor : MonoBehaviour {

    CharacterController controller;
    public float speed = 2f, rotate_speed = 0.3f;

    void Awake () {
        controller = GetComponent<CharacterController>();
    }
    
	void Update () {
        transform.Rotate( 0f, Input.GetAxis( "Mouse X" ) * rotate_speed, 0f );
        Vector3 move = new Vector3( Input.GetAxis( "Horizontal" ), 0f, Input.GetAxis( "Vertical" ) );
        move = transform.TransformDirection( move );
        move.Normalize();
        controller.SimpleMove( move * speed );
	}
}
