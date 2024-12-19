
using System;
using UnityEngine;

[Serializable]

public abstract class EnemyProjectileSpawner : MonoBehaviour
{
    [SerializeField] private bool primary;
    public abstract string Name { get; }
    public bool Primary { get => primary; }
    public abstract void Fire(Transform target);
}
