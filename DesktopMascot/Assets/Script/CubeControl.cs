using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class CubeControl : MonoBehaviour
{
	public Animator unitychan;

	// Use this for initialization
	void Start()
	{
	}
	public void OnUserAction()
	{
		unitychan.SetBool("Next", true);
	}
	public void OffUserAction()
	{
		unitychan.SetBool("Next", false);
	}
}
