using System.Collections;
using UnityEngine;

public class GravityTowerBullet : AOEBullet
{
    public SlowdownStatusEffect slowdownStatusEffect { get; set; }

    public void OnTriggerEnter(Collider other)
    {
        if (gameObject && other.gameObject == Target.gameObject)
        {
            // ... get all other towers and damange them as well.
            Collider[] aoeColliders = Physics.OverlapSphere(Target.transform.position, aoeRadius);
            for (var i = 0; i < aoeColliders.Length; i++)
            {
                var collider = aoeColliders[i];
                if (collider.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    StartCoroutine(GravityRoutine(enemy));
                }
            }

            Destroy(this.gameObject);
        }
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