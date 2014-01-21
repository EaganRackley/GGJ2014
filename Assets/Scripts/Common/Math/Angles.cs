//File:      Angles.cs
//Desc:      Provides delegated math functionality
//Date:      3/12/2011
//Author(s): Eagan Rackley

using UnityEngine;
using System.Collections;

namespace Common.Math
{
    public static class Angles
    {
        /**
         * @fn  public static float AdjustAngleRotation(float currentAngle, float targetAngle,
         *      float rotationSpeed)
         * @brief   Adjust angle rotation between two angles specified.
         * @author  Eagan
         * @date    1/31/2012
         * @param   currentAngle    The current angle.
         * @param   targetAngle     Target angle.
         * @param   rotationSpeed   The rotation speed.
         * @return  Adjusted angle.
         */
        public static float AdjustAngleRotation(float currentAngle, float targetAngle, float rotationSpeed)
        {
            float difference = currentAngle - targetAngle;
            float velocity = (rotationSpeed * Time.deltaTime);

            // If currentAngle minus targetAngle > 0 then velocity is 1 else velocity is -1
            if (difference < 0)
            {
                velocity = -velocity;
            }

            // If the player wants to rotate more than the speed at which we rotate...
            if (Mathf.Abs(difference) > Mathf.Abs(velocity))
            {
                // If absolute of currentAngle minus targetAngle is < 180 then we reverse our velocity	
                if (Mathf.Abs(difference) < 180.0f)
                {
                    velocity = -velocity;
                }

                // Add velocity to current angle
                currentAngle += velocity;

                // Adjust to see if we need to roll from 360->0 or 0->360
                if (currentAngle < 0.0f)
                {
                    currentAngle = 360.0f;
                }
                else if (currentAngle > 360.0f)
                {
                    currentAngle -= 360.0f;
                }
            }

            return currentAngle;
        }

        ///<summary>
        /// Routine that will return radians (0 to 255) from anle containing degrees (0 to 360)
        ///</summary>
        public static double GetRadiansFromAngle(float angle)
        {
            double dAngle = angle;
            return (dAngle * (System.Math.PI * 2.0f) / 360.0f);
            //return angle * Mathf.Deg2Rad;
            //return ( angle * ((Mathf.PI * 2.0f) / 360.0f) );
        }

        ///<summary>
        /// Routine that will modify an angle by a certain number of degrees and adjust for above or below 360..
        ///</summary>
        public static float ModifyAngleByDegrees(float angle, float modifier)
        {
            angle += modifier;
            // Adjust to see if we need to roll from 360->0 or 0->360
            if (angle < 0.0f)
            {
                angle = 360.0f;
            }
            else if (angle > 360.0f)
            {
                angle -= 360.0f;
            }

            return angle;
        }

        ///<summary>
        /// Returns the angle in degrees between two points
        ///</summary>
        public static float GetAngleBetweenTwoPoints(float x1, float y1, float x2, float y2)
        {
            float returnValue = 0.0f;
            if ((x1 != x2) && (y1 != y2))
            {
                // Get the angle in radians and convert to degrees, then correct for discontinuity (since arctan can return a negative)
                returnValue = (float)Mathf.Atan2((y2 - y1), (x2 - x1)) * 180.0f / Mathf.PI;
                returnValue = (float)(returnValue > 0.0 ? returnValue : (360.0 + returnValue));
            }

            return returnValue;
        }
    }
}
