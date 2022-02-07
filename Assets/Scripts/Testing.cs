using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField]
    private Transform originPosition;
    
    [SerializeField] [Range(1,48)]
    private int width = 3;
    
    [SerializeField] [Range(1,27)]
    private int height = 3;
    
    [SerializeField] [Range(3,40)]
    private float cellsize = 10f;
    
    private GridMap gridMap;
    
    private void Start()
    {
        gridMap = new GridMap(width, height, cellsize, originPosition.position);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gridMap.SetValue(GridMap.GetMousePosition(), 69);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(gridMap.GetValue(GridMap.GetMousePosition()).ToString());
        }
    }
}
