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



    // ������ �����ϴ� �Լ�
    public void QuitGame()
    {
        // Unity �����Ϳ����� �����͸� �����ϰ�, ����� ���ӿ����� ������ �����մϴ�.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
