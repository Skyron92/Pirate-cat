using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CatSquare : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Inventory _inventory;

    [SerializeField] [Range(0,200)] private float validRange;

    [HideInInspector] public Cat cat; 

    private bool IsOnAValidPosition =>
        _inventory.teamPlacement.Exists(x => Vector3.Distance(x.position, transform.position) < validRange);

    private Image _image;

    private void Awake() {
        _image = GetComponent<Image>();
        _inventory = GetComponentInParent<Inventory>();
        canvas = GetComponentInParent<Canvas>();
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, validRange);
    }

    public void DragHandler(BaseEventData data) {
        transform.SetParent(_inventory.transform);
        HideInformationBox();
        PointerEventData pointerEventData = (PointerEventData) data;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform) canvas.transform,
            pointerEventData.position,
            canvas.worldCamera,
            out position);
        transform.position = canvas.transform.TransformPoint(position);

        _image.color = IsOnAValidPosition ? Color.green : Color.red;
    }

    public void Release(BaseEventData data) {
        if (!IsOnAValidPosition) {
            transform.SetParent(_inventory.viewportTransform);
            if (_inventory.currentTeam.Contains(this)) _inventory.currentTeam.Remove(this);
        }
        else {
            foreach (var trans in _inventory.teamPlacement) {
                if (Vector3.Distance(trans.position, transform.position) < validRange) {
                    transform.position = trans.position;
                    _inventory.currentTeam.Add(this);
                }
            }
        }
    }

    public void HideInformationBox(BaseEventData data) {
        TooltipManager.Instance.HideToolTip();
    }
    
    public void HideInformationBox() {
        TooltipManager.Instance.HideToolTip();
    }

    public void ShowInformationBox(BaseEventData data) {
        TooltipManager.Instance.ShowToolTip(cat);
    }
}