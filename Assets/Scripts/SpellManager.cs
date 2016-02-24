using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ViewDirection { up, down, left, right }

public class SpellManager : MonoBehaviour {
	public static SpellManager instance = null;

    public Transform actualTarget;
    public string targetName;

	public ViewDirection viewRef;

    private Transform areaTargetRef;

    List<GameObject> enemiesList = new List<GameObject>();


	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != null) {
			Destroy(this);
		}
	}

	// Use this for initialization
	void Start () {
        areaTargetRef = transform.FindChild("Target Area");
	}
	
	// Update is called once per frame
	void Update () {
        /*if (enemiesList == null | enemiesList.Count==0) {
            actualTarget = GetClosestEnemy(enemiesList);
        }*/

        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        if (hAxis != 0) {
            if (hAxis < 0 & viewRef == ViewDirection.right){
                enemiesList = null;
                areaTargetRef.localScale = new Vector2(-1f, 1f);
            }
            if (hAxis > 0 & viewRef == ViewDirection.left)
            {
                enemiesList = null;
                areaTargetRef.localScale = new Vector2(1f, 1f);
            }
            actualTarget = GetClosestEnemy(enemiesList);
            if (actualTarget != null) {
                targetName = actualTarget.GetComponent<EnemyScript>().eName;
            }
        }


        if (Input.GetButtonDown("Jump")) {
            FindObjectOfType<PlayerMovement>().spelling_flag = true;
        }
        else if  (Input.GetButtonUp("Jump")){
            FindObjectOfType<PlayerMovement>().spelling_flag = false;
        }

	}

 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (enemiesList != null) {
            if (other.tag == "Enemy")
            {
                if (!enemiesList.Contains(other.gameObject))
                {
                    enemiesList.Add(other.gameObject);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (enemiesList != null) {
            if (enemiesList.Contains(other.gameObject))
            {
                enemiesList.Remove(other.gameObject);
                if (other.transform == actualTarget)
                {
                    actualTarget = null;
                }
            }
        }
    }

    Transform GetClosestEnemy(List<GameObject> enemies)
    {
        Transform tMin = null;
        if (enemiesList != null) {
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            foreach (GameObject t in enemies)
            {
                float dist = Vector3.Distance(t.transform.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t.transform;
                    minDist = dist;
                }
            }
            if (tMin != null) Debug.Log(tMin.gameObject.name + " looked.");
        }
        
        return tMin;
    }

}
