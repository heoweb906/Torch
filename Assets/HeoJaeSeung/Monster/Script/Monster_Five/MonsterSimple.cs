using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Monster-Simple, Monster-Horse ���� 
public class MonsterSimple : MonoBehaviourPunCallbacks
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
        if(!isStun)
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
        // ������ �������� ray�� ���ϴ�.
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.green);
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength))
        {
            // ray�� � ��ü�� �浹�ߴ��� Ȯ���մϴ�.
            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Monster"))
            {
                moveDirection *= -1f; 
                transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
            }

            // ray�� �浹�� ������ ������ ǥ���մϴ�.
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
        }
    }


}
