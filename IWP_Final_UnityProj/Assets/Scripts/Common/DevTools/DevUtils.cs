using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class is basically just a bunch of shortcuts that I can use to make my life easier 
/// </summary>
public static class DevUtils
{
    /// <summary>
    /// Basically just prints an error message if the condition is met, good for error checking
    /// </summary>
    public static void Assert(bool condition, string message, LogType DebugLogType = LogType.Warning)
    {
        if (condition)
        {
            switch (DebugLogType)
            {
                case LogType.Log:
                    Debug.Log(message);
                    break;
                case LogType.Error:
                    Debug.LogError(message); 
                    break;
                case LogType.Warning:
                default:
                    Debug.LogWarning(message);
                    break;
            }
        }
    }
}
