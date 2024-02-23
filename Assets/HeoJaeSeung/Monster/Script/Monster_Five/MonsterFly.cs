using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterFly : MonoBehaviourPunCallbacks
{
    public int hp;
    public float moveSpeed;

    public float rayLength; // ray의 길이

    private bool bMoveRight;
    private bool bMoveDown;
    
    public bool isStun = false;

    private void Awake()
    {
        bMoveRight = Random.value < 0.5f;
        bMoveDown = true;
        transform.rotation = Quaternion.Euler(0f, 180f, 0f); // 오른쪽으로 회전

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

    // 몬스터를 이동 방향으로 이동시킵니다.
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

    

    // 지형 지물을 감지합니다.
    private void SenseStage()
    {
        Vector3 rayStartPosition = transform.position + Vector3.up;

        // 각 방향으로 레이를 쏩니다.
        RaycastHit hit;

        Debug.DrawRay(rayStartPosition, Vector3.up * rayLength, Color.green);
        Debug.DrawRay(rayStartPosition, Vector3.down * rayLength, Color.green);
        Debug.DrawRay(rayStartPosition, Vector3.right * rayLength, Color.green);
        Debug.DrawRay(rayStartPosition, Vector3.left * rayLength, Color.green);


        // 위쪽 방향
        if (Physics.Raycast(rayStartPosition, Vector3.up, out hit, rayLength))
        {
            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Floor") || hit.collider.CompareTag("Monster"))
            {
                bMoveDown = true;
            }

            Debug.DrawRay(rayStartPosition, Vector3.up * hit.distance, Color.red);
        }

        // 아래쪽 방향
        if (Physics.Raycast(rayStartPosition, Vector3.down, out hit, rayLength))
        {
            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Floor") || hit.collider.CompareTag("Monster"))
            {
                bMoveDown = false;
            }

            Debug.DrawRay(rayStartPosition, Vector3.down * hit.distance, Color.red);
        }

        // 왼쪽 방향
        if (Physics.Raycast(rayStartPosition, Vector3.left, out hit, rayLength))
        {
            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Floor") || hit.collider.CompareTag("Monster"))
            {
                bMoveRight = false;
            }
                
            Debug.DrawRay(rayStartPosition, Vector3.left * hit.distance, Color.red);
        }

        // 오른쪽 방향
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
