using UnityEngine;
using System.Collections;

public class TransitionPuzzle: TransitionTile
{
	//public AudioSource myEffectTheme;
	public bool myPuzzleOpensDoors = false;
	
	void Start()
	{		
		this.collider.isTrigger = false;
	}
		
	protected void handleColliderTransitions ()
	{
		if ((myState != myLastState) && (mySelfDestructEnabled == false)) {
			if (myState == TileState.StateAbove) {
				this.collider.isTrigger = false;				
			} else if (myState == TileState.StateBelow) {
				this.collider.isTrigger = true;
			}			
		} else {
			this.collider.isTrigger = true;
		}
	}
		
	void OnTriggerEnter (Collider other)
	{
		if (mySelfDestructEnabled == false) {
			if (other.gameObject.tag == CommonValues.PLAYER_TAG) {
				if (myPuzzleOpensDoors == true) {							
					transform.parent.SendMessage (CommonAction.HANDLE_ACTION_METHOD_NAME, ACTION_TYPE.PUZZLE_SOLVED, SendMessageOptions.DontRequireReceiver);
				}
		
				if (this.audio.isPlaying == false) {
					this.audio.Play ();
				}
			}
						
			mySelfDestructEnabled = true;		
		}
		
	}
	
	void Update ()
	{				
		handleStateTransitions ();
		handleColliderTransitions ();
		handleTextureTransitions ();
		handleAlphaFading ();
	}
}
