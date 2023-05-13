using UnityEngine;
using UnityEngine.UI;

public class DeactivationButtons : MonoBehaviour
{
    [SerializeField]
    private Button[] _buttons;

    public void Deactivation()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].gameObject.SetActive(false);
        }
    }
}
