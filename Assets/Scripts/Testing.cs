using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform originPosition;
    [SerializeField] [Range(1,48)] private int width = 3;
    [SerializeField] [Range(1,27)] private int height = 3;
    [SerializeField] [Range(3,40)] private float cellSize = 10f;
    
    private GridMap<bool> _gridMap;
    
    private void Start()
    { 
        Pathfinding pathfinding = new Pathfinding(width, height, cellSize, originPosition.position);
        //_gridMap = new GridMap<bool>(width, height, cellSize, originPosition.position, (g, x, y) => false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //_gridMap.SetGridObject(GridMap<bool>.GetMousePosition(), true);
        }
        if (Input.GetMouseButtonDown(1))
        {
           // Debug.Log(_gridMap.GetGridObject(GridMap<bool>.GetMousePosition()).ToString());
        }
    }
}
