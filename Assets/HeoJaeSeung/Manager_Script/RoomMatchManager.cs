using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using static UnityEngine.EventSystems.EventTrigger;
using TMPro;

public class RoomMatchManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "v1.0";
    private string userId = "heojae";

    public TMP_InputField roomNameText;

    // 룸 목록을 저장하기 위한 딕셔너리 자료형
    private Dictionary<string,GameObject> roomDict = new Dictionary<string, GameObject>();

    // 룸을 표시할 프리펩
    public GameObject roomPrefs;

    // Room 프리펩을 차일화 시킬 부모 객체
    public Transform scrollContent;



    // #. 테스트용
    public GameObject Ascreen;
    public GameObject LoadingIcon;

    private void Awake()
    {        
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    // #. PunCallBack 함수 중 하나이다. 로컬 플레이어가 마스터 서버에 성공적으로 연결되었을 때 호출되는 함수이다.
    // 보통 이 시점에서 플레이어는 다른 플레이어와의 연결을 위한 준비를 할 수 있다. 
    public override void OnConnectedToMaster()
    {
        Debug.Log("01. 포톤 서버에 접속");
        PhotonNetwork.JoinLobby();
    }


    // #. 로비에 성공적으로 참여했을 때 호출된다.
    public override void OnJoinedLobby()
    {
        Debug.Log("02. 로비에 접속");
        

        // 테스트용... 로비에 접속이 완료되기 전에 클릭을 방지
        Ascreen.SetActive(false);
        LoadingIcon.SetActive(true);
    }

    // #. 랜덤한 방에 참여에 실패했을 때 호출된다.
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("랜덤 룸 접속 실패");

        RoomOptions room = new RoomOptions();
        room.IsOpen = true;
        room.IsVisible = true;
        room.MaxPlayers = 2;
        room.CleanupCacheOnLeave = true;

        roomNameText.text = $"USER_{Random.Range(1, 100):000}";

        PhotonNetwork.CreateRoom("room_1", room);
    }

    // #. 방이 성공적으로 생성되었을 때 호출된다.
    public override void OnCreatedRoom()
    {
        Debug.Log("03. 방 생성 완료");

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Play");
        }
    }

    // #. 방에 성공적으로 참여했을 때 호출된다.
    public override void OnJoinedRoom()
    {
        Debug.Log("04. 방 입장 완료");
    }



    // ###########################################################################################
    // #. 만들고 있는 함수들
    // ###########################################################################################


    // 방 생성 함수 (테스트용)
    public void ButtonCreateRoom()
    {
        RoomOptions room = new RoomOptions();
        room.IsOpen = true;
        room.IsVisible = true;
        room.MaxPlayers = 2;
        room.CleanupCacheOnLeave = true;

        PhotonNetwork.CreateRoom("room_1", room);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject tempRoom = null;
        
        foreach(var room in roomList)
        {
            // 룸이 삭제된 경우
            if(room.RemovedFromList == true)
            {
                roomDict.TryGetValue(room.Name, out tempRoom);
                Destroy(tempRoom);
                roomDict.Remove(room.Name);
            }
            // 룸 정보가 갱신된 경우
            else
            {
                // 룸이 처음 생성된 경우
                if(roomDict.ContainsKey(room.Name) == false)
                {
                    GameObject _room = Instantiate(roomPrefs, scrollContent);
                    _room.GetComponent<RoomData>().RoomInfo = room;
                    roomDict.Add(room.Name, _room);
                }
                // 룸 정보를 갱신하는 경우
                else
                {
                    roomDict.TryGetValue (room.Name, out tempRoom);
                    tempRoom.GetComponent<RoomData>().RoomInfo = room;
                }
            }
        }

    }


    #region UI_BUTTON_CALLBACK

    // 랜덤 방 입장
    public void OnRandomBtn()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    
    // Room 버튼 클릭 시 (룸 생성)
    public void OnMakeRoomClick()
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 2;

        if (string.IsNullOrEmpty(roomNameText.text))
        {
            // 랜덤 방이름 부여
            roomNameText.text = $"ROOM_{Random.Range(1, 100):000}";
        }

        PhotonNetwork.CreateRoom(roomNameText.text, ro);
    }
    #endregion

}
