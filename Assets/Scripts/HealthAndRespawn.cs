using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This gives an object health, the ability to die, and the ability to respawn
/// at an arbitrary spawn position.
/// </summary>
public class HealthAndRespawn : MonoBehaviour {
    public HUDController UIRef;
    /// <summary>
    /// The world position this object should respawn at when it dies.
    /// </summary>
    public Vector3 spawnPosition { get; private set; }
    /// <summary>
    /// The current health of this object. 0 is dead.
    /// </summary>
    public float hpCurrent = 100;
    public Vector3 hpPercent;
    /// <summary>
    /// The maximum health of this object.
    /// </summary>
    public float hpMax = 100;
    /// <summary>
    /// Whether or not the player should respawn when they die.
    /// This overrides destroyOnDeath.
    /// </summary>
    public bool respawnOnDeath = false;
    /// <summary>
    /// Whether or not to destroy this game object when it dies.
    /// This is ignored id respawnOnDeath is true.
    /// </summary>
    public bool destroyOnDeath = false;
    /// <summary>
    /// An event to call when this object dies.
    /// </summary>
    public UnityEvent onDeath;
    /// <summary>
    /// Sets the initial spawn position.
    /// </summary>
    void Start () {
        SetSpawn(transform.position);
        UIRef.playerHP = this;
	}
    /// <summary>
    /// Sets the spawn position.
    /// </summary>
    /// <param name="pos"></param>
    public void SetSpawn(Vector3 pos)
    {
        spawnPosition = pos;
    }
    /// <summary>
    /// Respawns this object at its spawn position. This resets the health to maximum.
    /// </summary>
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
    /// <summary>
    /// Call this method to do damage to this object. If health falls below 0, then trigger death.
    /// </summary>
    /// <param name="amount">How much health to take away.</param>
    public void TakeDamage(float amount)
    {
        hpCurrent -= amount;
        if (hpCurrent <= 0) Die();
    }
    /// <summary>
    /// Kills this object. Depending on settings, the object will
    /// respawn, be destroyed, or fire the onDeath event.
    /// </summary>
    public void Die()
    {
        onDeath.Invoke();
        if (respawnOnDeath) Respawn();
        else if (destroyOnDeath) Destroy(gameObject);
    }
}
