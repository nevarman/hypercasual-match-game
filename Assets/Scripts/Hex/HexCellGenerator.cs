using UnityEngine;
namespace HyperCasualMatchGame
{
    /// <summary>
    /// Helper class to create an hex board with hex block elements
    /// </summary>
    public class HexCellGenerator
    {
        HexBoardSettings _hexBoardSettings;
        Transform _hexBoardRoot;
        HexCell[,] _hexBoard;
        private int _numOfSpecialHex;

        public HexCell[,] HexBoard { get => _hexBoard; set => _hexBoard = value; }
        public int NumOfSpecialHex { get => _numOfSpecialHex; set => _numOfSpecialHex = value; }

        public HexCellGenerator(HexBoardSettings hexBoardSettings)
        {
            _hexBoardSettings = hexBoardSettings;
            CreateHexCells();
        }

        private void CreateHexCells()
        {
            _hexBoardRoot = new GameObject("HexBoard").transform;
            _hexBoard = new HexCell[_hexBoardSettings.width, _hexBoardSettings.height];
            // Create cells
            for (int width_index = 0, i = 0; width_index < _hexBoardSettings.width; width_index++)
            {
                for (int height_index = 0; height_index < _hexBoardSettings.height; height_index++)
                {
                    CreateHexCell(height_index, width_index, i++);
                }
            }
            // Center board transform
            _hexBoardRoot.transform.position -= new Vector3(_hexBoardSettings.width * _hexBoardSettings.innerRadius / 2f, _hexBoardSettings.height *
                _hexBoardSettings.outerRadius / 2f, 0f);
        }

        private void CreateHexCell(int height_index, int width_index, int i)
        {
            // Background hexagons
            Vector3 position;
            position.x = width_index * (_hexBoardSettings.innerRadius);
            position.y = (height_index + width_index * 0.5f - width_index / 2) * (_hexBoardSettings.outerRadius);
            position.z = 0f;
            // instantiate and set positions
            var cell = GameObject.Instantiate<Transform>(_hexBoardSettings.hexCellPrefab);
            cell.transform.SetParent(_hexBoardRoot.transform, false);
            cell.transform.localPosition = position;
            // set hex tiles
            HexCell hexCell = new HexCell() { transform = cell.transform, x = width_index, y = height_index };
            _hexBoard[width_index, height_index] = hexCell;
            InstantiateRandomHexBlock(hexCell);
        }

        internal HexBlock InstantiateRandomHexBlock(HexCell hex)
        {
            var random = UnityEngine.Random.Range(0, _hexBoardSettings.hexBlockPrefabs.Length);
            if(_hexBoardSettings.hexBlockPrefabs[random].isSpecial)
            {
                if (_numOfSpecialHex >= 3) //we do not  want too many specials 
                {
                    random -= UnityEngine.Random.Range(1, _hexBoardSettings.hexBlockPrefabs.Length - 1);
                }
                else 
                    _numOfSpecialHex ++;
            }
            // Instantiate block
            var blockAsset = GameObject.Instantiate<HexBlock>(_hexBoardSettings.hexBlockPrefabs[random]);
            blockAsset.transform.SetParent(hex.transform, true);
            // Set values
            blockAsset.transform.position = hex.transform.position;
            blockAsset.x = hex.x;
            blockAsset.y = hex.y;
            return blockAsset;
        }
    }
}