using UnityEngine;
using System.Collections;

public class TransitionSprite : TransitionObject {
	public Texture2D myAboveTexture;
	public Texture2D myBelowTexture;
	
	void Start()
	{
		this.renderer.materials[1].mainTexture = myAboveTexture;
	}
	
	protected void handleTextureTransitions()
	{
		if(myState != myLastState)
		{
			if(myState == TileState.StateAbove)
			{
				this.renderer.materials[1].mainTexture = myAboveTexture;
			}
			else if (myState == TileState.StateBelow)
			{
				this.renderer.materials[1].mainTexture = myBelowTexture;
			}			
		}	
	}	
	
	void Update()
	{				
		handleStateTransitions();
		handleTextureTransitions();		
	}
}
