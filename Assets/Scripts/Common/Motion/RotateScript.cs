//File:      FloatingScript.cs
//Desc:      Causes a script to float by an offset size (e.g. if it's 1.0 it'll go 0.5 up, and then 0.5 down using a gravity value)
//Date:      3/19/2011
//Author(s): Eagan Rackley

using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour
{
	public float XRotateSpeed = 0.0f;
	public float YRotateSpeed = 0.0f;
	public float ZRotateSpeed = 0.0f;
	
	private float myYRotateSpeed = 0.0f;
	private float myXRotateSpeed = 0.0f;
	private float myZRotateSpeed = 0.0f;

	/// <summary>
	/// Triggered when the instance is first activated
	/// </summary>
	void Awake()
	{
		myXRotateSpeed = XRotateSpeed;
		myYRotateSpeed = YRotateSpeed;
		myZRotateSpeed = ZRotateSpeed;
	}
	
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update() 
    {
        transform.Rotate(new Vector3(myXRotateSpeed, myYRotateSpeed, myZRotateSpeed));
	}
}
