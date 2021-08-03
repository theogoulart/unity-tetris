using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private Block[] _blocks;
    private float _timer;
    private bool _isDisabled;
    public Block pivot;
    public float fallTime;

    // Start is called before the first frame update
    void Start()
    {
        _blocks = new Block[transform.childCount];
        for (int i=0; i<_blocks.Length ; i++)
        {
            _blocks[i] = transform.GetChild(i).GetComponent<Block>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fall();
        OnInput();
    }

    void OnInput()
    {
        if (_isDisabled) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (IsRotationValid()) {
                transform.RotateAround(transform.position, Vector3.forward, 90f);
                UpdatePieceInGrid();
            }
        }

        Vector3 direction = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.A)) {
            direction = Vector3.left;
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            direction = Vector3.right;
        }

        Move(direction);
    }

    void Fall()
    {
        _timer += Time.deltaTime;

        if (_timer >= (Input.GetKeyDown(KeyCode.S) ? fallTime/10 : fallTime)) {
            bool isMoveValid = IsMoveValid(Vector3.down);
            if (!isMoveValid) {
                if (!_isDisabled) {
                    _isDisabled = true;
                    Spawner.instance.Spawn();
                }
                return;
            }
            Move(Vector3.down);
            _timer = 0;
        }
    }

    bool IsMoveValid(Vector3 direction)
    {
        foreach (Block block in _blocks)
        {
            if (!block.IsNextPosValid(direction)) {
                return false;
            }
        }

        return true;
    }

    bool IsRotationValid()
    {
        foreach (Block block in _blocks)
        {
            if (!block.IsRotationAllowed(pivot)) {
                return false;
            }
        }

        return true;
    }

    void Move(Vector3 direction) {
        transform.position += direction;
        UpdatePieceInGrid();
    }

    void UpdatePieceInGrid()
    {
        foreach (Block block in _blocks)
        {
            block.RemoveGridPosition();
        }

        foreach (Block block in _blocks)
        {
            block.UpdateGridPosition();
        }
    }
}
