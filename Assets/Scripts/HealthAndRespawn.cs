using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthAndRespawn : MonoBehaviour {

    public Vector3 spawnPosition { get; private set; }

    public float hpCurrent { get; private set; }
    public float hpMax = 100;
    public bool respawnOnDeath = false;
    public bool destroyOnDeath = false;

    public UnityEvent onDeath;

    void Start () {
        SetSpawn(transform.position);
	}
    public void SetSpawn(Vector3 pos)
    {
        spawnPosition = pos;
    }
	void Respawn()
    {
        hpCurrent = hpMax;
        transform.position = spawnPosition;
        LayerFixed zControl = GetComponent<LayerFixed>();
        if (zControl)
        {
            zControl.z = (int)(spawnPosition.z / LayerFixed.separation);
        }
    }
    public void TakeDamage(float amount)
    {
        hpCurrent -= amount;
        if (hpCurrent <= 0) Die();
    }
    public void Die()
    {
        onDeath.Invoke();
        if (respawnOnDeath) Respawn();
        else if (destroyOnDeath) Destroy(gameObject);
    }
}
