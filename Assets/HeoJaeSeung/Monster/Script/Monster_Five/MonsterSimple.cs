using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Monster-Simple, Monster-Horse 공유 
public class MonsterSimple : MonoBehaviourPunCallbacks
{
    public int hp;
    public float moveSpeed;

    public float moveDirection; // 이동 방향 (1이면 오른쪽, -1이면 왼쪽)
    public float rayLength; // ray의 길이

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

    // 몬스터를 이동 방향으로 이동시킵니다.
    private void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    // 몬스터의 이동 방향에 따라 회전합니다.
    private void RotateMonster()
    {
        if (moveDirection > 0) // 이동 방향이 오른쪽인 경우
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f); // 오른쪽으로 회전
        }
        else // 이동 방향이 왼쪽인 경우
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f); // 왼쪽으로 회전
        }
    }

    // 지형 지물을 감지합니다.
    private void SenseStage()
    {
        // 몬스터의 정면으로 ray를 쏩니다.
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.green);
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength))
        {
            // ray가 어떤 물체와 충돌했는지 확인합니다.
            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Monster"))
            {
                moveDirection *= -1f; 
                transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
            }

            // ray와 충돌한 지점을 눈으로 표시합니다.
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
        }
    }


}
