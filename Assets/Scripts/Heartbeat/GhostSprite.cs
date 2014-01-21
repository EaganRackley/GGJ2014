//File:      SpriteRenderer.cs
//Desc:      Handles animation of sprite objects based on direction and/or depression? sigh :P
//Date:      12/16/2011
//Author(s): Eagan Rackley
using UnityEngine;
using System.Collections;

public class GhostSprite : TransitionObject
{
	// Public properties
	public float NumberOfColumns = 3.0f;
	public float NubmerOfRows = 11.0f;
	public float FramesPerSecond = 10.0f;
	public float myFollowSpeed = 0.1f;
	public CharacterController PlayerCharacterController;
    

	// Private representations of the values set publically
	private float myNumberOfColumns = 5.0f;
	private float myNubmerOfRows = 5.0f;
	private float myFrameLength;
	private double myNextFrame = 0;
	private Vector2 myOffsetValues;
	private Vector2 myCurrentOffset;
	private Renderer myRenderer;
	private int currentFrameCount = 0;
	private bool myEndState = false;
	
	public void EnableEndState ()
	{
		myEndState = true;
	}
    
	void Start ()
	{
		// Get the renderer for the material
		myRenderer = renderer;
		if (myRenderer == null) {
			Debug.Log ("Renderer gone wrong!!!");
			enabled = false;
		}
       
		// Set up private versions of these public values
		myNubmerOfRows = NubmerOfRows;
		myNumberOfColumns = NumberOfColumns;
		myOffsetValues = new Vector2 ((1.0f / myNumberOfColumns), (1.0f / myNubmerOfRows));
		myCurrentOffset = new Vector2 ((myOffsetValues.x * 1), 0.0f);

		// Figure out how often we need to update our texture offsets...
		myFrameLength = (1.0f / FramesPerSecond);
		myNextFrame = 0;
	}

	/**
     * @fn  void Update()
     * @brief   Updates this object.
     * @author  Eagan
     * @date    1/29/2012
     */
	void Update ()
	{
		// Move this 2d sprite on top of the character controller and handle mirroring etc. based on the fwd of the controller?
		// Thank you Sam you durned genius!
		//this.transform.position = PlayerCharacterController.transform.position;
		if (myEndState == false) {
			Vector3 pos = this.transform.position;
			pos.x = Mathf.Lerp (pos.x, PlayerCharacterController.transform.position.x, myFollowSpeed * Time.deltaTime);
			pos.y = Mathf.Lerp (pos.y, PlayerCharacterController.transform.position.y, myFollowSpeed * Time.deltaTime);
			this.transform.position = pos;		
		}
		// Change the left/right oreintation of player sprite based on forward direction...
		/*if (PlayerCharacterController.transform.forward.x >= 0)
        {
            Vector3 characterRotation = transform.rotation.eulerAngles;
            characterRotation.y = 180.0f;
            transform.rotation = Quaternion.Euler(characterRotation);
        }
        else
        {
            Vector3 characterRotation = transform.rotation.eulerAngles;
            characterRotation.y = 0.0f;
            transform.rotation = Quaternion.Euler(characterRotation);
        }*/

        
		// Update any sprite animations we might need
		if (Time.time > myNextFrame) {
			currentFrameCount++;
			myNextFrame = (Time.time + myFrameLength);
			myCurrentOffset.y += myOffsetValues.y;
            

			if (myCurrentOffset.y >= 1.0f) {
				myCurrentOffset.y = 0.0f;
			}
            
			//Debug.Log(" Offset x: " + myCurrentOffset.x.ToString() + " Offset y: " + myCurrentOffset.y.ToString() );
			//myRenderer.material.SetTextureOffset("_MainTex", myCurrentOffset);
			myRenderer.materials [1].SetTextureOffset ("_MainTex", myCurrentOffset);
		}
        
	}
}

//myCurrentFrameCount = 0;