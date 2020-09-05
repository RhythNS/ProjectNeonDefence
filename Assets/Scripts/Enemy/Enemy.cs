using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour, ITargetable
{
    public Health Health { get; private set; }
    public int MoneyDrop { get; private set; }

    [SerializeField] private float speedForPassingTile;

    public float SpeedForPassingTile
    {
        get => speedForPassingTile;
        set => speedForPassingTile = value;
    }

    private List<Tile> path;
    private Tile targetWalkingTile;
    private int positionOnPath = 0;
    private int homeDamage;
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
        homeDamage = data.homeDamage;
        MoneyDrop = data.moneyDrop;
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
            // TODO: Replace when slowing effect is applied
            //timer += Time.deltaTime * slowDownPercentage;
            timer += Time.deltaTime;

            float percentage = timer / speedForPassingTile;

            this.transform.position = Vector3.Lerp(lastTilePassed, currentDestinationPoint, percentage);

            if (percentage >= 1)
            {
                if (++positionOnPath >= path.Count)
                {
                    OnHomeReached();
                    yield break;
                }

                SetNewDestination(false);
                timer = 0;
            }
            yield return null;
        }
    }

    private void OnHomeReached()
    {
        GameManager.Instance.RemainingHealth -= homeDamage;
        Destroy(this);
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

    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

}

