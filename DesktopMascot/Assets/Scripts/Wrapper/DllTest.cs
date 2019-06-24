using UnityEngine;
using System.Collections;

public class DllTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // スペースキー押されたら画面の解像度を表示
            var screenWidth = WinApi.GetSystemMetrics(WinApi.SystemMetrics.SM_CXSCREEN);
            var screenHeight = WinApi.GetSystemMetrics(WinApi.SystemMetrics.SM_CYSCREEN);

            Debug.Log("screenWidth = " + screenWidth + ", screenHeight = " + screenHeight);
        }
    }
}
