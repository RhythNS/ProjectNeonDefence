using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using TMPro;
using UnityEngine;

public class Drone : ITargetable
{
    public DroneTower parentTower;

    public Enemy targetEnemy;
    public Vector2Int currentPos;
    public int damage;
    public float damagePerSeconds;

    public bool CanWalk { get; set; }

    public Health Health { get; private set; }

    [SerializeField] private float speedForPassingTile;

    public List<Tile> Path { get; private set; }

    private Tile currentTile, targetWalkingTile;
    private Vector3 lastTilePassed;

    private Vector3 currentDestinationPoint;

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

    public void Set(DroneTower parent, Enemy target, float speed, int health, int damage, float damagePerSeconds, float aliveTime)
    {
        this.parentTower = parent;
        this.targetEnemy = target;
        this.speedForPassingTile = speed;
        this.damage = damage;
        this.damagePerSeconds = damagePerSeconds;

        Destroy(gameObject, aliveTime);

        CanWalk = false;
        Path = GetPath();
        currentTile.blockingTargets.Add(this);

        speedForPassingTile = speed;
        Health.Set(health);

        StartCoroutine(Walk());
    }

    public IEnumerator Walk()
    {
        yield return null;
        bool enteredNewTile = false;
        float timer = 0;
        while (true)
        {
            if (CanWalk == true)
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
                targetEnemy = GetNewTarget(GameManager.Instance.AliveEnemies);

                if (targetEnemy == null || !targetEnemy)
                    yield return new WaitForSeconds(1);
                else
                {
                    Path = GetPath();
                    timer = 0;
                    enteredNewTile = false;
                }
            }

            yield return null;
        }
    }

    private List<Tile> GetPath()
    {
        Vector2Int currPosition = World.Instance.WorldToGrid(transform.position);
        Vector2Int targetPosition = World.Instance.WorldToGrid(targetEnemy.transform.position);
        currentTile = World.Instance.Tiles.Get(currPosition.x, currPosition.y);
        targetWalkingTile = World.Instance.Tiles.Get(targetPosition.x, targetPosition.y);

        Path = new SimpleAStar().GeneratePath(
            currentTile,
            targetWalkingTile, true);
        SetNewDestination();
        return Path;
    }

    protected Enemy GetNewTarget(List<Enemy> enemiesInRange)
    {
        if (!this || enemiesInRange == null || enemiesInRange.Count == 0)
            return null;

        Enemy nearestEnemy = enemiesInRange[0];
        float minDistance = float.MaxValue;
        float currDistance;
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            if (!enemiesInRange[i])
                continue;

            currDistance = Vector3.SqrMagnitude(enemiesInRange[i].transform.position - transform.position);
            if (minDistance > currDistance)
            {
                minDistance = currDistance;
                nearestEnemy = enemiesInRange[i];
            }
        }

        return nearestEnemy;
    }

    private void SetNewDestination()
    {
        lastTilePassed = transform.position;
        if (Path == null || Path.Count <= 1)
        {
            currentDestinationPoint = transform.position;
            return;
        }
        targetWalkingTile = Path[1];
        currentDestinationPoint =
            World.Instance.GridToWorldMid(new Vector2Int(targetWalkingTile.X, targetWalkingTile.Y));

        transform.LookAt(targetWalkingTile.transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if (Path == null)
            return;
        Vector3 pos = Path[Path.Count - 1].transform.position;
        Gizmos.DrawLine(pos + new Vector3(0, -10, 0), pos + new Vector3(0, 10, 0));
    }
}
