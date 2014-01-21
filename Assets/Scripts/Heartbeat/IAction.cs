using UnityEngine;
using System.Collections;

/**
 * @class   CommonAction
 * @brief   Specifies values common to all IAction implementations
 * @author  Eagan
 * @date    2/3/2012
 */
static class CommonAction
{
    /// Text const used to specify handle action method name...
    public const string HANDLE_ACTION_METHOD_NAME = "HandleAction";
}

/**
 * @enum    ACTION_TYPE
 * @brief   Values that can be specified to influence different sub-objects when SendMessage is used...
 */
public enum ACTION_TYPE
{
   PARENT_TRIGGER_ACTIVATED,
   PARENT_TRIGGER_DEACTIVATED,
   PUZZLE_SOLVED
}

/**
 * @enum    ACTION_STATE
 * @brief   Describes various states that can be used by IAction implementations
 */
public enum ACTION_STATE
{
    STATE_READY,
    STATE_ACTIVE,
    STATE_COMPLETE,
    STATE_DESTROY
}

/**
 * @interface   IAction
 * @brief   Pure virtual class used to specify common actions that can be performed by child components...
 * @author  Eagan
 * @date    2/3/2012
 */

public interface IAction
{
    /**
     * @fn  void HandleAction( ACTION_TYPE type );
     * @brief   Handle action.
     * @param   type    The type.
     */
   void HandleAction( ACTION_TYPE type );
}
