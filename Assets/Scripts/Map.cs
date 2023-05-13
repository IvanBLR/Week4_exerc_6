using UnityEngine;

public class Map : MonoBehaviour
{
    public Vector2Int Size => _size;
    public Tile[,] MapWithTiles => _tiles;

    [SerializeField]
    private Vector2Int _size;

    private Tile[,] _tiles;

    private OurMap _ourMap;
    private int[,] _allowedTilesIndexes;
    private void Awake()
    {
        _tiles = new Tile[Size.x, Size.y];
        _ourMap = transform.GetComponent<OurMap>();
    }

    public bool IsCellAvailable(Vector2Int index)
    {
        // Если индекс за пределами сетки - возвращаем false
        var isOutOfGrid = index.x < 0 || index.y < 0 ||
                          index.x >= _tiles.GetLength(0) || index.y >= _tiles.GetLength(1);
        if (isOutOfGrid)
        {
            return false;
        }

        // Возвращаем значение, свободна ли клетка в пределах сетки
        var isFree = _tiles[index.x, index.y] == null;
        return isFree;
    }

    public void SetTile(Vector2Int index, Tile tile)
    {
        _tiles[index.x, index.y] = tile;
    }

    public void SetLayerForAllowedTiles()
    {
        _allowedTilesIndexes = _ourMap.AllowedIndexesInMapAreTheOnes;

        for (int i = 0; i < _allowedTilesIndexes.GetLength(0); i++)
        {
            for (int j = 0; j < _allowedTilesIndexes.GetLength(1); j++)
            {
                if (_allowedTilesIndexes[i, j] == 1)
                {
                    _tiles[i, j].transform.GetChild(1).gameObject.layer = 3;
                }
            }
        }
    }
    public void ResetColor()
    {

        _allowedTilesIndexes = _ourMap.AllowedIndexesInMapAreTheOnes;

        for (int i = 0; i < _allowedTilesIndexes.GetLength(0); i++)
        {
            for (int j = 0; j < _allowedTilesIndexes.GetLength(1); j++)
            {
                if (_allowedTilesIndexes[i, j] == 1)
                {
                    _tiles[i, j].transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                }
            }
        }
    }
}