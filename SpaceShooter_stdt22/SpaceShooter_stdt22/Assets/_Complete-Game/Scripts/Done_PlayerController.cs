using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	 
	private float nextFire;

	/* G421 */
	// Create EventInstance for the movement Events created in FMOD.
	// These are public because we need to call them when the ship is moving in any direction AND when it explodes and can no longer move
	//public FMOD.Studio.EventInstance moveHorizontalSound;
	//public FMOD.Studio.EventInstance moveVerticalSound;

	void Start()
	{
		/* G421 */
		// link FMOD Events to the EventInstance(s) created above
		// Link FMOD Events to the EventInstance created above
		// Paste your Event full path into CreateInstance("event:/path_to_event_in_FMOD")
		//moveHorizontalSound = FMODUnity.RuntimeManager.CreateInstance("");
		//moveVerticalSound = FMODUnity.RuntimeManager.CreateInstance("");
	}

	void Update ()
	{
		/* G421 */
		// Input Object presets are used to cue and stop movement sounds
		if (Input.GetButtonDown ("Horizontal"))
		{
			// play FMOD horizontal movement Event
			//moveHorizontalSound.start();
			//print ("now moving horizontally");
		}

		if (Input.GetButtonUp ("Horizontal"))
		{
			// quit FMOD horizontal movement event
			//moveHorizontalSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			//print ("not moving horizontally");
		}

		if (Input.GetButtonDown ("Vertical"))
		{
			// play FMOD vertical movement Event
			//moveVerticalSound.start();
			//print ("now moving vertically");
		}

		if (Input.GetButtonUp ("Vertical"))
		{
			// quit FMOD vertical movement event
			//moveVerticalSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			//print ("not moving vertically");
		}
		
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

			/* G421 */
			// player weapon randomized event via FMOD
			// Link FMOD Events to the EventInstance created above
			// Paste your Event full path into PlayOneShot("event:/path_to_event_in_FMOD")
			FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerExplosion");

			// original shot fired audio call was here //
			Debug.Log("player fires");
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
		
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}
}
