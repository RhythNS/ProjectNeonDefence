using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour, ITargetable
{
    public Health Health { get; private set; }

    private Behaviour behaviour;
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
    private float slowDownPercentage = 1f;
    private SlowdownStatusEffect slowdownStatusEffect;

    public SlowdownStatusEffect SlowdownStatusEffect
    {
        get => slowdownStatusEffect;
        set
        {
            slowdownStatusEffect = value;
            if (value == null)
            {
                slowDownPercentage = 1f;
            }
            else slowDownPercentage = slowdownStatusEffect.slowdownPercentage;
        }
    }

    private Vector3 currentDestinationPoint;
    private bool newPathAvailable;
    private List<Tile> alternativePath;

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
        yield return null;
        behaviour = GetComponent<Behaviour>();
        bool enteredNewTile = false;
        float timer = 0;
        while (true)
        {
            // TODO: Replace when slowing effect is applied
            timer += Time.deltaTime * slowDownPercentage;
            //timer += Time.deltaTime;

            float percentage = timer / speedForPassingTile;

            this.transform.position = Vector3.Lerp(lastTilePassed, currentDestinationPoint, percentage);

            if (percentage >= 0.5 && !enteredNewTile)
            {
                behaviour.OnNewTileEntered();
                enteredNewTile = true;
            }
            if (percentage >= 1)
            {
                
                if (++positionOnPath >= path.Count)
                {
                    OnHomeReached();
                    yield break;
                }
                if (newPathAvailable)
                {
                    path = alternativePath;
                    newPathAvailable = false;
                }
                SetNewDestination(false);
                timer = 0;
                enteredNewTile = false;
            }

            yield return null;
        }
    }

    private void OnHomeReached()
    {
        GameManager.Instance.RemainingHealth -= homeDamage;
        Destroy(this.gameObject);
    }


    public void OnWorldChange()
    {
        //Create new path
        //ggf optimieren und schauen, wo zerstörtes Teil liegt
        Vector2Int currPosition = World.Instance.WorldToGrid(this.transform.position);
        Vector2Int homePosition = GameManager.Instance.CurrentLevel.worldGenSettings.homePosition;
        alternativePath = new SimpleAStar(TowerManager.Instance.getLocationsOfTowers()).GeneratePath(World.Instance.Tiles.Get(currPosition.x, currPosition.y),
            World.Instance.Tiles.Get(homePosition.x, homePosition.y));

        if (path[positionOnPath].Tower == null && alternativePath[1] == path[positionOnPath])
        {
            //New Path available and next tile is not affected  
            newPathAvailable = true;
            alternativePath.RemoveAt(0);
        }
        else
        {
            //Turn around and move one Tile backwards
            this.path = alternativePath;
            positionOnPath = 0;
            SetNewDestination(true);
        }

    }

    private void SetNewDestination(bool firstTime = false)
    {
        lastTilePassed = firstTime ? transform.position : currentDestinationPoint;
        targetWalkingTile = path[positionOnPath];
        currentDestinationPoint =
            World.Instance.GridToWorldMid(new Vector2Int(targetWalkingTile.X, targetWalkingTile.Y));

        transform.LookAt(targetWalkingTile.transform);
    }

    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }

    public GameObject GetGameObject()
    {
        if (this)
            return gameObject;
        return null;
    }
}