using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{




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
