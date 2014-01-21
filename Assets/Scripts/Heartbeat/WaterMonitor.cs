using UnityEngine;
using System.Collections;

public class WaterMonitor : MonoBehaviour
{		
	private static float myWaterLevel = 0.0f;
	private static float myVectorLength = 0.25f;
	private static float myAngle = 0.0f;
	private static float myAngleSpeed = 0.010f;
	private static bool myEndStateEnabled = false;

	void Start ()
	{
		myWaterLevel = transform.position.y;	
	}

	public static float WaterLevel {
		get {
			return myWaterLevel;
		}
	}
	
	public static void EnableEndState ()
	{
		myEndStateEnabled = true;	
	}

	void Update ()
	{        
		if (myEndStateEnabled == false) {
			myWaterLevel += myVectorLength * Mathf.Sin (myAngle);
			myAngle += myAngleSpeed;
			Vector3 pos = this.transform.position;
			pos.y = WaterMonitor.WaterLevel;			
			this.transform.position = pos;		
		} else {
			myWaterLevel = 1000.0f;
		}
	}    
	
}
