using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public Transform StartPosition;
    public GridController Grid;

    private void Awake()
    {
        Grid = GetComponent<GridController>();
    }
}
