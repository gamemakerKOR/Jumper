using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // 스피드 조정 변수
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    private float applySpeed;

    [SerializeField]
    private float jumpForce;


    // 상태 변수
    private bool isRun = false;
    private bool isGround = true;


    // 앉았을 때 얼마나 앉을지 결정하는 변수.
    [SerializeField]

    private float originPosY;


    // 땅 착지 여부
    private BoxCollider boxCollider;

    [SerializeField]
    private Rigidbody myRigid;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;

    }

    void Update()
    {

        IsGround();
        TryJump();
        TryRun();
        Move();
        //CharacterRotation();

    }

    // 지면 체크.
    private void IsGround()
    {
        isGround = Physics.Raycast(boxCollider.bounds.center, Vector3.down, boxCollider.bounds.extents.y + 0.2f);
    }


    // 점프 시도
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }


    // 점프
    private void Jump()
    {
        myRigid.linearVelocity = transform.up * jumpForce;
    }


    // 달리기 시도
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }

    // 달리기 실행
    private void Running()
    {
        isRun = true;
        applySpeed = runSpeed;
    }


    // 달리기 취소
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }


    // 움직임 실행
    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x, 0f, z).normalized * applySpeed;
        myRigid.MovePosition(myRigid.position + move * Time.deltaTime);
    }

    // 좌우 캐릭터 회전
    /*private void CharacterRotation()
    {
        float Rotation = Input.GetAxisRaw("Horizontal");
        Vector3 _characterRotationY = new Vector3(0f, Rotation, 0f);
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }*/
}
