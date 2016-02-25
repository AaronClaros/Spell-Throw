using UnityEngine;
using System.Collections;

	public enum LookRef { left, right};
	public class PlayerMovement : MonoBehaviour {
		public static PlayerMovement instance = null;

		public float playerScale = 1f;
		public float speed;
		private Vector2 velocity;
		private float hAxis;
		private float vAxis;

		//private Rigidbody2D rb2d;
		private SpriteRenderer sprite;

		public LookRef lRef;
		public Animator anim;



		public bool spelling_flag;
		private PlayerActions paRef;

		//private bool flip_flag;



		//Singlenton pattern
		void awake(){
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);		
		}

		// Use this for initialization
		void Start () {
			//rb2d = GetComponent<Rigidbody2D>();
			anim = GetComponent<Animator>();
			transform.localScale = new Vector2 (playerScale, playerScale);
			paRef = FindObjectOfType<PlayerActions>();
		}
		
		// Update is called once per frame
		void Update () {

			//spelling_flag = paRef.doingRitual;

			//moving player
			hAxis = Input.GetAxis("Horizontal");
			vAxis = Input.GetAxis("Vertical");

			if (!spelling_flag) {
				if (hAxis != 0) {
					transform.position = new Vector2 (transform.position.x + (speed * Time.deltaTime) * hAxis, transform.position.y);
					if (hAxis > 0f) {
						lRef = LookRef.right;
					}
					if (hAxis < 0f) {
						lRef = LookRef.left;
					}
					anim.SetBool("walking", true);
				} 
				if (vAxis != 0) {
					transform.position = new Vector2(transform.position.x, transform.position.y + (speed * Time.deltaTime) * vAxis);
					anim.SetBool("walking", true);
				}
				if (hAxis == 0 && vAxis == 0) {
					anim.SetBool ("walking", false);
				}

			}

			//fliping player
			if (lRef == LookRef.right) {
				Vector2 scale = new Vector2 (playerScale, playerScale);
				paRef.spellText.transform.localScale = scale; 
				transform.localScale = scale;  

			} else if (lRef == LookRef.left) {
				Vector2 scale = new Vector2 (playerScale * -1, playerScale);
				paRef.spellText.transform.localScale = scale; 
				transform.localScale = scale;
			}
			if (Input.GetButtonDown ("Fire1")) {
				Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				if (mousePos.x > transform.position.x)
					lRef = LookRef.right;
				else if (mousePos.x < transform.position.x)
					lRef = LookRef.left;
			}
		}
	}


