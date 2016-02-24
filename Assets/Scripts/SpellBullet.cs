using UnityEngine;
using System.Collections;

public class SpellBullet : MonoBehaviour {
    public int damage;
    public float speed;
    public string textRef;
    public Transform target;

	// Use this for initialization
	void Start () {
        target = FindObjectOfType<SpellManager>().transform;
	}

    void Update()
    {
        if (target != null)
        {
            //transform.LookAt(target);
            transform.Translate(target.position*speed*Time.deltaTime);
            Debug.Log("Going to: " + target.position);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("enemy damaged");
        gameObject.SetActive(false);
    }
}
