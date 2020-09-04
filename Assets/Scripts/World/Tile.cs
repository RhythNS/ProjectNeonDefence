using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool CanBuildOn { get; private set; }
    public Tower Tower { get; set; }
    public float X { get; private set; }
    public float Y { get; private set; }

    public void Set(bool canBuildOn, float x, float y)
    {
        CanBuildOn = canBuildOn;
        X = x;
        Y = y;


    }
}
