/**
* @file	RetroBoosterScript.cs
* @brief	Implements booster animation by changing Z randomization of attached particle system.
* @details  
* @since    1.0.0
* @version  1.0.0
* @package  Woebot Chatper 1
*/

using UnityEngine;
using System.Collections;


/**
* @class	BoosterScript
* @brief	Implements booster animation by changing Z randomization of attached particle system.
* @author	Eagan
* @date	1/6/2012
*/
[RequireComponent (typeof (ParticleAnimator))]
public class RetroBoosterScript : MonoBehaviour 
{
	public ParticleEmitter myParticleEmitter;
	
	/**
	* @fn	BoosterScript.Update
	* @brief	Raycasts to find out where the nearest ground object is and sets the booster animation based on the most appropriate value range.
	* @author	Eagan
	* @date	6/13/2011
	*/
	void Update () 
	{
		RaycastHit hitInfo = new RaycastHit();
		Vector3 down = transform.TransformDirection (Vector3.down);
		if( Physics.Raycast(transform.position, down, out hitInfo, 100.0f) ) 
		{
			float distanceToGround = hitInfo.distance;
			print("distance:" + distanceToGround.ToString());
			if( (distanceToGround <= 2.0f) && (distanceToGround > 1.0f) )
			{
				myParticleEmitter.emit = true;
			}
			else
			{
				myParticleEmitter.emit = false;
			}
		}
	}
}