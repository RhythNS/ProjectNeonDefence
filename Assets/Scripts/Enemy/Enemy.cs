using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Behaviour))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    public Health Health;

    [SerializeField] private Behaviour[] behaviour;

    [SerializeField] private float speed;

    [SerializeField] private float distanceThreshold;

    [SerializeField] private float timeUpdatePath;

    private List<Tile> path;
    private Vector3 targetWalkingTile;
    private Vector3 direction;

    public void Set(EnemyData data)
    {

    }

    private void Start()
    {
        StartCoroutine(UpdatePath());
        StartCoroutine(Walk());
    }

    /*
     * Keeps moving towards the next targetTile
     */
    public IEnumerator Walk()
    {
        while (true)
        {
            this.transform.position += direction * Time.deltaTime * speed;
            yield return null;
        }
    }

    /*
     * If the object is on the current target (distance <= threshold), update next target
     */
    public IEnumerator UpdatePath()
    {
        while(true){
            float dis = Vector3.Distance(this.transform.position, targetWalkingTile);
            if (dis <= distanceThreshold)
            {
                path.RemoveAt(0);
                if(path.Count <= 0 )
                {
                    //TODO Damage einbauen an der Basis (Theoretisch durch collider gelöst)
                    break;
                }
                targetWalkingTile = path[0].transform.position;
                direction = (targetWalkingTile - this.transform.position).normalized;
            }
            yield return new WaitForSeconds(timeUpdatePath);
        }
    }

    
    public void OnWorldChange()
    {
        //Create new path
        //ggf optimieren und schauen, wo zerstörtes Teil liegt
        Vector2Int currPosition = World.Instance.WorldToGrid(this.transform.position);
        Vector2Int homePosition = GameManager.Instance.CurrentLevel.worldGenSettings.homePosition;
        path = SimpleAStar.GeneratePath(World.Instance.Tiles.Get(currPosition.x, currPosition.y), World.Instance.Tiles.Get(homePosition.x, homePosition.y));

    }

}

