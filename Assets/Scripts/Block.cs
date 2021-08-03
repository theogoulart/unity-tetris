using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Vector2 gridPos;

    private void Start() {
        gridPos = new Vector2((int)transform.position.x, (int)transform.position.y);
    }

    public bool IsRotationAllowed(Block pivot)
    {
        Debug.Log(pivot.name);
        return true;
    }

    public bool IsNextPosValid(Vector2 direction)
    {
        Vector2 nextPos = gridPos + direction;
        if ((int) nextPos.x < 0 || (int) nextPos.x >= Grid.width) {
            return false;
        }

        if ((int) nextPos.y < 0) {
            return false;
        }

        Transform cell = Grid.grid[(int)nextPos.y, (int)nextPos.x];
        if (cell != null && cell.parent != transform.parent) {
            return false;
        }

        return true;
    }

    public void RemoveGridPosition()
    {
        Debug.Log("---");
        Debug.Log((int)gridPos.y);
        Debug.Log((int)gridPos.x);
        Grid.grid[(int)gridPos.y, (int)gridPos.x] = null;
    }

    public void UpdateGridPosition()
    {
        int xPos = (int)transform.position.x;
        int yPos = (int)transform.position.y;

        gridPos = new Vector2(xPos, yPos);
        Grid.grid[yPos, xPos] = transform;
    }
}
