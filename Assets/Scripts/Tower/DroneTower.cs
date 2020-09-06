using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class DroneTower : Tower
{
    public int maxDrones;
    public float timeBetweenShots;
    public float currentCooldown;

    public GameObject dronePrefab;
    public float droneSpeed;
    public int droneHealth;
    public int droneDamage;
    public float droneDamagePerSeconds;
    public float droneAliveTime;

    private bool spawning;
    private UpgradePath nextUpgradePath;

    [SerializeField]
    private UpgradePath[] upgradePaths;

    public int Rank => rank;

    private int rank = 1;

    public List<Drone> ActiveDrones { get; private set; } = new List<Drone>();

    void Update()
    {
        if (spawning == true)
            return;

        currentCooldown += Time.deltaTime;
        if (currentCooldown >= timeBetweenShots && ActiveDrones.Count < maxDrones)
            StartCoroutine(Spawn());
    }

    protected override Enemy GetNewTarget()
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

    private IEnumerator Spawn()
    {
        Animator.SetBool("Spawn", true);
        spawning = true;

        Drone drone = Instantiate(dronePrefab, transform.position, Quaternion.identity).AddComponent<Drone>();
        drone.Set(this, targetEnemy, droneSpeed, droneHealth, droneDamage, droneDamagePerSeconds, droneAliveTime);
        drone.targetEnemy = targetEnemy;

        while (drone)
        {
            if (drone.Path != null && drone.Path.Count > 1)
            {
                yield return TurnToPath(drone.Path);
                drone.CanWalk = true;
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        spawning = false;
        Animator.SetBool("Spawn", false);
        currentCooldown = 0;
    }

    private IEnumerator TurnToPath(List<Tile> path)
    {
        float timer = 0;
        float toTurn = 0.5f;

        int difX = path[1].X - path[0].X;
        int difY = path[1].Y - path[0].Y;

        Vector3 rotation = new Vector3();
        if (difX == 1 && difY == 0)
            rotation = new Vector3(0, 90, 0);
        else if (difX == -1 && difY == 0)
            rotation = new Vector3(0, -90, 0);
        else if (difX == 0 && difY == 1)
            rotation = new Vector3(0, 0, 0);
        else if (difX == 0 && difY == -1)
            rotation = new Vector3(0, 180, 0);

        Quaternion from = transform.rotation;
        Quaternion to = Quaternion.Euler(rotation);

        bool repeat = true;
        while (repeat)
        {
            timer += Time.deltaTime;
            float perc = timer / toTurn;
            if (perc > 1)
            {
                perc = 1;
                repeat = false;
            }
            transform.rotation = Quaternion.Slerp(from, to, perc);
            yield return null;
        }
    }

    public override bool Upgrade()
    {
        if (nextUpgradePath == null) nextUpgradePath = upgradePaths[0];
        int currentMoney = MoneyManager.Instance.CurrentMoney;
        if (currentMoney < nextUpgradePath.Cost)
        {
            return false;
        }

        if (rank > upgradePaths.Length) return false;

        MoneyManager.Instance.CurrentMoney -= nextUpgradePath.Cost;
        CurrentValue += nextUpgradePath.Cost;

        this.droneDamage = nextUpgradePath.DroneDamage;
        this.maxDrones = nextUpgradePath.MaxDrones;


        if (rank < upgradePaths.Length)
            nextUpgradePath = upgradePaths[rank];
        rank++;
        return true;
    }

    [System.Serializable]
    public class UpgradePath
    {
        private int cost;
        private int droneDamage;
        private int maxDrones;

        public int Cost => cost;

        public int DroneDamage => droneDamage;

        public int MaxDrones => maxDrones;

    }

}
