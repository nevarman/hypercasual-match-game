using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace HyperCasualMatchGame
{
    public class TilesSelectedEventArgs : EventArgs
    {
        public TilesSelectedEventArgs(List<HexBlock> tiles)
        {
            this.SelectedTiles = tiles;
        }
        public List<HexBlock> SelectedTiles { get; private set; }
    }

    public class PlayerInput : MonoBehaviour
    {
        private Camera _mainCamera;
        private bool _checkInput = false;
        private List<HexBlock> _selectedTiles = new List<HexBlock>();
        private Transform _lastTile;

        public event EventHandler<TilesSelectedEventArgs> TilesSelected;

        void Awake()
        {
            _mainCamera = Camera.main;
        }

        void Update()
        {
            CheckPlayerInput();
            if (Input.GetMouseButtonDown(0))
            {
                _lastTile = null;
                _selectedTiles.Clear();
                _checkInput = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _checkInput = false;
                Submit();
            }
        }

        private void Submit()
        {
            if (_selectedTiles.Count < 3)
            {
                ResetTiles();
                return;
            }
            if (TilesSelected != null)
                TilesSelected(this, new TilesSelectedEventArgs(_selectedTiles));
        }

        private void ResetTiles()
        {
            foreach (var item in _selectedTiles)
            {
                item.transform.localScale = Vector3.one;
            }
        }

        private void CheckPlayerInput()
        {
            if (!_checkInput)
                return;
            RaycastHit2D hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1f, 1);

            if (hit.collider != null)
            {
                var tile = hit.collider.transform;
                if (tile == _lastTile)
                {
                    return;
                }
                var block = tile.GetComponent<HexBlock>();
                if (!_selectedTiles.Any(e => e.Equals(block)))
                {
                    _selectedTiles.Add(block);
                    tile.localScale *= 1.15f;
                }
                else
                {
                    // Deselecting
                    _selectedTiles.RemoveAt(_selectedTiles.Count -1);
                    _lastTile.localScale = Vector3.one;
                }
                // Set last tile
                _lastTile = tile;
            }
        }
    }
}