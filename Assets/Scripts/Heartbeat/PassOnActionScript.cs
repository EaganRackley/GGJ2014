using UnityEngine;
using System.Collections;

public class PassOnActionScript : TransitionObject, IAction
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public void HandleAction (ACTION_TYPE type)
	{
		for (int idx = 0; idx < transform.GetChildCount(); idx++) {
			transform.GetChild (idx).SendMessage (CommonAction.HANDLE_ACTION_METHOD_NAME, type, SendMessageOptions.DontRequireReceiver);
		}
		
	}
}
