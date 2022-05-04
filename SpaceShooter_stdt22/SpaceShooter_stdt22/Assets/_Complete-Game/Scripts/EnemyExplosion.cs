using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour {
	// Anything to declare? Do it up here...
	/* G421 */
	FMOD.Studio.EventInstance enemy_hits;

	// Use this for initialization
	void Start() {

		/* G421 */
		// Link FMOD Events to the EventInstance created above
		// Paste your Event full path into CreateInstance("event:/path_to_event_in_FMOD")  
		enemy_hits = FMODUnity.RuntimeManager.CreateInstance("event:/EnemyExplosion");

		// Watch the Console to get View Port values for the position of an exploding asteroid
		Debug.Log("enemy destroyed at " + transform.position.x);

		/* G421 */
		// Use the next line to position your sounds Left to Right in the stereo field
		//YourEventNameGoesHere.setParameterByName("parameter_string", transform.position.x);
		// FMOD parameter sheet should run across values -6 (LEFT) to 6 (RIGHT)
		enemy_hits.setParameterByName("exploded-at", transform.position.x);

		// Once the pan has been determined, play the sound
		/* G421 */
		enemy_hits.start();
	}
}
