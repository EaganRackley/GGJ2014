using UnityEngine;
using System.Collections;

public class TransitionTile : TransitionObject {
	public Texture2D myAboveTexture;
	public Texture2D myBelowTexture;
	public Texture2D myAboveGlowTexture;
	public Texture2D myBelowGlowTexture;		
	
	
	void Start()
	{
		this.renderer.material.mainTexture = myAboveTexture;
		mySelfDestructEnabled = false;
	}
		
	
	protected void handleAlphaFading ()
	{
		//(myState == TileState.StateAbove) 
		if ((mySelfDestructEnabled == true) 
			&& (this.renderer.material.color.a > 0.0f)) {				
			Color fadedColor = this.renderer.material.color;			
			fadedColor.a -= 0.05f;
			if (fadedColor.a < 0.0f) { 
				fadedColor.a = 0.0f;
			}
			this.renderer.material.color = fadedColor;			
			if(this.renderer.material.color.a <= 0.0f)
			{
				Destroy (this);
			}
		}
		
	}
	
	
	protected void handleTextureTransitions()
	{
		// If our object is being destroyed, swich to glow textures [specified]
		if(mySelfDestructEnabled == true)
		{
			if( myAboveGlowTexture != null )
			{
				myAboveTexture = myAboveGlowTexture;
			}
			if( myBelowGlowTexture != null )
			{
				myBelowTexture = myBelowGlowTexture;
			}
		}
		// Evaluate our state and assign textures according to state
		if(myState != myLastState)
		{
			if(myState == TileState.StateAbove)
			{
				this.renderer.material.mainTexture = myAboveTexture;
			}
			else if (myState == TileState.StateBelow)
			{
				this.renderer.material.mainTexture = myBelowTexture;
			}			
		}	
	}	
	
	void Update()
	{				
		handleStateTransitions();
		handleTextureTransitions();		
		handleAlphaFading();	
	}
}
