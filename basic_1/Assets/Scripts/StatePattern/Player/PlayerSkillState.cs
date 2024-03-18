using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : MonoBehaviour, PlayerState
{
    private PlayerController _playerController;
    
    public void RockOnOrDeFaultEvent()
    {
        // ������ ��� �ش� �ش� ����� �ٶ󺸰� ó��
        if (_playerController.LockTarget != null)
        {
            Vector3 dir = _playerController.LockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }

        SkillAnimationState(GetComponent<Animator>());
    }

    // �ִϸ��̼ǿ� �߰��� �̺�Ʈ �ߵ�
    void OnHitEvent()
    {        
        if(_playerController.LockTarget != null)
        {
            // Q) ���� ������ ���������� ���� ���� ������ hp�� �����ͼ� �ٿ��ٰ���? �ƴϸ� �����ʿ��� hp�� ������ ��Բ� ó���Ұ�����? 
            // ��� : ���ظ� �޴� �����ʿ��� ������ hp�� ��°� ���� ���� �ִٰ� ������ 
            // �ֳ��ϸ� ������ ����, ���ݷ� ���� ... ������ ���� hp�� ���̴� ������ �پ��� �ֱ� ������ �����ʿ��� �ڵ带 �����ϴ°� ����
            Stat targetStat = _playerController.LockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_playerController.Stat);            
        }

        if (_playerController.StopSkill)
        {            
            _playerController.Wait();
        }
        else
        {
            _playerController.PlayerState = Defines.State.Skill;
        }
    }

    public void SkillAnimationState(Animator anim)
    {
        _playerController.PlayerState = Defines.State.Skill;
        anim.Play("ATTACK");
    }

    public void Handle(PlayerController playerController)
    {
        if (playerController == null)
            _playerController = gameObject.GetOrAddComponent<PlayerController>();
        else
            _playerController = playerController;

        RockOnOrDeFaultEvent();
    }
}