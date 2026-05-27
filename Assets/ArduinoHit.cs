using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoHit : MonoBehaviour
{
    private SerialPort stream;

    void Start()
    {
        // ⚠️ 請把 "COM3" 改成您 Arduino 連接電腦的實際埠號
        stream = new SerialPort("COM4", 9600);
        stream.ReadTimeout = 100;
        stream.Open();
    }

    // 當角色碰到刺時，會自動執行這裡
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 檢查碰到的物件名稱是不是叫做 DeathLine
        if (collision.gameObject.name == "DeathLine")
        {
            if (stream != null && stream.IsOpen)
            {
                stream.Write("1"); // 發送字串 "1" 給 Arduino 讓它亮燈
                Debug.Log("💥 碰到刺了！發送亮燈訊號！");
            }
        }
    }

    // 關閉遊戲時，必須把連線切斷
    void OnApplicationQuit()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Close();
        }
    }
}