using UnityEngine;
using System.Collections;

public class SpellBullet : MonoBehaviour {
    public int damage;
    public float speed;
    public string textRef;
    public Transform target;

	// Use this for initialization
	void Start () {
	}

    void Update()
    {
        if (target != null)
        {
            //transform.LookAt(target);
            transform.position = Vector2.Lerp(transform.position, new Vector2(target.position.x, target.position.y-0.2f), Time.deltaTime * speed);
            /*
            Debug.Log("Going to: " + target.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), 2 * Time.deltaTime);
            //move towards the player
            transform.position += transform.forward * speed * Time.deltaTime;*/
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("enemy damaged");
        this.gameObject.SetActive(false);
    }
}
