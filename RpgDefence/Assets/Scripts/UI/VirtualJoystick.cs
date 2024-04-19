using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(10, 150)]
    private float leverRange;

    private Vector3 inputDirection;
    public Vector3 InputDirection { get { return inputDirection; } }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Managers.Game.GetPlayer().GetComponent<PlayerController>().PlayerState = Defines.State.Moving;
        ControlJoystickLever(eventData);        
    }

    // Ŭ���ؼ� �巡�� �ϴ� �߿� �߻��ϴ� �̺�Ʈ    
    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Managers.Game.GetPlayer().GetComponent<PlayerController>().PlayerState = Defines.State.Wait;
        lever.anchoredPosition = Vector2.zero; // ���̽�ƽ�� �߽ɺη� ������ ����ġ        
    }

    void ControlJoystickLever(PointerEventData eventData)
    {
        Vector2 vector = new Vector2(0.2f, 0.2f);
        Vector2 noVetor = vector.normalized * 10;
        Vector2 noVetor2 = noVetor / 10;
        Debug.Log(noVetor);
        Debug.Log(noVetor2);

        Vector2 inputPos = eventData.position - rectTransform.anchoredPosition;        
        Vector2 inputVector = inputPos.magnitude <= leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector.normalized;
    }

}