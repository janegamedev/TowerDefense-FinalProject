using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private Image healthBar;
    
    private void Start()
    {
        _camera = Camera.main;
    }

    public void UpdateHealth( float health, float maxHealth)
    {
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
        healthBar.fillAmount = health / maxHealth;
    }
}

