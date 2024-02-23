using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using static UnityEngine.EventSystems.EventTrigger;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviourPunCallbacks
{


    // #. �� ������ �Լ�
    public void LeaveRoomAndLoadScene(string sceneName)
    {
        Debug.Log("�κ�� ����");

        // ���� ������ ���� ���� �濡 �ִ��� Ȯ���մϴ�.
        if (PhotonNetwork.InRoom)
        {
            // ���� �����ϴ�.
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            Debug.LogWarning("���� �������� ������ ���� �濡 ���� �ʽ��ϴ�.");
            SceneManager.LoadScene("Menu");
        }
    }
    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom() ȣ���");
        SceneManager.LoadScene("Menu");
    }

}
