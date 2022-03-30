using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosion : MonoBehaviour {

	// Use this for initialization
	void Start () {

		// set reference to the game object that hosts the script we need to access
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Done_PlayerController playerController = player.GetComponent<Done_PlayerController>();

		/* G421 */
		// call the 'fade out' routine when the player ship explodes and can no longer move
		//playerController.moveHorizontalSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		//playerController.moveVerticalSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

		/* G421 */
		// play the ship explosion sound
		// Paste your Event full path into PlayOneShot("event:/path_to_event_in_FMOD")
		//FMODUnity.RuntimeManager.PlayOneShot("");

	}
}
