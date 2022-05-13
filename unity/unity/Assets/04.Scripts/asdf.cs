using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asdf : MonoBehaviour
{
    // ������Ʈ ĳ��ó���� ���� ����
    private CharacterController controller;
    private new Transform transform;
    private Animator animator;
    private new Camera camera;

    // ������ Plane�� ����ĳ�����ϱ����� ����
    private Plane plane;
    private Ray ray;
    private Vector3 hitPoint;

    // �̵��ӵ�
    public float moveSpeed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); // ������Ʈ�Ҵ�
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        camera = Camera.main;   // ī�޶�������

        // ������ �ٴ��� �������� ���ΰ��� ��ġ�� ����
        plane = new Plane(transform.up, transform.position);    // 

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Turn();
    }

    float h => Input.GetAxis("Horizontal");
    float v => Input.GetAxis("Vertical");

    void Move()
    {
        // x�� �̵�
        Vector3 cameraForward = camera.transform.forward;
        // y�� �̵�
        Vector3 cameraRight = camera.transform.right;
        // x,y�� �ʱ�ȭ
        cameraForward.y = 0.0f;
        cameraRight.y = 0.0f;

        // �̵��� ���⺤�� ���
        Vector3 moveDir = (cameraForward * v) + (cameraRight * h);
        // x, y, z
        moveDir.Set(moveDir.x, 0.0f, moveDir.z);

        // ĳ���� �̵� ó�� (���� * �ӵ�)
        controller.SimpleMove(moveDir * moveSpeed);
        // ĳ���� �ִϸ��̼� ó��
        float forward = Vector3.Dot(moveDir, transform.forward);
        float strafe = Vector3.Dot(moveDir, transform.right);
        animator.SetFloat("Forward", forward);
        animator.SetFloat("Strafe", strafe);

    }
    void Turn()
    {
        // ���콺�� 2���� ��ǩ���� �̿��� 3���� ���̺��� ����
        ray = camera.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;

        // ������ �ٴڿ� Ray�� �߻��� �浹 ������ �Ÿ��� enter������ ��ȯ
        plane.Raycast(ray, out enter);
        // ������ �ٴڿ� Ray�� �浹�� ��ǩ���� ����
        hitPoint = ray.GetPoint(enter);

        // ȸ���ؾ� �� ������ ���͸� ���
        Vector3 lookDir = hitPoint - transform.position;
        lookDir.y = 0;
        // ĳ������ ȸ���� ����
        transform.localRotation = Quaternion.LookRotation(lookDir);

    }
}