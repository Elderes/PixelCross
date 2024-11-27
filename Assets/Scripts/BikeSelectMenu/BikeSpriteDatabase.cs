using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BikeSpriteDatabase : ScriptableObject
{
    public Bike[] bike;

    public int BikeCount
    {
        get
        {
            return bike.Length;
        }
    }

    public Bike GetBike(int index)
    {
        return bike[index];
    }

}
