using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	public static Shoot instance = null;

	#region Shoot Stats
	public float _Rate;
	public float _Max_Range;
	public float _Damage;
	public float _Projectil_Speed;
	public Vector2 _Direcction;
	public Transform _Spawner;
	public Vector2 _Spawner_Pos;
	#endregion

	private float nextFireTime;
	public ObjectPool _Pool;

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);

		_Pool = GetComponent<ObjectPool> ();
		_Spawner = transform.FindChild ("Bullet Spawner");

	}

	void ShootTo(Vector2 direccion){
		GameObject bulletRef = _Pool.NextObject ();
		Projectil projectil = bulletRef.GetComponent<Projectil>();
		projectil.SetStats (_Damage, _Projectil_Speed, _Max_Range, direccion);
		projectil.transform.position = _Spawner_Pos;
		bulletRef.SetActive (true);
	}

	void Update(){
		_Direcction = new Vector2(Input.GetAxisRaw("Shoot Horizontal"), Input.GetAxisRaw("Shoot Vertical"));
		_Spawner_Pos = _Spawner.position;
		if (Mathf.Abs(_Direcction.x) != Mathf.Abs(_Direcction.y) && Time.time > nextFireTime) {
			nextFireTime = Time.time + _Rate * Time.deltaTime;
			ShootTo(_Direcction);
		}
	}
}
