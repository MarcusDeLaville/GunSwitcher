﻿using System;
using _GAME.Common;
using RootMotion.FinalIK;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public Action Die;
    public UnityAction <Vector3> AddExplosionForce;

    [SerializeField] private Transform _hitTarget;
    [SerializeField] private Ragdoll _ragdoll;
    [SerializeField] private AimIK _aim;
    [SerializeField] private GameObject _laser;
    [SerializeField] private AutoShooting _autoShooting;
    [SerializeField] private Collider _weaponTrigger;
    [SerializeField] private bool _alive = true;
    [SerializeField] private GameObject _particleExplosionParts;

    public Transform HitTarget => _hitTarget;

    private void OnEnable()
    {
        _laser.SetActive(false);
        Die += Dead;
        AddExplosionForce += AddExplosion;
    }

    private void OnDisable()
    {
        Die -= Dead;
        AddExplosionForce -= AddExplosion;
    }

    private void Dead()
    {
        _weaponTrigger.enabled = false;
        _aim.enabled = false;
        _alive = false;
        _laser.SetActive(false);
        _autoShooting.enabled = false;
    }

    private void AddExplosion(Vector3 bulletTransform)
    {
        if (_particleExplosionParts != null)
        {
            var partsOfEnemy = Instantiate(_particleExplosionParts, bulletTransform, transform.rotation);
            partsOfEnemy.transform.rotation =
                new quaternion(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z, 0);
            Destroy(partsOfEnemy, 3f);
        }
         
        PlayerHealth playerPosition = FindObjectOfType<PlayerHealth>();
        Vector3 offset = new Vector3(0, -3, 0);
        Vector3 direction =  transform.position - (playerPosition.transform.position + offset);
        _ragdoll.ActivateRagdoll(direction);
    }

    public void TurnOnLaser()
    {
        if (_alive)
        {
            _laser.SetActive(true);
        }
    }
}