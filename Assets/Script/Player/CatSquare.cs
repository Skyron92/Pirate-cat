using UnityEngine;
using UnityEngine.EventSystems;

public class CatSquare : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    public void DragHandler(BaseEventData data) {
        
        PointerEventData pointerEventData = (PointerEventData) data;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform) canvas.transform,
            pointerEventData.position,
            canvas.worldCamera,
            out position);
        transform.position = canvas.transform.TransformPoint(position);
    }
}
