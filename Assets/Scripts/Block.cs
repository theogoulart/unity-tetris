using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Vector2 gridPos;

    private void Start() {
        gridPos = new Vector2((int)transform.position.x, (int)transform.position.y);
    }

    public bool IsRotationAllowed(Block pivot, bool isClockWiseRotation)
    {
        float distX = transform.position.x - pivot.transform.position.x;
        float distY = transform.position.y - pivot.transform.position.y;
        int xMtplr = isClockWiseRotation ? -1 : 1;
        int yMtplr = isClockWiseRotation ? 1 : -1;

        Vector2 nextPos = new Vector2(
            (float)Math.Floor(yMtplr * distY + pivot.transform.position.x),
            (float)Math.Floor(xMtplr * distX + pivot.transform.position.y)
        );

        return IsNextPosValid(nextPos);
    }

    public bool IsNextPosValid(Vector2 nextPos)
    {
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
