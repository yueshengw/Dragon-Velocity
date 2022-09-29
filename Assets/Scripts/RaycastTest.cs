using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
	public GameObject Feet;
	public Rigidbody2D rb2d;
	private RaycastHit2D rh;
	public float multiplier;
    private void Start()
    {
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	void Update()
	{
		/**
		float laserLength = 1f;

		RaycastHit2D hit = Physics2D.Raycast(Feet.transform.position, Vector2.down * multiplier, laserLength);
		rb2d.velocity = new Vector2(rb2d.velocity.x - (hit.normal.x * 5), rb2d.velocity.y);
		if (hit.collider != null && hit.collider.tag != "Player")
		{
			//Hit something, print the tag of the object
			Debug.Log("Hitting: " + hit.collider.tag);
			rb2d.velocity = new Vector3(hit.normal.y, -hit.normal.x, 0);
		}
		
		Debug.DrawRay(Feet.transform.position, Vector2.down * multiplier, Color.red);
		*/
		/**
		//Length of the ray
		float laserLength = 0.5f;

		//Get the first object hit by the ray
		RaycastHit2D hit = Physics2D.Raycast(Feet.transform.position, Vector2.down*multiplier, laserLength);

		//If the collider of the object hit is not NUll
		if (hit.collider != null && hit.collider.tag != "Player")
		{
			//Hit something, print the tag of the object
			Debug.Log("Hitting: " + hit.collider.tag);
			//rh = Physics2D.Raycast(Feet.transform.position, -Feet.transform.up, 5);
			rb2d.velocity = new Vector3(hit.normal.y, -hit.normal.x, 0);
		}

		//Method to draw the ray in scene for debug purpose
		//Debug.DrawRay(Feet.transform.position, Vector2.down * laserLength, Color.red);
		Debug.DrawRay(Feet.transform.position, Vector2.down * multiplier, Color.red);
		**/
	}
}
