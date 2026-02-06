using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody myRigid;

    // 스피드 조정 변수
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    private float applySpeed;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float fallMultiplier;

    // 상태 변수
    private bool isRun = false;
    private bool isGround = true;

    // 땅 착지 여부
    private BoxCollider boxCollider;

    void Start()
    {
        anim = GetComponent<Animator>();
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

    }

    void FixedUpdate()
    {
        if (myRigid.linearVelocity.y < 0)
        {
            myRigid.AddForce(
                Physics.gravity * fallMultiplier,
                ForceMode.Acceleration
            );
        }
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
        anim.SetTrigger("Jump");
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
        if (move.sqrMagnitude > 0.01f)
        {
            anim.SetBool("Walk", true);
            anim.SetBool("Run", isRun);
            Quaternion targetRot = Quaternion.LookRotation(move);
            myRigid.MoveRotation(
                Quaternion.Slerp(myRigid.rotation, targetRot, rotateSpeed * Time.deltaTime)
            );
        }
        else
        {
            anim.SetBool("Walk",false);
            anim.SetBool("Run", false);
        }
    }
}
