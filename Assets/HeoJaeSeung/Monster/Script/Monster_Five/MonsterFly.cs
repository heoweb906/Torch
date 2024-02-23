using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterFly : MonoBehaviourPunCallbacks
{
    public int hp;
    public float moveSpeed;

    public float rayLength; // ray�� ����

    private bool bMoveRight;
    private bool bMoveDown;
    
    public bool isStun = false;

    private void Awake()
    {
        bMoveRight = Random.value < 0.5f;
        bMoveDown = true;
        transform.rotation = Quaternion.Euler(0f, 180f, 0f); // ���������� ȸ��

    }

    // Update is called once per frame
    void Update()
    {
        if (!isStun)
        {
            MoveMonsterFly();
        }

        SenseStage();
    }

    // ���͸� �̵� �������� �̵���ŵ�ϴ�.
    private void MoveMonsterFly()
    {
        if(bMoveRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        if(bMoveDown)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        } 
    }

    

    // ���� ������ �����մϴ�.
    private void SenseStage()
    {
        Vector3 rayStartPosition = transform.position + Vector3.up;

        // �� �������� ���̸� ���ϴ�.
        RaycastHit hit;

        Debug.DrawRay(rayStartPosition, Vector3.up * rayLength, Color.green);
        Debug.DrawRay(rayStartPosition, Vector3.down * rayLength, Color.green);
        Debug.DrawRay(rayStartPosition, Vector3.right * rayLength, Color.green);
        Debug.DrawRay(rayStartPosition, Vector3.left * rayLength, Color.green);


        // ���� ����
        if (Physics.Raycast(rayStartPosition, Vector3.up, out hit, rayLength))
        {
            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Floor") || hit.collider.CompareTag("Monster"))
            {
                bMoveDown = true;
            }

            Debug.DrawRay(rayStartPosition, Vector3.up * hit.distance, Color.red);
        }

        // �Ʒ��� ����
        if (Physics.Raycast(rayStartPosition, Vector3.down, out hit, rayLength))
        {
            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Floor") || hit.collider.CompareTag("Monster"))
            {
                bMoveDown = false;
            }

            Debug.DrawRay(rayStartPosition, Vector3.down * hit.distance, Color.red);
        }

        // ���� ����
        if (Physics.Raycast(rayStartPosition, Vector3.left, out hit, rayLength))
        {
            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Floor") || hit.collider.CompareTag("Monster"))
            {
                bMoveRight = false;
            }
                
            Debug.DrawRay(rayStartPosition, Vector3.left * hit.distance, Color.red);
        }

        // ������ ����
        if (Physics.Raycast(rayStartPosition, Vector3.right, out hit, rayLength))
        {
            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Floor") || hit.collider.CompareTag("Monster"))
            {
                bMoveRight = true;
            }

            Debug.DrawRay(rayStartPosition, Vector3.right * hit.distance, Color.red);
        }

    }
}
