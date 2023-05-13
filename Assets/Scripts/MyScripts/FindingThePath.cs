using System.Collections.Generic;
using UnityEngine;

public class FindingThePath : MonoBehaviour
{
    [SerializeField]
    private OurMap _ourMap;
      
    private int[,] _newAllowedLabirint;
    private int[,] _originLabirint;
    private Vector3Int _startPlayerPosition;

    private Queue<Vector3Int> _points;

    private Vector3 _targetPoint;
    private void Start()
    {
        _originLabirint = _ourMap.AllowedIndexesInMapAreTheOnes;
        _newAllowedLabirint = new int[_originLabirint.GetLength(0) + 2, _originLabirint.GetLength(1) + 2];
        _startPlayerPosition = _ourMap.StartPlayerPosition;
        _points = new Queue<Vector3Int>();
        CreateLabirint();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(CanPlayerWalking());

        }

    }
    private void CreateLabirint()
    {
        for (int i = 0; i < _originLabirint.GetLength(0); i++)
        {
            for (int j = 0; j < _originLabirint.GetLength(1); j++)
            {
                if (_originLabirint[i, j] == 1)
                {
                    _newAllowedLabirint[i + 1, j + 1] = 1;
                }
            }
        }
        for (int i = 0; i < _originLabirint.GetLength(0); i++)
        {
            _originLabirint[i, 0] = -1;
            _originLabirint[0, i] = -1;
        }
        for (int i = _originLabirint.GetLength(1) - 1; i >= 0; i--)
        {
            _originLabirint[i, _originLabirint.GetLength(1) - 1] = -1;
            _originLabirint[_originLabirint.GetLength(1) - 1, i] = -1;
        }
    }

    private bool CanPlayerWalking()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int x = 0;
        int y = 0;
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, 1 << 3))
        {
            _points.Enqueue(_startPlayerPosition);// добавили в очередь стартовую точку

            x = Mathf.CeilToInt(hitInfo.point.x) + 4;
            y = Mathf.CeilToInt(hitInfo.point.z) + 4;

            var tagretPoint = new Vector3Int(x, 0, y);// конечная точка

            int step = 2;
            int start = 1;
            int end = 1;
            int count = 1;

            while (true)
            {
                for (int i = start; i <= end; i++)
                {
                    var currentPoint = _points.Dequeue();

                    var X = currentPoint.x;
                    var Y = currentPoint.z;

                    if (_newAllowedLabirint[X - 1, Y] == 1)
                    {
                        _newAllowedLabirint[X - 1, Y] = step;
                        _points.Enqueue(new Vector3Int(X - 1, 0, Y));
                        count++;
                    }
                    if (_newAllowedLabirint[X + 1, Y] == 1)
                    {
                        _newAllowedLabirint[X + 1, Y] = step;
                        _points.Enqueue(new Vector3Int(X + 1, 0, Y));
                        count++;
                    }
                    if (_newAllowedLabirint[X, Y + 1] == 1)
                    {
                        _newAllowedLabirint[X, Y + 1] = step;
                        _points.Enqueue(new Vector3Int(X, 0, Y + 1));
                        count++;
                    }
                    if (_newAllowedLabirint[X, Y - 1] == 1)
                    {
                        _newAllowedLabirint[X, Y - 1] = step;
                        _points.Enqueue(new Vector3Int(X, 0, Y - 1));
                        count++;
                    }

                    if ((X - 1 == x && Y == y) ||
                        (X + 1 == x && Y == y) ||
                        (X == x && Y + 1 == y) ||
                        (X == x && Y - 1 == y))
                    {
                        break;
                    }


                }
                if (_points.Count == 0)
                {
                    break;
                }
                start = end + 1;
                end = count;
                step++;
            }
        }
        if (_newAllowedLabirint[x, y] == 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Dijkstra(int start)
    {
        int n = _newAllowedLabirint.GetLength(0);

        int[] distances = new int[n];
        bool[] visited = new bool[n];
        int[] parents = new int[n];

        for (int i = 0; i < n; i++)
        {
            distances[i] = int.MaxValue;
            visited[i] = false;
            parents[i] = -1;
        }

        distances[start] = 0;

        for (int i = 0; i < n - 1; i++)
        {
            int minDist = int.MaxValue;
            int minIndex = -1;

            for (int j = 0; j < n; j++)
            {
                if (!visited[j] && distances[j] < minDist)
                {
                    minDist = distances[j];
                    minIndex = j;
                }
            }

            visited[minIndex] = true;

            for (int j = 0; j < n; j++)
            {
                int edge = _newAllowedLabirint[minIndex, j];

                if (edge > 0 && !visited[j])
                {
                    int newDist = distances[minIndex] + edge;

                    if (distances[j] > newDist)
                    {
                        distances[j] = newDist;
                        parents[j] = minIndex;
                    }
                }
            }
        }
    }
}
