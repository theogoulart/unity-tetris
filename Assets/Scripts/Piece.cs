using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private Block[] _blocks;
    private float _timer;
    private float _localFalltime;
    private bool _isDisabled;
    public Block pivot;

    // Start is called before the first frame update
    void Start()
    {
        _localFalltime = GameManager.instance.fallTime;
        _blocks = new Block[transform.childCount];
        for (int i=0; i<_blocks.Length ; i++)
        {
            _blocks[i] = transform.GetChild(i).GetComponent<Block>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        OnInput();
        Fall();
    }

    void OnInput()
    {
        if (_isDisabled) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q)) {
            if (IsRotationValid(Input.GetKeyDown(KeyCode.E))) {
                transform.RotateAround(transform.position, Vector3.forward, 90f);
                UpdatePieceInGrid();
            }
        }

        Vector2 direction = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.A)) {
            direction = Vector2.left;
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            direction = Vector2.right;
        }

        bool isMoveValid = IsMoveValid(direction);
        if (isMoveValid) {
            Move(direction);
        }
    }

    void Fall()
    {
        _timer += Time.deltaTime;
        if (_timer >= (Input.GetKeyDown(KeyCode.S) ?
        _localFalltime/10 : _localFalltime)) {
            bool isMoveValid = IsMoveValid(Vector2.down);
            if (!isMoveValid) {
                if (!_isDisabled) {
                    _isDisabled = true;
                    _localFalltime = _localFalltime/10;
                    Spawner.instance.Spawn();
                }
                GameManager.instance.CheckGame();
                return;
            }
            Move(Vector2.down);
            _timer = 0;
        }
    }

    bool IsMoveValid(Vector2 direction)
    {
        foreach (Block block in _blocks)
        {
            if (block == null) {
                continue;
            }

            if (!block.IsNextPosValid(block.gridPos + direction)) {
                return false;
            }
        }

        return true;
    }

    bool IsRotationValid(bool isClockWiseRotation)
    {
        if (pivot == null) {
            return false;
        }

        foreach (Block block in _blocks)
        {
            if (block == null) {
                continue;
            }

            if (!block.IsRotationAllowed(pivot, isClockWiseRotation)) {
                return false;
            }
        }

        return true;
    }

    void Move(Vector2 direction) {
        transform.position += (Vector3) direction;
        UpdatePieceInGrid();
    }

    void UpdatePieceInGrid()
    {
        foreach (Block block in _blocks)
        {
            if (block == null) {
                continue;
            }

            block.RemoveGridPosition();
        }

        foreach (Block block in _blocks)
        {
            if (block == null) {
                continue;
            }

            block.UpdateGridPosition();
        }
    }
}
