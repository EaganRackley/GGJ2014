using UnityEngine;
using System.Collections;

public class RotatePlayerScript : TransitionObject, IAction
{
	public Transform myPlayerTransform;
	public float myLerpSpeed = 1.0f;
	private bool myStateEnabled = false;
	private float myLerp = 0.0f;
	
	// Update is called once per frame
	void Update ()
	{
		if (myStateEnabled == true) {
			myLerp += (Time.deltaTime * myLerpSpeed);
			if (myLerp >= 1.0f)
				myLerp = 1.0f;
			Vector3 pos = myPlayerTransform.position;
			pos.x = Mathf.Lerp (pos.x, this.transform.position.x, myLerp);
			pos.y = Mathf.Lerp (pos.y, this.transform.position.y, myLerp);
			pos.z = Mathf.Lerp (pos.z, this.transform.position.z, myLerp);
			myPlayerTransform.position = pos;
		}
	}
	
	public void HandleAction (ACTION_TYPE type)
	{
		if (type == ACTION_TYPE.PARENT_TRIGGER_ACTIVATED) {			
			myLerp = 0.0f;
			myStateEnabled = true;
			//mySpriteRenderer.EnableJumpStateForEva ();
			//WaterMonitor.EnableEndState();
		}
	}
}
