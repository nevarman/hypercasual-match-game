using System;
using System.Collections;
using System.Linq;
using HyperCasualMatchGame.Audio;
using UnityEngine;
namespace HyperCasualMatchGame
{
    /// <summary>
    /// Controls behaviour of the hex board
    /// Creates, updates, moves the hex board tiles
    /// </summary>
    public class BoardController : MonoBehaviour
    {
        private HexBoardSettings _settings;
        HexCellGenerator _hexCellGenerator;
        public event Action<int> ScoreChanged;
        public void Init(HexBoardSettings settings)
        {
            _settings = settings;
            _hexCellGenerator = new HexCellGenerator(_settings);
        }

        /// <summary>
        /// Event listener from Player Input
        /// Initialized from Bootstrap
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        public void CheckSelectedTiles(object source, TilesSelectedEventArgs args)
        {
            var tiles = args.SelectedTiles;
            // Check equals, get a tile thats not special to check
            var equals = !(tiles.Any(o => o != tiles.FirstOrDefault(t => !t.isSpecial)));
            if (!equals)
            {
                // Reset States
                tiles.ForEach(t => t.transform.localScale = Vector3.one);
                AudioController.Instance.PlayFailSound();
            }
            else
            {
                // Remove blocks from hex
                // TODO object pool
                var score = 0;
                tiles.ForEach(t =>
                {
                    // calculate score
                    if (t.isSpecial)
                    {
                        _hexCellGenerator.NumOfSpecialHex -= 1;
                        score += _settings.scoreAdditionSpecial;
                        Instantiate(_settings.specialHexEffect, t.transform.position, Quaternion.identity);
                        AudioController.Instance.PlaySpecialSound();
                    }
                    else
                    {
                        score += _settings.scoreAddition;
                    }
                    Destroy(t.gameObject);
                });
                // Invoke score event
                AudioController.Instance.PlaySuccesSound();
                ScoreChanged?.Invoke(score);
                StartCoroutine(DecreaseRows());
            }
        }
        private IEnumerator DecreaseRows()
        {
            yield return new WaitForEndOfFrame();
            int emptyCount = 0;
            for (int width_index = 0; width_index < _settings.width; width_index++)
            {
                for (int height_index = 0; height_index < _settings.height; height_index++)
                {
                    var hexCell = _hexCellGenerator.HexBoard[width_index, height_index];
                    if (hexCell.IsEmpty())
                    {
                        emptyCount++;
                    }
                    else if (emptyCount > 0)
                    {
                        DecreaseRow(hexCell, emptyCount);
                    }
                }
                emptyCount = 0;
            }
            yield return new WaitForSeconds(_settings.waitForRefill);
            Refill();
        }

        private void DecreaseRow(HexCell cell, int decrease)
        {
            var newCellY = cell.y - decrease;
            var hexBlock = cell.GetHexBlock();
            var newCell = _hexCellGenerator.HexBoard[cell.x, newCellY];
            hexBlock.transform.SetParent(newCell.transform, true);
            hexBlock.x = cell.x;
            hexBlock.y = newCellY;
            StartCoroutine(MoveHexBlockToZero(hexBlock));
        }

        private void Refill()
        {
            for (int width_index = 0; width_index < _settings.width; width_index++)
            {
                for (int height_index = 0; height_index < _settings.height; height_index++)
                {
                    var hexCell = _hexCellGenerator.HexBoard[width_index, height_index];
                    if (hexCell.IsEmpty())
                    {
                        var hexBlock = _hexCellGenerator.InstantiateRandomHexBlock(hexCell);
                        hexBlock.transform.localPosition += new Vector3(0f, _settings.outerRadius, 0f);
                        StartCoroutine(MoveHexBlockToZero(hexBlock));
                    }
                }
            }
            AudioController.Instance.PlayHexSound();
        }
        /// <summary>
        /// Moves the hex blocks down until they have locally zero position
        /// </summary>
        /// <param name="hexBlock"></param>
        /// <returns></returns>
        private IEnumerator MoveHexBlockToZero(HexBlock hexBlock)
        {
            yield return new WaitForEndOfFrame();
            var diff = hexBlock.transform.localPosition;
            while (hexBlock.transform.localPosition.magnitude > 0.1f)
            {
                hexBlock.transform.localPosition -= diff * Time.deltaTime * _settings.dropSpeed;
                yield return null;
            }
            hexBlock.transform.localPosition = Vector3.zero;
        }
    }
}