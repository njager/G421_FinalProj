/*****************************************************************************
 * DOCUMENTATION *
 * https://docs.google.com/document/d/1IyjVMLsTq1Bojb457LHmqeN3CQgzXBHpB-OdODrL-wk/edit?usp=sharing
 * Use the Google Doc link above to check the details of this code
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Done_GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public Text scoreText; 
	public Text startText;
	public Text restartText;
	
	private bool gameOver;
	private bool restart;
	public int score;

	// initialize timer value for game intro instructions
	private float timeLimit = 4.0f;

	// init game timer
	private float timePlayed;

	/* G421 */
	// Declare the FMOD instance for game music and ambience
	FMOD.Studio.EventInstance gameMusic;
	//FMOD.Studio.EventInstance spaceAmbience;

	void Start ()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		startText.text = "G421\nSPACE SHOOTER\n\nMove: W-A-S-D or arrows\nShoot: mouse or space";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());

		/* G421 */
		// Link FMOD Events to the EventInstance created above
		// Paste your Event full path into CreateInstance("event:/path_to_event_in_FMOD")
		gameMusic = FMODUnity.RuntimeManager.CreateInstance("event:/GameMusic");
		// cue the music to play
		gameMusic.start();
		// copy these lines for spaceAmbience if/as needed

	}
	
	void Update ()
	{
		// translate object for interval determined by timeLimit.
		if(timeLimit > 0) {
			// Decrease timeLimit.
			timeLimit -= Time.deltaTime;
		} else {
			startText.text = "";
			//Debug.Log("Time has expired!!");
		}

		/* G421 */
		// Need a counter to track time playing?
		timePlayed += Time.deltaTime;
		//Debug.Log ("Time Elapsed: "+timePlayed);


		//Use numeric values in the switch statement to change
		//parameter values based on time elapsed while playing
		
		switch (timePlayed)
		{
			case float val when val < 5:
				gameMusic.setParameterByName("Intensity", 0);
				break;
		case float val when val < 5:
				gameMusic.setParameterByName("Intensity", 1);
				break;
			default:
				break;
		}

		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

				/* G421 */
				// if you need to reset any audio when players hit "R" to reset, this is the place to do it
				// use FMOD AHDSR envelope to fade out background sound when player resets the game
				gameMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
				gameMusic.start();
				gameMusic.setParameterByName("Intensity", 0);
			}
		}
	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
			if (gameOver)
			{
				restartText.text = "Game Over\nPress 'R' for Restart";
				restart = true;
				break;
			}
		}
	}
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
		/* G421 */
		// use the current value of score to direct playback of any adaptive audio features in your music
		//gameMusic.setParameterByName("Intensity", score);
		if(score >= 50)
        {
			gameMusic.setParameterByName("Intensity", 2);
		}
		if (score >= 100)
		{
			gameMusic.setParameterByName("Intensity", 3);
		}
	}
	
	public void GameOver ()
	{
		restartText.text = "Game Over";
		gameOver = true;
		gameMusic.setParameterByName("Intensity", 4);
	}
}