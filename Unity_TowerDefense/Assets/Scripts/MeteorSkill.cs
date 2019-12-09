using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeteorSkill : MonoBehaviour
{
    public int damage;
    public int range;

    public Button skillButton;
    public GameObject meteor;
    public float resetTime;
    private Vector3 _destination;
    private bool _isSelected;
    private bool _canFire;
    private Camera _camera;
    
    void Start()
    {
        _camera = Camera.main;
        skillButton.onClick.AddListener(Select);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isSelected)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Road")))
            {
                _destination = hit.transform.position;
                _canFire = true;
            }
            else
            {
                _canFire = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }

            if (Input.GetMouseButtonDown(1))
            {
                _isSelected = false;
                skillButton.interactable = true;
            }
        }
    }

    private void Select()
    {
        Debug.Log("Skill selected");
        _isSelected = true;
        skillButton.interactable = false;
    }

    private void Fire()
    {
        if (_canFire)
        {
            skillButton.interactable = false;
            
            Debug.Log("Fire!");
            _isSelected = false;
            _canFire = false;
            StartCoroutine(Restart());

            Meteor m = Instantiate(meteor, _destination + Vector3.up * 150, Quaternion.identity).GetComponent<Meteor>();
            m.damage = damage;
            m.range = range;
            
            _destination = Vector3.zero;
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(resetTime);
        skillButton.interactable = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_destination,range);
    }
}
