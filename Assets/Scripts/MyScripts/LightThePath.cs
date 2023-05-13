using System;
using UnityEngine;

public class LightThePath : MonoBehaviour
{
    [SerializeField]
    private Color _allowingColor;

    [SerializeField]
    private OurMap _ourMap;

    [SerializeField]
    private Map _map;

    private int _x;
    private int _y;

    private bool _flag;
    private void Update()                                                        // работает идеально, но мб не оптимально
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Single.MaxValue, 1 << 3))
        {
            _flag = true;
            var meshRenderersArray = hitInfo.transform.GetComponents<MeshRenderer>();
            foreach (var renderer in meshRenderersArray)
            {
                renderer.material.color = _allowingColor;
            }
            var currentX = Mathf.CeilToInt(hitInfo.point.x);
            var currentY = Mathf.CeilToInt(hitInfo.point.z);

            if (Mathf.Abs(_x - currentX) >= 1 || Mathf.Abs(_y - currentY) >= 1)
            {
                _x = currentX;
                _y = currentY;
                _map.ResetColor();
            }
        }
        else if (_flag)
        {
            _map.ResetColor();
            _flag = false;
           // Debug.Log(46);
        }
    }
}
