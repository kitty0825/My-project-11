using UnityEngine;
using System.IO.Ports;

public class ArduinoHit : MonoBehaviour
{
    private SerialPort stream;
    private bool isPaused = false; // 👈 紀錄目前遊戲是否暫停

    void Start()
    {
        // 已經幫你修改為 COM4 囉！
        stream = new SerialPort("COM4", 9600);
        stream.ReadTimeout = 10; // 👈 設定讀取逾時，避免畫面卡頓
        stream.Open();
    }

    // 👈 每一幀都去檢查 Arduino 有沒有傳「按鈕按下」的訊號過來
    void Update()
    {
        if (stream != null && stream.IsOpen)
        {
            try
            {
                // 讀取從 Arduino 傳過來的一行字串
                string data = stream.ReadLine().Trim();
                // 如果收到 TOGGLE_PAUSE，就切換暫停狀態
                if (data == "TOGGLE_PAUSE")
                {
                    ToggleGamePause();
                }
            }
            catch (System.TimeoutException)
            {
                // 超時沒讀到資料是正常的，直接忽略即可
            }
        }
    }

    // 當角色碰到刺時，會自動執行這裡（保留你原本的亮燈邏輯）
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

    // 👈 切換暫停與開始的方法
    void ToggleGamePause()
    {
        isPaused = !isPaused; // 切換狀態（原本暫停就變開始，原本開始就變暫停）

        if (isPaused)
        {
            Time.timeScale = 0f; // 暫停遊戲時間（所有移動、物理都會靜止）
            Debug.Log("遊戲已暫停");
        }
        else
        {
            Time.timeScale = 1f; // 恢復遊戲時間
            Debug.Log("遊戲恢復開始");
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