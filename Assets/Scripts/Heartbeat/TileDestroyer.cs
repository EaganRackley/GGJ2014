using UnityEngine;
using System.Collections;

public class TileDestroyer : TransitionObject {
	//public Transform myGhostTransform;
			
	/*void OnCollisionEnter(Collision collision)
	{		
		
		if ( collision.collider.gameObject.tag == "Ground" )
		{
			//GameObject.Instantiate(explosionScript, transform.position, transform.rotation);
			Destroy(collision.collider.gameObject);
		}
	}*/
	void OnTriggerEnter(Collider other)
	{
		if( other.gameObject.tag == "Ground" )
		{
			other.gameObject.transform.SendMessage("TouchedByGhost");					
		}		
	}
	
	void Update()
	{
		//this.transform.position = myGhostTransform.position;
	}
}
