using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

	public class GUIManager : MonoBehaviour {
		public static GUIManager instance = null;
		public Transform canvas;

		public Transform leftPage;
		public Transform rightPage;

		private Transform imageBook;
		//private Text leftPageText;
		//private Text rightPageText;

		private Image leftPageImage;
		private Image rightPageImage;

		private Animator anim;

        private List<GameObject> enemiesList;

        private string[] namesList;
        private Color[] colorList;

        private int actualEnemyindex = 0;

        public bool anim_end;
		void Awake(){
			if (instance == null)
				instance = this;
			else if (instance != null)
				Destroy (this);
		}

		// Use this for initialization
		void Start () {
			canvas = GetComponent<Transform>();
			imageBook = canvas.transform.FindChild ("Image Book").transform;

			leftPage = canvas.transform.FindChild ("Image Book").FindChild ("Page Left").transform;
			rightPage = canvas.transform.FindChild ("Image Book").FindChild ("Page Right").transform;

			//leftPageText = leftPage.FindChild("Text").GetComponent<Text> ();
			//rightPageText = rightPage.FindChild("Text").GetComponent<Text> ();

			leftPageImage = leftPage.GetComponent<Image>();
			rightPageImage = rightPage.GetComponent<Image>();

			anim = imageBook.GetComponent<Animator> ();
            /*
			leftPageText.font = GameManager.instance.readable_Font;
			rightPageText.font = GameManager.instance.readable_Font;

            List<GameObject> enemiesList = GameManager.instance.enemiesList;
            colorList = new Color[GameManager.instance.listsCount];
            namesList = new string[GameManager.instance.listsCount];
            for (int i = 0; i <= enemiesList.Count; i++)
            {
                namesList[i] = enemiesList[i].GetComponent<EnemyScript>().eName;
                colorList[i] = enemiesList[i].GetComponent<SpriteRenderer>().color;
            }*/
            


            /*
			enemiesList = FindObjectsOfType<EnemyScript> ();
			if (enemiesList.Length >= 1){
				leftPageImage.color = enemiesList[0].sprRefColor;
				leftPageText.text = enemiesList[0].eName.ToUpper();

				rightPageImage.color = enemiesList[1].sprRefColor;
				Debug.Log(enemiesList[1].sprRefColor);
				rightPageText.text = enemiesList[1].eName.ToUpper();
			}*/
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void Page_Left(){
            Time.timeScale = 1f;
			StartCoroutine (PassPage ("pass_left", 0.3f));
            var plRef = FindObjectOfType<PlayerMovement>();
            if (!anim_end) {
                this.gameObject.SetActive(false);
                plRef.spelling_flag = false;
                
            }
		}
		public void Page_Right(){
			StartCoroutine (PassPage ("pass_right", 0.3f));
            Application.Quit();
		}

		IEnumerator PassPage(string id, float duration){
            anim.SetBool(id, true);
            yield return new WaitForSeconds(duration);/*
            leftPageImage.color = colorList[actualEnemyindex--];
            leftPageText.text = namesList[actualEnemyindex--];*/
            anim.SetBool(id, false);
            anim_end = true;
		}
	}

