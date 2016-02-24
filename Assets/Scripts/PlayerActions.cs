using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

	public class PlayerActions : MonoBehaviour {
		[HideInInspector]
		public static PlayerActions instance = null;
		public int lives;
		public bool doingRitual;
		public Animator anim;

		public Transform spellCanvas;
		[HideInInspector]
		public Text spellText;
		public string sText;

		public GameObject actualTarget;


		//Singlenton pattern
		void awake(){
			if (instance == null)
				instance = this;
			else if (instance != null)
				Destroy (this);
		}

		// Use this for initialization
		void Start () {
			anim = GetComponent<Animator> ();
			spellCanvas = GameObject.Find("Canvas Spell").GetComponent<Transform>();
			spellCanvas.gameObject.SetActive (false);
			spellText = spellCanvas.FindChild("Text").GetComponent<Text>();
			spellText.font = GameManager.instance.readable_Font;
		}
		
		// Update is called once per frame
		void Update () {
			//showing text when spelling
			doingRitual = FindObjectOfType<PlayerMovement>().spelling_flag;
			if (doingRitual) {
				anim.SetBool ("spelling", true);
				//spellCanvas.position = transform.position;
				spellCanvas.gameObject.SetActive (true);

			} else {
				anim.SetBool ("spelling", false);
				spellCanvas.gameObject.SetActive (false);
			}

			if (lives <= 0) {
				anim.SetTrigger("dead");
			}
			//Changing canvas text
			if (doingRitual) {
                if (Input.inputString != " ") {
                    sText = sText + "" + Input.inputString;
                }
				CompareText();
				//Debug.Log ("Comparing");
			} else {
				sText = "";
			}
			spellText.text = sText.ToUpper();
		}

		void CompareText(){
			if (actualTarget != null){
                var enemy = actualTarget.GetComponent<EnemyScript>();
                if (enemy != null) {
                    if (InvertText(enemy.eName.ToUpper()) == sText.ToUpper())
                    {
                        Debug.Log(sText);
                        if (!enemy.dead_flag)
                        {
                            StopAllCoroutines();
                            StartCoroutine(enemy.DeadOnce());
                        }
                    }
                }
                else {
                    var boss = actualTarget.GetComponent<BossScript>();
                    if (InvertText(boss.eName.ToUpper()) == sText.ToUpper())
                    {
                        Debug.Log(sText);
                        if (!boss.dead_flag)
                        {
                            StopAllCoroutines();
                            StartCoroutine(boss.DeadOnce());
                        }
                    }
                }
			}
		}

		string InvertText(string text){
			char[] charArray = text.ToCharArray();
			Array.Reverse(charArray);
			return new string (charArray);
		}
	}

