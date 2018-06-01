using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveBall : MonoBehaviour {
	Rigidbody rb;
	public int ballspeed = 0;
	public int jumpspeed = 0;
	private bool istouching = true;
	private int counter;
	public Text Cointext;
	public AudioSource asource;
	public AudioClip aclip;
	void Start () {
		rb = GetComponent<Rigidbody>();
		counter = 12;
		Cointext.text = "COINS: " + counter;
	}

	void Update () {
		float Hmove = Input.GetAxis("Horizontal");
		float Vmove = Input.GetAxis("Vertical");

		Vector3 ballmove = new Vector3(Hmove,0.0f,Vmove);
		rb.AddForce(ballmove*ballspeed);

		if((Input.GetKey(KeyCode.Space)) && istouching == true){
			Vector3 balljump = new Vector3(0.0f, 6.0f, 0.0f);
			rb.AddForce(balljump*jumpspeed);
		}
		istouching = false;
	}

	private void OnCollisionStay(){
		istouching = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Coinstag"))
		{
			other.gameObject.SetActive(false);
			Cointext.text = "COINS: " + counter;
			counter--;
			asource.PlayOneShot(aclip);
			if (counter == 0)
			{
				SceneManager.LoadScene("EndScene");
			}
		}
	}
}
