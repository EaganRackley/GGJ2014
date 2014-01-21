using UnityEngine;
using System.Collections;

public class LightGlow : TransitionObject, IAction {

	public int myRangeStart = 1;
	public int myRangeStop = 80;
	private float myTime = 0.0f;
	private bool myGlowEnabled = false;
	
	void Start()
	{
		myGlowEnabled = false;
		this.light.range = myRangeStart;
	}
	
	void Update()
	{
		if(myGlowEnabled == true)
		{
			myTime += Time.deltaTime;
			if(myTime > 0.05f)
			{
				myTime = 0.0f;
				if(this.light.range < myRangeStop)
				{
					this.light.range++;	
				}				
			}
		}
	}
	
	public void HandleAction (ACTION_TYPE type)
	{
		if (type == ACTION_TYPE.PARENT_TRIGGER_ACTIVATED) {
			myGlowEnabled = true;
		}
	}
}
