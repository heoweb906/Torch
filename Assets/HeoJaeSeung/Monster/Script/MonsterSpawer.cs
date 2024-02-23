using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using static UnityEngine.EventSystems.EventTrigger;
using TMPro;

public class MonsterSpawer : MonoBehaviourPunCallbacks
{
    public Transform posiotnSpawn;
    public GameObject[] monsterPrefabs;
    public GameObject randomMonster;
    // 0 - �Ϲ� �ذ�
    // 1 - �⸶�� �ذ�
    // 2 - ���� �ذ�
    // 3 - ���� �ذ�
    // 4 - ���� �ذ�

    private void Start()
    {
        InvokeRepeating("SpawnMonster", 0f, 3f);
    }


    [PunRPC]
    private void SpawnMonster()
    {
        if (PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient)
        {
            // ���� ����
            int radomNum = Random.Range(0, monsterPrefabs.Length);

            randomMonster = monsterPrefabs[radomNum];
            //GameObject monsterInstance = Instantiate(randomMonster, posiotnSpawn.position, Quaternion.identity);

            GameObject monsterInstance = PhotonNetwork.Instantiate(this.randomMonster.name, posiotnSpawn.position, Quaternion.identity);
        }
          
    }

}
