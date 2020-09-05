using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool CanBuildOn { get; private set; }
    public Tower Tower { get; set; }
    public int X { get; private set; }
    public int Y { get; private set; }

    public List<ITargetable> blockingTargets { get; private set; } = new List<ITargetable>();

    public void Set(bool canBuildOn, int x, int y)
    {
        CanBuildOn = canBuildOn;
        X = x;
        Y = y;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6))
            new GameObject("Test").transform.position = World.Instance.GridToWorld(new Vector2Int(X, Y));
    }
}
