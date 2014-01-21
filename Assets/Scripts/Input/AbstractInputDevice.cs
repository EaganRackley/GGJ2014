using UnityEngine;
using System.Collections;

	public enum JUMP_STATE
	{
        PENDING,
		ACTIVE,
		INACTIVE
	}
	
	public enum INPUT_ACTION_STATE
	{
		ACTIVE,
		INACTIVE
	}
	
	public class InputData
	{
		public float Magnitude = new float();
		public float Angle = new float();	
		public JUMP_STATE JumpState = new JUMP_STATE();
		public INPUT_ACTION_STATE ActionState = new INPUT_ACTION_STATE();
	}
	
	///<summary>
	/// Provides interface that all player controlled non-gui input is to be provided through
	/// in conjunction with <see cref=">InputProvider"/>
	///</summary>
	public class AbstractInputDevice : MonoBehaviour 
	{		
		protected InputData myInputData = new InputData();
		
		public InputData Data
		{
			get
			{
				return myInputData;
			}
		}
		
		public virtual void HandleInput() {}
	}
