using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Camera _camera;
    public Image healthBar;

    private Enemy _enemy;
    private void Start()
    {
        _camera = Camera.main;
        _enemy = GetComponentInParent<Enemy>();
    }

    private void Update()
    {
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
        healthBar.fillAmount = _enemy.health / _enemy.maxHealth;
    }
}

