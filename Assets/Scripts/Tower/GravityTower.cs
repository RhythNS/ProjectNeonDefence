﻿using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GravityTower : Tower
{
    public GravityTowerBullet basedBullet;

    public float currentCooldown;

    public float cooldownBetweenShots;

    public float slowdownPercentage;
    public float slowdownTime;


    public int Rank => rank;
    private int rank = 1;

    [SerializeField] public UpgradePath[] upgradePaths;

    public UpgradePath NextUpgradePath
    {
        get => nextUpgradePath;
        set => nextUpgradePath = value;
    }

    private UpgradePath nextUpgradePath;

    public SlowdownStatusEffect slowdownStatusEffect { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        slowdownStatusEffect = new SlowdownStatusEffect(slowdownPercentage, slowdownTime);
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= cooldownBetweenShots)
        {
            currentCooldown = 0;
            ShootGravityBoolet();
        }
    }

    void ShootGravityBoolet()
    {
        /*if (targetEnemy != null)
        {
            var newBoolet = Instantiate(basedBullet);
            newBoolet.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            newBoolet.Target = targetEnemy;
            newBoolet.slowdownStatusEffect = new SlowdownStatusEffect(slowdownPercentage,slowdownTime);
        }*/
        Collider[] aoeColliders = Physics.OverlapSphere(transform.position, Range);
        for (var i = 0; i < aoeColliders.Length; i++)
        {
            var collider = aoeColliders[i];
            if (collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                StartCoroutine(GravityRoutine(enemy));
            }
        }
    }

    public override bool Upgrade()
    {
        if (!nextUpgradePath) nextUpgradePath = upgradePaths[0];
        if (MoneyManager.Instance.CurrentMoney < nextUpgradePath.Cost)
        {
            return false;
        }

        this.slowdownPercentage = nextUpgradePath.SlowPercentage;
        this.slowdownTime = nextUpgradePath.SlowDuration;
        if (rank < upgradePaths.Length)
            nextUpgradePath = upgradePaths[rank - 1];
        rank++;
        return true;
    }

    public class UpgradePath : Component
    {
        int cost;
        float slowPercentage;
        float slowDuration;

        public int Cost
        {
            get => cost;
        }

        public float SlowPercentage => slowPercentage;

        public float SlowDuration => slowDuration;
    }

    public IEnumerator GravityRoutine(Enemy e)
    {
        if (e.SlowdownStatusEffect == null)
        {
            Renderer renderer = e.GetComponent<Renderer>();
            if (renderer)
                renderer.material = MaterialDict.Instance.MaterialPrefabs[1];
            e.SlowdownStatusEffect = slowdownStatusEffect;
            yield return new WaitForSeconds(slowdownStatusEffect.slowdownTime);
            e.SlowdownStatusEffect = null;
            if (e && renderer)
            {
                renderer.material = MaterialDict.Instance.MaterialPrefabs[0];
                e.SlowdownStatusEffect = null;
            }
        }
    }
}