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

    // �� ����� �����ϱ� ���� ��ųʸ� �ڷ���
    private Dictionary<string,GameObject> roomDict = new Dictionary<string, GameObject>();

    // ���� ǥ���� ������
    public GameObject roomPrefs;

    // Room �������� ����ȭ ��ų �θ� ��ü
    public Transform scrollContent;



    // #. �׽�Ʈ��
    public GameObject Ascreen;
    public GameObject LoadingIcon;

    private void Awake()
    {        
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    // #. PunCallBack �Լ� �� �ϳ��̴�. ���� �÷��̾ ������ ������ ���������� ����Ǿ��� �� ȣ��Ǵ� �Լ��̴�.
    // ���� �� �������� �÷��̾�� �ٸ� �÷��̾���� ������ ���� �غ� �� �� �ִ�. 
    public override void OnConnectedToMaster()
    {
        Debug.Log("01. ���� ������ ����");
        PhotonNetwork.JoinLobby();
    }


    // #. �κ� ���������� �������� �� ȣ��ȴ�.
    public override void OnJoinedLobby()
    {
        Debug.Log("02. �κ� ����");
        

        // �׽�Ʈ��... �κ� ������ �Ϸ�Ǳ� ���� Ŭ���� ����
        Ascreen.SetActive(false);
        LoadingIcon.SetActive(true);
    }

    // #. ������ �濡 ������ �������� �� ȣ��ȴ�.
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("���� �� ���� ����");

        RoomOptions room = new RoomOptions();
        room.IsOpen = true;
        room.IsVisible = true;
        room.MaxPlayers = 2;
        room.CleanupCacheOnLeave = true;

        roomNameText.text = $"USER_{Random.Range(1, 100):000}";

        PhotonNetwork.CreateRoom("room_1", room);
    }

    // #. ���� ���������� �����Ǿ��� �� ȣ��ȴ�.
    public override void OnCreatedRoom()
    {
        Debug.Log("03. �� ���� �Ϸ�");

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Play");
        }
    }

    // #. �濡 ���������� �������� �� ȣ��ȴ�.
    public override void OnJoinedRoom()
    {
        Debug.Log("04. �� ���� �Ϸ�");
    }



    // ###########################################################################################
    // #. ����� �ִ� �Լ���
    // ###########################################################################################


    // �� ���� �Լ� (�׽�Ʈ��)
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
            // ���� ������ ���
            if(room.RemovedFromList == true)
            {
                roomDict.TryGetValue(room.Name, out tempRoom);
                Destroy(tempRoom);
                roomDict.Remove(room.Name);
            }
            // �� ������ ���ŵ� ���
            else
            {
                // ���� ó�� ������ ���
                if(roomDict.ContainsKey(room.Name) == false)
                {
                    GameObject _room = Instantiate(roomPrefs, scrollContent);
                    _room.GetComponent<RoomData>().RoomInfo = room;
                    roomDict.Add(room.Name, _room);
                }
                // �� ������ �����ϴ� ���
                else
                {
                    roomDict.TryGetValue (room.Name, out tempRoom);
                    tempRoom.GetComponent<RoomData>().RoomInfo = room;
                }
            }
        }

    }


    #region UI_BUTTON_CALLBACK

    // ���� �� ����
    public void OnRandomBtn()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    
    // Room ��ư Ŭ�� �� (�� ����)
    public void OnMakeRoomClick()
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 2;

        if (string.IsNullOrEmpty(roomNameText.text))
        {
            // ���� ���̸� �ο�
            roomNameText.text = $"ROOM_{Random.Range(1, 100):000}";
        }

        PhotonNetwork.CreateRoom(roomNameText.text, ro);
    }
    #endregion

}
