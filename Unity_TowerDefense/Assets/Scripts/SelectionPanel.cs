using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionPanel : MonoBehaviour
{
    public Button[] buttons;
    public TextMeshProUGUI[] costs;
    public Transform cell;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if(cell!=null)
        transform.position = _camera.WorldToScreenPoint(cell.position);
    }
}
