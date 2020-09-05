using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool CanBuildOn { get; private set; }
    public Tower Tower { get; set; }
    public int X { get; private set; }
    public int Y { get; private set; }

    public void Set(bool canBuildOn, int x, int y)
    {
        CanBuildOn = canBuildOn;
        X = x;
        Y = y;
    }
}
