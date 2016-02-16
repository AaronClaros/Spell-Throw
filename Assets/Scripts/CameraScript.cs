using UnityEngine;
using System.Collections;

	public class CameraScript : MonoBehaviour {
		public static CameraScript instance = null;
		public float dampTime = 0.15f;
		private Vector3 velocity = Vector3.zero;
		public Transform target;

		void Awake(){
			if (instance == null)
				instance = this;
			else if (instance != null)
				Destroy (this);
		}

		void Start(){
			target = FindObjectOfType<PlayerMovement>().transform;
		}
		
		// Update is called once per frame
		void Update () 
		{
			if (target)
			{
				Vector3 point = Camera.main.WorldToViewportPoint(target.position);
				Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); 
				//(new Vector3(0.5, 0.5, point.z));
				Vector3 destination = transform.position + delta;
				transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
			}
			
		}
	}
