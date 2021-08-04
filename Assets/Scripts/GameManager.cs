using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float fallTime;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void CheckGame()
    {
        Time.timeScale = 0;
        Grid.DeleteRows();
        Time.timeScale = 1;
    }
}
