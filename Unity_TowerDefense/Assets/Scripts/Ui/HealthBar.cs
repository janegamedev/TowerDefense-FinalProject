using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Camera _camera;
    private Canvas _characterCanvas;
    private Enemy _enemy;
    [SerializeField] private Image healthBar;
    
    private void Start()
    {
        _camera = Camera.main;
        _enemy = GetComponentInParent<Enemy>();
        _characterCanvas = GetComponent<Canvas>();
        _characterCanvas.worldCamera = _camera;
    }

    public void Update()
    {
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
        healthBar.fillAmount = _enemy.Health / _enemy.MAxHealth;
    }
}

