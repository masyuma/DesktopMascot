using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeNowScript : MonoBehaviour
{
    public GameObject GoMenu, ClockImg;
	public GameObject textUI;
	public Text unitychanText;
	public Text YearText, DayText, ClockText;
	public Animator unitychan;

	bool one1 , one2;
	// Use this for initialization
	void Start()
    {
		one1 = true;
		one2 = true;
	}

    // Update is called once per frame
    void Update()
    {
        /*int y = System.DateTime.Now.Year;
        int M = System.DateTime.Now.Month;
        int d = System.DateTime.Now.Day;
        int H = System.DateTime.Now.Hour;
        int m = System.DateTime.Now.Minute;
        int s = System.DateTime.Now.Second;*/

        string dayOfWeekJa = "";

        switch (System.DateTime.Now.DayOfWeek)
        {
            case DayOfWeek.Sunday:
                dayOfWeekJa = "(日)";
                break;
            case DayOfWeek.Monday:
                dayOfWeekJa = "(月)";
                break;
            case DayOfWeek.Tuesday:
                dayOfWeekJa = "(火)";
                break;
            case DayOfWeek.Wednesday:
                dayOfWeekJa = "(水)";
                break;
            case DayOfWeek.Thursday:
                dayOfWeekJa = "(木)";
                break;
            case DayOfWeek.Friday:
                dayOfWeekJa = "(金)";
                break;
            case DayOfWeek.Saturday:
                dayOfWeekJa = "(土)";
                break;
        }

        switch (System.DateTime.Now.Hour)
        {
            case 00:
				if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one1)
					{
						SoundManager.PlaySE(0);
						one2 = true;
						one1 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "日付が変わったよ！";
				}
                break;
            case 01:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one2)
					{
						SoundManager.PlaySE(1);
						one1 = true;
						one2 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "１時！明日の準備は 大丈夫？";
				}
				break;
            case 02:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one1)
					{
						SoundManager.PlaySE(2);
						one2 = true;
						one1 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "２時だよ〜…わたし もう寝るね〜…おやふみ…";
				}
				break;
            case 03:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one2)
					{
						SoundManager.PlaySE(3);
						one1 = true;
						one2 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "ふぁんじ…";
				}
				break;
            case 04:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one1)
					{
						SoundManager.PlaySE(4);
						one2 = true;
						one1 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "ひょじー…";
				}
				break;
            case 05:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one2)
					{
						SoundManager.PlaySE(5);
						one1 = true;
						one2 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "ほじ…";
				}
				break;
            case 06:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one1)
					{
						SoundManager.PlaySE(6);
						one2 = true;
						one1 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "ほふじ…";
				}
				break;
            case 07:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one2)
					{
						SoundManager.PlaySE(7);
						one1 = true;
						one2 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "７時かぁ…。 ふわ〜ぁ…。おはよ〜";
				}
				break;
            case 08:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one1)
					{
						SoundManager.PlaySE(8);
						one2 = true;
						one1 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "８時だね。そろそろ エンジンかけなきゃ";
				}
				break;
            case 09:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one2)
					{
						SoundManager.PlaySE(9);
						one1 = true;
						one2 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "９時だよ！遅刻して ない？大丈夫？";
				}
				break;
            case 10:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one1)
					{
						SoundManager.PlaySE(10);
						one2 = true;
						one1 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "１０時〜";
				}
				break;
            case 11:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one2)
					{
						SoundManager.PlaySE(11);
						one1 = true;
						one2 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "１１時！もうすぐ お昼！";
				}
				break;
            case 12:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one1)
					{
						SoundManager.PlaySE(12);
						one2 = true;
						one1 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "お昼だよ〜！";
				}
				break;
            case 13:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one2)
					{
						SoundManager.PlaySE(13);
						one1 = true;
						one2 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "１３時をお知らせ しまーす";
				}
				break;
            case 14:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one1)
					{
						SoundManager.PlaySE(14);
						one2 = true;
						one1 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "１４時だね。一番 マッタリしちゃう時間…";
				}
				break;
            case 15:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one2)
					{
						SoundManager.PlaySE(15);
						one1 = true;
						one2 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "１５時…。油断する と眠気が…";
				}
				break;
            case 16:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one1)
					{
						SoundManager.PlaySE(16);
						one2 = true;
						one1 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "じゅうろく…へへ、 もう食べられない…よ…";
				}
				break;
            case 17:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one2)
					{
						SoundManager.PlaySE(17);
						one1 = true;
						one2 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "はっ！何時の間に１７ 時に！？";
				}
				break;
            case 18:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one1)
					{
						SoundManager.PlaySE(18);
						one2 = true;
						one1 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "もう１８時か〜。 そろそろ暗くなって くるね";
				}
				break;
            case 19:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one2)
					{
						SoundManager.PlaySE(19);
						one1 = true;
						one2 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "１９時だよ。みんな テレビ見てる時間 かな？";
				}
				break;
            case 20:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one1)
					{
						SoundManager.PlaySE(20);
						one2 = true;
						one1 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "２０時！お仕事 してる人はそろそろ 一区切りかな？";
				}
				break;
            case 21:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one2)
					{
						SoundManager.PlaySE(21);
						one1 = true;
						one2 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "２１時〜。何か 面白いテレビやって るかなぁ";
				}
				break;
            case 22:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one1)
					{
						SoundManager.PlaySE(22);
						one2 = true;
						one1 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "ふんふ〜ん♪２２時 だよーん♪";
				}
				break;
            case 23:
                if (System.DateTime.Now.Minute == 00 && System.DateTime.Now.Second == 00)
				{
					if (one2)
					{
						SoundManager.PlaySE(23);
						one1 = true;
						one2 = false;
					}
					unitychan.SetBool("Signal", true);
					textUI.SetActive(true);
					unitychanText.text = "ふい〜…あと一時間 で今日も終わりだね〜";
				}
				break;
        }

        YearText.text = DateTime.Now.ToString("yyyy年");
        DayText.text = DateTime.Now.ToString("MM月dd日" + dayOfWeekJa);
        ClockText.text = DateTime.Now.ToString("HH時mm分ss秒");
    }

    public void BackClock()
    {
        GoMenu.SetActive(true);
        ClockImg.SetActive(false);
    }
}
