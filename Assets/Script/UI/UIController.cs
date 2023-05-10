using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class UIController : MonoBehaviour
{
    public enum Grid { Horizontal, Vertical, Both }

    public Grid gridType;

    [SerializeField] private InputActionReference selectReference;
    private InputAction SelectAction => selectReference.action;
    [SerializeField] private InputActionReference horizontalSelectReference;
    private InputAction HorizontalSelectAction => horizontalSelectReference.action;
    [SerializeField] private InputActionReference verticalSelectReference;
    private InputAction VerticalSelectAction => verticalSelectReference.action;

    private List<Button> _grid = new List<Button>();

    private int _index = 0;
    private int _columnCount = 0;
    private Button _currentSelected;

    private void Awake() {
        _grid.Clear();
        foreach (var button in GetComponentsInChildren<Button>()) {
            button.image.color = Color.white;
            _grid.Add(button);
        }

        switch (gridType) {
            case Grid.Horizontal : HorizontalSelectAction.Enable(); break;
            case Grid.Vertical :   VerticalSelectAction.Enable(); break;
            case Grid.Both :       
                SelectAction.Enable();
                GridLayoutGroup group = GetComponent<GridLayoutGroup>();
                _columnCount = group.constraint == GridLayoutGroup.Constraint.FixedColumnCount ? group.constraintCount :
                    group.constraint == GridLayoutGroup.Constraint.FixedRowCount ? _grid.Count / group.constraintCount :
                    1;  
                break;
            default:               Debug.LogError("Grid Type isn't assigned"); break;
        }
    }

    public void SelectUI(InputAction.CallbackContext context) {
        if(_currentSelected != null) _currentSelected.image.color = Color.white;
        //_index += context.ReadValue<>();
        if (_index >= _grid.Count) _index = 0;
        _currentSelected = _grid[_index];
        _currentSelected.image.color = Color.green;
    }

}
