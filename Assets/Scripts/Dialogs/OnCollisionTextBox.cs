using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextBoxDialog))]
public class OnCollisionTextBox : TransitionObject
{
	public bool myDialogRequiresKeypress = true;
	public string myTypeOfKeyRequired = "up";
	public int myStartingLineOfDialog = 0;
	public int myLinesOfDialogToShow = 1;
	private Collider myLastCollision = null;
	private bool myKeyHasBeenPressed = false;

	/**
     * @fn  void OnUpdate()
     * @brief   Handles the update action which processes key presses
     * @author  Eagan
     * @date    1/24/2013
     */
	void OnGUI ()
	{
		TextBoxDialog currentDialog = GetComponent<TextBoxDialog> ();
		if (currentDialog.TextBoxIsVisible () == false) {
			if (myLastCollision != null) {
				if ((myDialogRequiresKeypress == true) 
                    && (Input.GetKeyDown (myTypeOfKeyRequired) == true)
                    && (myKeyHasBeenPressed == false)) {
					myKeyHasBeenPressed = true;
					currentDialog.ShowTextBox (myStartingLineOfDialog, myLinesOfDialogToShow);
				}
			}
		}

		// Once we've indicated that the key has been pressed we need to ensure that we record
		// when it has been released so that the next time the users presses it a dialog is displayed
		if ((myKeyHasBeenPressed == true) && (Input.GetKeyUp (myTypeOfKeyRequired) == true)) {
			myKeyHasBeenPressed = false;
		}
	}

	/**
    * @fn  void OnTriggerEnter(Collider other)
    * @brief   Triggers the text box for this object when colliding with a character
    * @author  Eagan
    * @date    1/24/2013
    * @param   other   Other.
    */
	void OnTriggerEnter (Collider other)
	{
		TextBoxDialog currentDialog = GetComponent<TextBoxDialog> ();
		if (other.collider.gameObject.tag == CommonValues.PLAYER_TAG) {
			myLastCollision = other;
        

			if ((myDialogRequiresKeypress == false) & (currentDialog.TextBoxIsVisible () == false)) {
				currentDialog.ShowTextBox (myStartingLineOfDialog, myLinesOfDialogToShow);
			}
		
			if (this.audio != null) {
				if (this.audio.isPlaying == false) {
					this.audio.Play ();
				}
			}
		}
	}

	/**
     * @fn  void OnTriggerExit(Collider other)
     * @brief   Executes the trigger exit action.
     * @author  Eagan
     * @date    1/24/2013
     * @param   other   The other.
     */
	void OnTriggerExit (Collider other)
	{
		if (other.collider.gameObject.tag == CommonValues.PLAYER_TAG) {
			myLastCollision = null;
		}
	}
}
