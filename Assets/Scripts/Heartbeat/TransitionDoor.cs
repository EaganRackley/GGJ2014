using UnityEngine;
using System.Collections;

public enum DoorState
{
	DoorClosing,
	DoorClosed,
	DoorOpening,
	DoorOpen,
	DoorStopped
}

public class TransitionDoor : TransitionObject, IAction
{
	private bool myEmittersEnabled = false;
	private DoorState myDoorState = DoorState.DoorOpen;
	public Vector3 myOpenPosition = new Vector3 ();
	public Vector3 myClosedPosition = new Vector3 ();
	public float myDoorSpeed = 1.0f;
	public float myNumberOfPuzzles = 4;	
	public AudioSource mySolvedSound;
	private float myPuzzlesSolved = 0;
	private float myDoorLerp = 0.0f;
	private int lerpsToDo = 0;	
	
	void Start ()
	{
		myPuzzlesSolved = 0;
	}
	
	void EnableEmitters ()
	{
		myEmittersEnabled = true;
               
		foreach (ParticleEmitter emitter in GetComponentsInChildren<ParticleEmitter>(true)) {
			emitter.emit = true;
		}
	}
	
	void DisableEmitters ()
	{
		myEmittersEnabled = false;
		foreach (ParticleEmitter emitter in GetComponentsInChildren<ParticleEmitter>(true)) {
			emitter.emit = false;
		}
	}
	
	void FixedUpdate ()
	{
		if (lerpsToDo > 0 && (myDoorState == DoorState.DoorStopped || myDoorState == DoorState.DoorClosed) ) {
			if (myDoorState != DoorState.DoorStopped) {
				myDoorLerp = 0.0f;	
			}			
			myPuzzlesSolved++;
			myDoorState = DoorState.DoorOpening;			
			audio.Play ();
			lerpsToDo--;
		}
		
		if (myDoorState == DoorState.DoorClosing) {			
			myDoorLerp += (Time.deltaTime * myDoorSpeed);
			Vector3 pos = transform.localPosition;
			pos.x = Mathf.Lerp (myOpenPosition.x, myClosedPosition.x, myDoorLerp);
			pos.y = Mathf.Lerp (myOpenPosition.y, myClosedPosition.y, myDoorLerp);
			pos.z = Mathf.Lerp (myOpenPosition.z, myClosedPosition.z, myDoorLerp);
			transform.localPosition = pos;
			if (myDoorLerp >= 1.0f) {
				myDoorState = DoorState.DoorClosed;
			}
		}
		if (myDoorState == DoorState.DoorOpening) {
			myDoorLerp += (Time.deltaTime * myDoorSpeed);
			Vector3 pos = transform.localPosition;
			pos.x = Mathf.Lerp (myClosedPosition.x, myOpenPosition.x, myDoorLerp);
			pos.y = Mathf.Lerp (myClosedPosition.y, myOpenPosition.y, myDoorLerp);
			pos.z = Mathf.Lerp (myClosedPosition.z, myOpenPosition.z, myDoorLerp);
			transform.localPosition = pos;
			float targetLerp = (1.0f / myNumberOfPuzzles) * myPuzzlesSolved; 						
			if (myDoorLerp >= targetLerp) {
				myDoorState = DoorState.DoorStopped;
			}
			if (myDoorLerp >= 1.0f) {
				myDoorState = DoorState.DoorOpen;	
				//if( mySolvedSound.isPlaying == false) mySolvedSound.Play();
			}
			
		}
		// Enable emitters while rigidbody reports a veloicty on the y plane
    
		if (myDoorState == DoorState.DoorClosing || myDoorState == DoorState.DoorOpening) {
			if (myEmittersEnabled == false) {
				EnableEmitters ();
			}	
		} else if (myDoorState == DoorState.DoorClosed || myDoorState == DoorState.DoorOpen) {
			if (myEmittersEnabled == true) {
				DisableEmitters ();
			}		
		}
	}

	/**
    * @fn   public void HandleAction(ACTION_TYPE type)
    * @brief    When the trigger action for the parent of the door object sends a message with PARENT_TRIGGER_ACTIVATED then the door will turn on gravity and slam shut...
    * @author   Eagan
    * @date 2/3/2012
    * @param    type    The type.
    */
	public void HandleAction (ACTION_TYPE type)
	{
		if (type == ACTION_TYPE.PARENT_TRIGGER_ACTIVATED) {			
			myDoorLerp = 0.0f;
			myDoorState = DoorState.DoorClosing;
			audio.Play ();			
		} else if (type == ACTION_TYPE.PUZZLE_SOLVED) {
			if(myPuzzlesSolved < myNumberOfPuzzles)
			{
				lerpsToDo++;				
			}
			
		}
	}

}
