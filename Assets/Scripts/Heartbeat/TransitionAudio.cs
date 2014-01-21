using UnityEngine;
using System.Collections;

public class TransitionAudio : TransitionObject {
	public AudioSource myAboveAudio;
	public AudioSource myBelowAudio;
	
	void Start()
	{
		myAboveAudio.Play();
	}
	
	protected void handleAudioTransitions()
	{
		if(myState != myLastState)
		{
			if(myState == TileState.StateAbove)
			{
				if(myBelowAudio.isPlaying)
				{
					myBelowAudio.Pause();	
					myAboveAudio.Play ();				
				}				
			}
			else if (myState == TileState.StateBelow)
			{
				if(myAboveAudio.isPlaying) 
				{
					myAboveAudio.Pause();	
					myBelowAudio.Play();				
				}
				
			}			
		}	
	}	
	
	void Update()
	{				
		handleStateTransitions();
		handleAudioTransitions();
	}
}
