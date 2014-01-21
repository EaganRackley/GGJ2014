using UnityEngine;
using System.Collections;

public class TransitionBalloon: TransitionTile
{
	//public AudioSource myEffectTheme;
	public bool myBalloonHoldsPlayer = false;
	public float myFloatUpSpeed = -0.1f;
	public float myFloatDownSpeed = 0.01f;
			
	void OnTriggerEnter (Collider other)
	{
		if (mySelfDestructEnabled == false) {
			if (other.gameObject.tag == CommonValues.PLAYER_TAG) {					
				if (this.audio.isPlaying == false) {
					this.audio.Play ();
				}
				myBalloonHoldsPlayer = true;
			}								
		}		
	}
	
	void handleBalloonTransitions()
	{
		if( myState == TileState.StateAbove )
		{
			Vector3 pos = this.transform.position;
			pos.y += Time.deltaTime * myFloatUpSpeed;
			this.transform.position = pos;
		}
		else if( myState == TileState.StateBelow )
		{
			Vector3 pos = this.transform.position;
			pos.y += Time.deltaTime * myFloatDownSpeed;
			this.transform.position = pos;
		}
	}
	
	void Update ()
	{				
		handleStateTransitions ();
		handleTextureTransitions ();		
		handleBalloonTransitions();
	}
}
