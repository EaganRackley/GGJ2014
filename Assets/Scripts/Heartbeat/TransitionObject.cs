using UnityEngine;
using System.Collections;

public enum TileState
{
	StateAbove,
	StateBelow
}

public class TransitionObject : MonoBehaviour {
	
	protected TileState myState = TileState.StateAbove;
	protected TileState myLastState = TileState.StateAbove;
	protected bool mySelfDestructEnabled = false;
		
	protected void TouchedByGhost()
	{
		mySelfDestructEnabled = true;
	}
	
	protected void handleStateTransitions()
	{
		if(WaterMonitor.WaterLevel > transform.position.y )
		{
			if(myState == TileState.StateAbove)
			{
				myLastState = myState;
				myState = TileState.StateBelow;
			}
			
		}
		else
		{
			if(myState == TileState.StateBelow)
			{
				myLastState = myState;
				myState = TileState.StateAbove;
			}			
		}
	}	
	
}
