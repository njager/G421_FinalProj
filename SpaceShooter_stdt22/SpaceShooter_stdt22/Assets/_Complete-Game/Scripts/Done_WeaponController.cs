using UnityEngine;
using System.Collections;

public class Done_WeaponController : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRateMin = 1f;
	public float fireRateMax = 2.5f;
	//public float delay; // used with original InvokeRepeating() to delay time to first shot

	/* G421 */
	// Create EventInstance for the enemy weapon Event(s) created in FMOD.
	//FMOD.Studio.EventInstance enemy_fire;

	void Start ()
	{
		//InvokeRepeating ("Fire", delay, fireRate);
		// ORIGINAL ^^^ but I want a new version with a random interval (BAD when enemy ships shoot simultaneously...)
		// vvv NEW VERSION with Invoke() does that after every enemy shot
		Invoke("Fire", fireRateMin);

		/* G421 */
		// Link FMOD Events to the EventInstance created above
		// Link FMOD Events to the EventInstance created above
		// Paste your Event full path into CreateInstance("event:/path_to_event_in_FMOD")
		//enemy_fire = FMODUnity.RuntimeManager.CreateInstance("");
	}

	void Fire ()
	{
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		/* G421 */
		// Use the next line to position your sounds Left to Right in the stereo field
		//YourEventNameGoesHere.setParameterByName("parameter_string", transform.position.x);
		// FMOD parameter sheet should run across values -6 (LEFT) to 6 (RIGHT)
		//enemy_fire.setParameterByName("", transform.position.x);
		// Once the pan has been determined, play the sound
		//enemy_fire.start();
		Debug.Log("ENEMY fires");

		Invoke("Fire", Random.Range(fireRateMin,fireRateMax));
	}
}
