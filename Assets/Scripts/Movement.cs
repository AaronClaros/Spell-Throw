using UnityEngine;
using System.Collections;

public enum MovementType {grounded, floating}; 
public class Movement : MonoBehaviour {
	public static Movement instance = null;

	#region Movement Stats
	public float _Speed;
	public Vector2 _Direcction;
	public MovementType _Type;
	#endregion
	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
	}

	void MoveTo(Vector2 direcction){
		transform.position = new Vector2 (transform.position.x + (_Speed * Time.deltaTime) * direcction.x, 
		                                  transform.position.y + (_Speed * Time.deltaTime) * direcction.y);

	}

	void Update(){
		_Direcction = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		if (_Direcction != Vector2.zero) {
			MoveTo(_Direcction);
		}
	}
}
