using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using static UnityEngine.EventSystems.EventTrigger;


public class MenuController : MonoBehaviour
{
    [SerializeField]
    private RoomMatchManager roomManager;

    private void Awake()
    {
        roomManager = FindObjectOfType<RoomMatchManager>();
    }



    // 게임을 종료하는 함수
    public void QuitGame()
    {
        // Unity 에디터에서는 에디터를 종료하고, 빌드된 게임에서는 게임을 종료합니다.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
