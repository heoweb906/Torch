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


    // #. 방 나가기 함수
    public void LeaveRoomAndLoadScene(string sceneName)
    {
        Debug.Log("로비로 나감");

        // 방을 나가기 전에 현재 방에 있는지 확인합니다.
        if (PhotonNetwork.InRoom)
        {
            // 방을 나갑니다.
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            Debug.LogWarning("방을 나가려고 했지만 현재 방에 있지 않습니다.");
            SceneManager.LoadScene("Menu");
        }
    }
    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom() 호출됨");
        SceneManager.LoadScene("Menu");
    }

}
