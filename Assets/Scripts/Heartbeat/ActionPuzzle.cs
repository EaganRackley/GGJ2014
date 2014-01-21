using UnityEngine;
using System.Collections;

public class ActionPuzzle : TransitionObject, IAction
{
	public bool TriggerOnlyOnce = false;
	private bool myTriggerEntered = false;
	private bool myTriggerExited = false;
	
	void OnTriggerEnter (Collider other)
	{
		if (myTriggerEntered == false) {
			if (other.gameObject.tag == CommonValues.PLAYER_TAG) {
				for (int idx = 0; idx < transform.GetChildCount(); idx++) {
					transform.GetChild (idx).SendMessage (CommonAction.HANDLE_ACTION_METHOD_NAME, ACTION_TYPE.PARENT_TRIGGER_ACTIVATED, SendMessageOptions.DontRequireReceiver);
				}
			}			
		}
		// Set myTrigger entered to prevent this from happening again if TriggerOnlineOnce is set to true...
		if (other.gameObject.tag == CommonValues.PLAYER_TAG && TriggerOnlyOnce == true) {
			myTriggerEntered = true;
		}

       
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == CommonValues.PLAYER_TAG && myTriggerExited == false) {
			for (int idx = 0; idx < transform.GetChildCount(); idx++) {
				transform.GetChild (idx).SendMessage (CommonAction.HANDLE_ACTION_METHOD_NAME, ACTION_TYPE.PARENT_TRIGGER_DEACTIVATED, SendMessageOptions.DontRequireReceiver);
			}
		}

		// Set myTrigger entered to prevent this from happening again if TriggerOnlineOnce is set to true...
		if (other.gameObject.tag == CommonValues.PLAYER_TAG && TriggerOnlyOnce == true) {
			myTriggerExited = true;
		}
	}
	
	public void HandleAction (ACTION_TYPE type)
	{
		if (type == ACTION_TYPE.PUZZLE_SOLVED) {
			for (int idx = 0; idx < transform.GetChildCount(); idx++) {
				transform.GetChild (idx).SendMessage (CommonAction.HANDLE_ACTION_METHOD_NAME, ACTION_TYPE.PUZZLE_SOLVED, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
