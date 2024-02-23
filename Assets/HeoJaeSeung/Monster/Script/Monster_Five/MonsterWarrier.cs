using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWarrier : MonoBehaviourPunCallbacks
{
    public int hp;
    public float moveSpeed;

    public float moveDirection; // �̵� ���� (1�̸� ������, -1�̸� ����)
    public float rayLength; // ray�� ����

    public bool isStun = false;

    private void Awake()
    {
        moveDirection = (Random.value < 0.5f) ? 1f : -1f;
        RotateMonster();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStun)
        {
            MoveForward();
        }

        SenseStage();
    }

    // ���͸� �̵� �������� �̵���ŵ�ϴ�.
    private void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    // ������ �̵� ���⿡ ���� ȸ���մϴ�.
    private void RotateMonster()
    {
        if (moveDirection > 0) // �̵� ������ �������� ���
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f); // ���������� ȸ��
        }
        else // �̵� ������ ������ ���
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f); // �������� ȸ��
        }
    }

    // ���� ������ �����մϴ�.
    private void SenseStage()
    {
        Vector3 rayStartPosition = transform.position + Vector3.up;

        // ������ �������� ray�� ���ϴ�.
        Debug.DrawRay(rayStartPosition, transform.forward * rayLength, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(rayStartPosition, transform.forward, out hit, rayLength))
        {
            // ray�� � ��ü�� �浹�ߴ��� Ȯ���մϴ�.
            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Monster"))
            {
                moveDirection *= -1f;
                transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
            }

            // ray�� �浹�� ������ ������ ǥ���մϴ�.
            Debug.DrawRay(rayStartPosition, transform.forward * hit.distance, Color.red);
        }
    }
}
