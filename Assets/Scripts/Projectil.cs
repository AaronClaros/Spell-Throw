using UnityEngine;
using System.Collections;

public class Projectil : MonoBehaviour {

	#region Projectil Stats
	public float _Damage;
	public float _Speed;
	public float _Max_Range;
	public Vector2 _Direcction;

	private Vector2 _Origin;
	private float _Min_Range = 2;
	#endregion 

	void Start(){
		_Origin = Shoot.instance._Spawner_Pos;
	}

	void Update(){
		transform.Translate(_Direcction * (_Speed * Time.deltaTime));

		Vector2 center = _Origin;
		Vector2 position = transform.position; // outside your desired circle
		
		Vector2 offset = position - center;
		Vector2.ClampMagnitude(offset, 2f);
		
		position = center + offset;
		transform.position = position;
	}

	public void SetStats( float damage, float speed, float maxRange, Vector2 direcction){
		_Damage = damage;
		_Speed = speed;
		_Max_Range = maxRange;
		_Direcction = direcction;
	}

	void OnBecameInvisible(){
		gameObject.SetActive (false);
		transform.position = new Vector2(100f, 0f);
	}
}
