using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalUtils
{
    /*--Input Offset--*/
    // It is stored in milliseconds as an integer and is to be converted to a float in seconds when needed
    // The offset is how many seconds are to be waited before a pot is thrown
    // It is set when the beat counter is set to zero, with the input offset in seconds being added/subtracted from it
    public static string offsetPrefsName = "offset";

    public static int GetInputOffset()
    {
        // If there is no offset key, it is created and set to zero
        if (!PlayerPrefs.HasKey(offsetPrefsName))
            PlayerPrefs.SetFloat(offsetPrefsName, -130f);

        return PlayerPrefs.GetInt("offsetPrefsName");
    }
    
    public static void SetInputOffset(int newMs)
    {
        PlayerPrefs.SetInt("offsetPrefsName", newMs);
    }

    public static float GetInputOffsetInSeconds()
    {
        int offsetInMs = GetInputOffset();
        float offsetInSeconds = offsetInMs / 1000f;

        return offsetInSeconds;
    }
    /*------*/

    /*--Flow--*/
    // Flow is basically a percentage of notes hit in all songs, with each song contributing ~33% to it. The goal is to get to 100 flow
    public static string flowPrefsName = "flow";

    public static int GetFlow()
    {
        // Same as in input offset
        if (!PlayerPrefs.HasKey(flowPrefsName))
            PlayerPrefs.SetInt(flowPrefsName, 50);

        return PlayerPrefs.GetInt(flowPrefsName);
    }

    public static void SetFlow(int newFlow)
    {
        PlayerPrefs.SetInt(flowPrefsName, newFlow);
    }
    /*------*/
}
