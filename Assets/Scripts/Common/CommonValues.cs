using UnityEngine;
using System.Collections;

/**
 * @class   CommonValues
 * @brief   Specifies common values shared between all scripts to prevent accidental typos.
 * @author  Eagan
 * @date    1/28/2012
 */
public static class CommonValues
{
		public const float UpAngleLow       = 315.0f;
		public const float UpAngleMid       = 0.0f;
		public const float UpAngleHigh      = 45.0f;
		
		public const float RightAngleLow    = 45.0f;
		public const float RightAngleMid    = 90.0f;
		public const float RightAngleHigh   = 135.0f;
		
		public const float DownAngleLow     = 135.0f;
		public const float DownAngleMid     = 180.0f;
		public const float DownAngleHigh    = 225.0f;
		
		public const float LeftAngleLow     = 225.0f;
		public const float LeftAngleMid     = 270.0f;
		public const float LeftAngleHigh    = 315.0f;

        public const string WALKING_LEFT    = "WalkingL";
        public const string WALKING_RIGHT   = "WalkingR";
        public const string STANDING_LEFT   = "StandingL";
        public const string STANDING_RIGHT  = "StandingR";
        public const string JUMPING_RIGHT   = "JumpingR";
        public const string JUMPING_LEFT    = "JumpingL";
        public const string CLIMBING        = "Climbing";

        public const string WATER_COLLIDER  = "Water";
        public const string GROUND_COLLIDER = "Ground";
        public const string WALL_COLLIDER   = "Wall";

		public const string PLAYER_TAG		= "Player";
		
		public const string BULLET_TAG = "Bullet";
		public const string GROUND_TAG = "Ground";
		public const string WAYPOINT_TAG = "Waypoint";
		public const string SOLID_WALL_TAG = "ReallySolidWall";
		
		public const string WAYPOINT_IMAGE = "WaypointGizmo.png";
		public const string SPAWN_IMAGE = "GirlSpawnGizmo.png";
		public const string GOAL_IMAGE = "GoalEndGizmo.png";
		public const string SNAKE_EMIT_IMAGE = "SnakeEmitterGizmo.png";

		public const double UPDATE_POSITION_DELAY = 0.1f;
		public const float ACTIVE_WAYPOINT_HEALTH = 1.0f;
		public const float INACTIVE_WAYPOINT_HEALTH = 0.0f;
		public const float BLOCK_RAYCAST_LENGTH = 4f;
}
