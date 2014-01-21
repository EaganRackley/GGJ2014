/**
 * @def DEBUG
 * @brief   Defines whether or not assert functionality will be stripped from our code.
 * @author  Eagan
 * @date    1/24/2013
 */
#define DEBUG


using System;
using UnityEngine;

/**
 * @class   DebugUtils
 * @brief   Debug utilities. 
 * @author  Eagan
 * @date    1/24/2013
 */
public class DebugUtils
{

    /**
     * @fn  static void Assert(bool condition)
     * @brief   Handles assert behavior if debug is still defined
     * @author  Eagan
     * @date    1/24/2013
     * @exception   Exception   Thrown when exception.
     * @param   condition   true to condition.
     */
#if DEBUG
    public static void Assert(bool condition)
    {
        if (!condition) throw new Exception();
    }

    public static void Assert(bool condition, String message)
    {
        if (!condition)
        {
            Debug.Log(message);                        
            throw new Exception(message);
        }

    }
#endif
}