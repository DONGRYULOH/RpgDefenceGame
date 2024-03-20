using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    int _mask = (1 << (int)Defines.Layer.Ground) | (1 << (int)Defines.Layer.Monster1); 
    int _doorMask = (1 << (int)Defines.Layer.NextChapter) | (1 << (int)Defines.Layer.Store);

    // ����ȭ���� ���콺 Ŀ��    
    Texture2D _attackIcon, _handIcon, _nextChapterIcon, _storeIcon;

    // ���� é�ͷ� �̵��ϰų� �������� �̵��� Ŭ�������� Ŀ���� ����Ǹ� �ȵ�
    static public bool chapterOrStoreClick = false;

    public enum CursorType
    {
        None,
        Attack,
        Hand,
        NextChapter,
        Store
    }
    static public CursorType _cursorType = CursorType.None;
    
    void Start()
    {
        _attackIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        _handIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");
        _nextChapterIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/NextChapter");
        _storeIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Store");
    }
    
    void Update()
    {
        if (chapterOrStoreClick) return;

        // ���ʿ� Ŭ���� ������ �ش��ϴ� Ŀ���θ� ǥ���ϰ� ��� ���콺 ��ư�� �������� �ִ� ���¶�� �ٸ� Ŀ���� ���� X
        // ex) ���͸� Ŭ���ϰ� ��� ���� ���¿��� Ŀ���� �ٸ������� �̵������� �ٲ�°��� ����
        if (Input.GetMouseButton(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Defines.Layer.Monster1)
            {
                // �̹� Attack Ŀ���� ���(���� Ŀ���ÿ���) SetCursor �۾��� ���� �ʵ��� ����
                if (_cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
                    _cursorType = CursorType.Attack;
                }
            }
            else
            {
                if (_cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_handIcon, new Vector2(_handIcon.width / 3, 0), CursorMode.Auto);
                    _cursorType = CursorType.Hand;
                }
            }
        }

        if (Physics.Raycast(ray, out hit, 100.0f, _doorMask))
        {
            if (hit.collider.gameObject.layer == (int)Defines.Layer.NextChapter)
            {                
                Cursor.SetCursor(_nextChapterIcon, new Vector2(_nextChapterIcon.width / 5, 0), CursorMode.Auto);
                _cursorType = CursorType.NextChapter;                
            }
            else
            {             
                Cursor.SetCursor(_storeIcon, new Vector2(_storeIcon.width / 5, 0), CursorMode.Auto);
                _cursorType = CursorType.Store;    
            }
        }
    }
}