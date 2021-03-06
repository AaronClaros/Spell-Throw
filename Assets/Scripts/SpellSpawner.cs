using UnityEngine;
using System.Collections;

public class SpellSpawner : MonoBehaviour {
    public PlayerMovement plMoveRef;
    public Transform target;
    public ObjectPool pool;

	// Use this for initialization
	void Start () {
        target = FindObjectOfType<SpellManager>().transform;
        pool = GetComponent<ObjectPool>();
        plMoveRef = FindObjectOfType<PlayerMovement>();
	}
	
	// Update is called once per frame
    void Update() { 
        var playerSpelling = plMoveRef.spelling_flag;
        target = FindObjectOfType<SpellManager>().actualTarget;
        if (playerSpelling) {
            if (Input.anyKeyDown) {
                if (!Input.GetKeyDown(KeyCode.Space)) {
                    Debug.Log("shooting spells");
                    StartCoroutine(LaunchSpell(0.0f));

                }
            }
        }

    }

    IEnumerator LaunchSpell(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        var bullet = pool.NextObject().GetComponent<SpellBullet>();
        bullet.transform.localPosition = plMoveRef.transform.position;
        if (target != null)
        {
            bullet.target = target;
            bullet.gameObject.SetActive(true);
        }
        StopAllCoroutines();
    }

}
