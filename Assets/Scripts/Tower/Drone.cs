using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour, ITargetable
{
    public DroneTower parentTower;

    public Enemy targetEnemy;
    public Vector2Int currentPos;
    public int damage;
    public float damagePerSeconds;

    public Health Health { get; private set; }

    [SerializeField] private float speedForPassingTile;

    public float SpeedForPassingTile
    {
        get => speedForPassingTile;
        set => speedForPassingTile = value;
    }

    private List<Tile> path;
    private Tile currentTile, targetWalkingTile;
    private int positionOnPath = 0;
    private Vector3 lastTilePassed;

    private Vector3 currentDestinationPoint;
    private bool newPathAvailable;
    private List<Tile> alternativePath;

    private void Awake()
    {
        Health = GetComponent<Health>();
        GameManager.Instance.AliveDrones.Add(this);
    }

    private void OnDestroy()
    {
        parentTower.ActiveDrones.Remove(this);
        GameManager.Instance.AliveDrones.Remove(this);

        if (currentTile.blockingTargets.Contains(this) == true)
            currentTile.blockingTargets.Remove(this);
        if (targetWalkingTile.blockingTargets.Contains(this) == true)
            targetWalkingTile.blockingTargets.Remove(this);
    }

    public Vector3 GetCurrentPosition() => transform.position;

    public GameObject GetGameObject() => gameObject;

    public void Set(DroneTower parent, Enemy target, float speed, int health, int damage, float damagePerSeconds)
    {
        this.parentTower = parent;
        this.targetEnemy = target;
        this.speedForPassingTile = speed;
        this.damage = damage;
        this.damagePerSeconds = damagePerSeconds;

        OnWorldChange();

        speedForPassingTile = speed;
        Health.Set(health);

        targetWalkingTile = path[0];
        SetNewDestination(true);

        StartCoroutine(Walk());
    }

    public IEnumerator Walk()
    {
        yield return null;
        bool enteredNewTile = false;
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;

            float percentage = timer / speedForPassingTile;

            this.transform.position = Vector3.Lerp(lastTilePassed, currentDestinationPoint, percentage);

            if (percentage >= 0.5 && !enteredNewTile)
            {
                enteredNewTile = true;

                if (currentTile.blockingTargets.Contains(this) == true)
                    currentTile.blockingTargets.Remove(this);
                if (targetWalkingTile.blockingTargets.Contains(this) == false)
                    targetWalkingTile.blockingTargets.Add(this);
            }
            if (percentage >= 1)
            {
                if (++positionOnPath >= path.Count)
                {
                    yield return new WaitForSeconds(1);
                }

                if (newPathAvailable)
                {
                    path = alternativePath;
                    newPathAvailable = false;
                    positionOnPath = 0;
                }
                else
                    OnWorldChange();

                //SetNewDestination(false);
                timer = 0;
                enteredNewTile = false;
            }

            yield return null;
        }
    }

    public void OnWorldChange()
    {
        // Create new path
        // ggf optimieren und schauen, wo zerstörtes Teil liegt

        if (targetEnemy == null || !targetEnemy)
        {
            targetEnemy = GetNewTarget(GameManager.Instance.AliveEnemies);
            if (targetEnemy == null)
                return;
        }

        Vector2Int currPosition = World.Instance.WorldToGrid(transform.position);
        Vector2Int targetPosition = World.Instance.WorldToGrid(targetEnemy.transform.position);
        currentTile = World.Instance.Tiles.Get(currPosition.x, currPosition.y);
        targetWalkingTile = World.Instance.Tiles.Get(targetPosition.x, targetPosition.y);

        if (currentTile.blockingTargets.Contains(this) == false)
            currentTile.blockingTargets.Add(this);

        alternativePath = new SimpleAStar(TowerManager.Instance.GetLocationsOfTowers()).GeneratePath(
            currentTile,
            targetWalkingTile);

        if (path[positionOnPath].Tower == null && alternativePath[1] == path[positionOnPath])
        {
            //New Path available and next tile is not affected  
            newPathAvailable = true;
            alternativePath.RemoveAt(0);
        }
        else
        {
            //Turn around and move one Tile backwards
            path = alternativePath;
            positionOnPath = 0;
            SetNewDestination(true);
        }

    }

    protected Enemy GetNewTarget(List<Enemy> enemiesInRange)
    {
        if (enemiesInRange == null || enemiesInRange.Count == 0)
            return null;

        Enemy nearestEnemy = enemiesInRange[0];
        float minDistance = Vector3.SqrMagnitude(enemiesInRange[0].transform.position - transform.position);
        float currDistance;
        for (int i = 1; i < enemiesInRange.Count; i++)
        {
            currDistance = Vector3.SqrMagnitude(enemiesInRange[i].transform.position - transform.position);
            if (minDistance > currDistance)
            {
                minDistance = currDistance;
                nearestEnemy = enemiesInRange[i];
            }
        }

        return nearestEnemy;
    }

    private void SetNewDestination(bool firstTime = false)
    {
        lastTilePassed = firstTime ? transform.position : currentDestinationPoint;
        targetWalkingTile = path[positionOnPath];
        currentDestinationPoint =
            World.Instance.GridToWorldMid(new Vector2Int(targetWalkingTile.X, targetWalkingTile.Y));

        transform.LookAt(targetWalkingTile.transform);
    }
}
