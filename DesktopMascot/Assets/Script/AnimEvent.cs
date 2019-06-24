// この C# 関数はアニメーションイベントによって呼び出されます
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class AnimEvent : MonoBehaviour
{
	public GameObject textUI;
	public Text unitychanText;
	public Animator unitychan;

	void Start()
	{
		textUI.SetActive(false);
	}

	public void FaceClick()
	{
		int i = Random.Range(0, 5);
		switch (i)
		{
			case 0:
				SoundManager.PlaySE(24);
				textUI.SetActive(true);
				unitychanText.text = "きゃっ！";
				break;
			case 1:
				SoundManager.PlaySE(25);
				textUI.SetActive(true);
				unitychanText.text = "あんっ！";
				break;
			case 2:
				SoundManager.PlaySE(26);
				textUI.SetActive(true);
				unitychanText.text = "あううっ…";
				break;
			case 3:
				SoundManager.PlaySE(27);
				textUI.SetActive(true);
				unitychanText.text = "ひゃっ！";
				break;
			case 4:
				SoundManager.PlaySE(28);
				textUI.SetActive(true);
				unitychanText.text = "ひっどぉーいっ！";
				break;
			default:
				break;
		}
	}

	public void FaceClickEnd()
	{
		textUI.SetActive(false);
	}

	public void SignalEnd()
	{
		unitychan.SetBool("Signal", false);
		textUI.SetActive(false);
	}
}