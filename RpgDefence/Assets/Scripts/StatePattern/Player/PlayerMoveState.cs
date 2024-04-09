using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveState : MonoBehaviour, PlayerState
{
    private PlayerController _playerController;

    void UpdateMoving()
    {
        Vector3 dir = _playerController.DestPos - transform.position;
        dir.y = 0; // RayCasting 좌표가 y축이 있다면 위로 이동하므로 y축은 이동하지 못하도록 설정

        // 1.락온이 되어있는 오브젝트를 향해 이동 (플레이어 사정거리 안에 몬스터가 있으면 공격상태로 변경)
        if (_playerController.LockTarget != null)
        {
            float distance = dir.magnitude;
            // 플레이어의 사정거리안에 몬스터가 들어오면 공격처리
            if (distance <= _playerController.Stat.AttackRange)
            {
                Managers.Game.GetPlayer().GetComponent<PlayerController>().StopSkill = false;
                _playerController.Skill();
                return;
            }
        }
        else if (dir.magnitude < 0.1f)
        {
            // 2.단순한 이동으로 목적지까지 움직이는 경우
            _playerController.Wait();
            return;
        }
               
                             
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
        {
            if (Input.GetMouseButton(0) == false)
                _playerController.Wait();
            return;
        }

        float moveDist = Mathf.Clamp(_playerController.Stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
        transform.position += dir.normalized * moveDist;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        MovingAnimationState(GetComponent<Animator>());
        
    }

    public void MovingAnimationState(Animator anim)
    {        
        _playerController.PlayerState = Defines.State.Moving;
        // 애니메이션 툴과 싱크를 맞추는 작업을 하지않고 애니메이션 제어도 코드에서 관리하도록 설정 
        // 애니메이션이 많아질수록 코드에서 싱크를 맞춰주는 작업도 많아지므로 오히려 코드에서만 제어하도록 하는게 좋을 수도 있음
        anim.Play("RUN");        
    }

    // 애니메이션 이벤트 
    public void OnRunEvent() { }                

    public void Handle(PlayerController playerController)
    {
        if (playerController == null)
            _playerController = gameObject.GetOrAddComponent<PlayerController>();
        else
            _playerController = playerController;

        MovingAnimationState(GetComponent<Animator>());
        UpdateMoving();
    }
}
