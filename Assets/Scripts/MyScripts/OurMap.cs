using UnityEngine;

public class OurMap : MonoBehaviour
{
    public int[,] AllowedIndexesInMapAreTheOnes => _normalTilesInMap;

    private int[,] _normalTilesInMap;

    public Vector3Int StartPlayerPosition => _startPlayerPosition;

    [SerializeField]
    private Transform _parent;

    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private Map _map;


    private Vector3Int _startPlayerPosition;
    public void GetAllowedPath()
    {
        var finalMap = _map.MapWithTiles;
        var x = finalMap.GetLength(0);
        var y = finalMap.GetLength(1);
        _normalTilesInMap = new int[x, y];
               
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                var tile = finalMap[i, j];
                if (tile != null)
                {
                    if (finalMap[i, j].gameObject.GetComponentsInChildren<BoxCollider>().Length <= 3)
                    {
                        _normalTilesInMap[i, j] = 1;
                    }
                }
            }
        }
        bool flag = false;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {                                                                  // по-хорошему, надо не конкретные числа писать,
                if (_normalTilesInMap[i, j] == 1)                              // а взять размер тайла и размер карты, и уже     
                {                                                              // исходя от этих данные вести расчеты
                    var chicken = Instantiate(_prefab);
                    chicken.transform.SetParent(_parent);
                    chicken.transform.position = new Vector3(i - 4.5f, 1, j - 4.5f);
                    var X = (int)chicken.transform.position.x;
                    var Y = (int)chicken.transform.position.z;
                    _startPlayerPosition = new Vector3Int(X, 0, Y);                   
                    flag = true;                                                           
                    break;                                                                
                }
            }
            if (flag) break;
        }
        _map.SetLayerForAllowedTiles();
    }
}
