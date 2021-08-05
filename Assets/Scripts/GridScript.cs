using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    public static int width = 10;
    public static int height = 27;
    public static int maxHeight = 20;
    public static Transform[,] grid = new Transform[height, width];

    public static void DeleteRows()
    {
        for (int i=0 ; i<height ; i++) {
            bool isRowComplete = true;
            for (int j=0 ; j<width ; j++) {
                if (grid[i,j] == null) {
                    isRowComplete = false;
                }
            }

            if (isRowComplete) {
                for (int j=0 ; j<width ; j++) {
                    Destroy(grid[i,j].gameObject);
                    grid[i,j] = null;
                }
            }
        }
    }
}
