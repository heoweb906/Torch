using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;
using UnityEditor;

public class RoomData : MonoBehaviour
{
    public TMP_Text RoomInfoText;
    private RoomInfo _roomInfo;

    public RoomInfo RoomInfo
    {
        get
        {
            return _roomInfo;
        }
        set
        {
            _roomInfo = value;
            RoomInfoText.text = $"{_roomInfo.Name} ({_roomInfo.PlayerCount}/{_roomInfo.MaxPlayers})";
            GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnEnterRoom(_roomInfo.Name));
        }
    }

    private void Awake()
    {
        RoomInfoText = GetComponentInChildren<TMP_Text>();
    }

    void OnEnterRoom(string roomName)
    {
        RoomOptions room = new RoomOptions();
        room.IsOpen = true;
        room.IsVisible = true;
        room.MaxPlayers = 2;

        PhotonNetwork.JoinOrCreateRoom(roomName, room, TypedLobby.Default);
    }

}
