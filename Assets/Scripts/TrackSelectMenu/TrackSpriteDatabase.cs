using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TrackSpriteDatabase : ScriptableObject
{
    public Track[] track;

    public int TrackCount
    {
        get
        {
            return track.Length;
        }
    }

    public Track GetBike(int index)
    {
        return track[index];
    }

}
