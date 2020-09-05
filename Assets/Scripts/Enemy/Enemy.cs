using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    public Health Health { get; private set; }

    [SerializeField] private float speedForPassingTile;

    private List<Tile> path;
    private Tile targetWalkingTile;
    private int positionOnPath = 0;
    private Vector3 lastTilePassed;

    private Vector3 currentDestinationPoint;

    private void Awake()
    {
        Health = GetComponent<Health>();
    }

    public void Set(EnemyData data, List<Tile> path)
    {
        this.path = path;
        speedForPassingTile = data.speed;
        Health.Set(data.health);
        targetWalkingTile = path[0];
        SetNewDestination(true);

        StartCoroutine(Walk());
    }

    /*
     * Keeps moving towards the next targetTile
     */
    public IEnumerator Walk()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;

            float percentage = timer / speedForPassingTile;

            if (percentage >= 1)
            {
                positionOnPath++;
                SetNewDestination(false);
                timer = 0;
            }
            this.transform.position = Vector3.Lerp(lastTilePassed, currentDestinationPoint, percentage);
            yield return null;
        }
    }

    
    public void OnWorldChange()
    {
        //Create new path
        //ggf optimieren und schauen, wo zerstörtes Teil liegt
        Vector2Int currPosition = World.Instance.WorldToGrid(this.transform.position);
        Vector2Int homePosition = GameManager.Instance.CurrentLevel.worldGenSettings.homePosition;
        path = SimpleAStar.GeneratePath(World.Instance.Tiles.Get(currPosition.x, currPosition.y), World.Instance.Tiles.Get(homePosition.x, homePosition.y));

        positionOnPath = 0;
        SetNewDestination(true);

    }

    private void SetNewDestination(bool firstTime = false)
    {
        lastTilePassed = firstTime ? transform.position : currentDestinationPoint;
        targetWalkingTile = path[positionOnPath];
        currentDestinationPoint = World.Instance.GridToWorldMid(new Vector2Int(targetWalkingTile.X, targetWalkingTile.Y));

        transform.LookAt(targetWalkingTile.transform);
    }

}

