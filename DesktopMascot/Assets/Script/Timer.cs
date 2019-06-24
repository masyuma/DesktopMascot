using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject GoMenu, TimerImg;
    public Text TextStartorStop;
    //　トータル制限時間
    private float totalTime;
    //　制限時間（分）
    [SerializeField]
    private int minute;
    //　制限時間（秒）
    [SerializeField]
    private float seconds;
    //　前回Update時の秒数
    private float oldSeconds;
    private Text timerText;

    bool isStart = false;
    bool one = true;

    void Start()
    {
        totalTime = minute * 60 + seconds;
        oldSeconds = 0f;
        timerText = GetComponentInChildren<Text>();
    }
    void Update()
    {
        //　一旦トータルの制限時間を計測；
        totalTime = minute * 60 + seconds;
        if (isStart == true)
        {
            totalTime -= Time.deltaTime;
            TextStartorStop.text = "Stop";
        }

        if (isStart == false)
        {
            TextStartorStop.text = "Start";
            if (one)
            {
                totalTime = 0f;
                one = false;
            }
        }

        //　再設定
        minute = (int)totalTime / 60;
        seconds = totalTime - minute * 60;
        //　タイマー表示用UIテキストに時間を表示する
        if ((int)seconds != (int)oldSeconds)
        {
            timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
        oldSeconds = seconds;
        //　制限時間以下になったらコンソールに『制限時間終了』という文字列を表示する
        if (totalTime <= 0f)
        {
            Debug.Log("制限時間終了");
            if (isStart)
            {
                isStart = false;
                one = true;
            }
        }
    }

    public void Mue()
    {
        if (isStart == false)
        {
            minute += 1;
            timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
    }

    public void Sue()
    {
        if (isStart == false)
        {
            seconds += 1;
        }
    }

    public void Msita()
    {
        if (isStart == false && minute >= 1)
        {
            minute -= 1;
            timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
    }

    public void Ssita()
    {
        if (isStart == false && seconds >= 1)
        {
            seconds -= 1;
        }
    }

    public void StartorStop()
    {
        isStart = !isStart;
        one = true;
    }

    public void BackTimer()
    {
        isStart = false;
        one = true;
        GoMenu.SetActive(true);
        TimerImg.SetActive(false);
    }
}
