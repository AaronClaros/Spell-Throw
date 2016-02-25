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

    public List<GameObject> enemiesList = new List<GameObject>();


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

        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        if (hAxis != 0) {
            if (hAxis < -0.1f && viewRef == ViewDirection.right){
                viewRef = ViewDirection.left;
                //enemiesList = new List<GameObject>();
                Debug.Log("looking for left side enemies");
                //areaTargetRef.localScale = new Vector2(-1f, 1f);
                actualTarget = GetClosestEnemy(enemiesList);
            }
            if (hAxis > 0.1f && viewRef == ViewDirection.left)
            {
                viewRef = ViewDirection.right;
                Debug.Log("looking for right side enemies");
                //enemiesList = new List<GameObject>();
                //areaTargetRef.localScale = new Vector2(1f, 1f);
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
        Debug.Log(other.name);
        if (other.tag == "Enemy")
        {
            if (!enemiesList.Contains(other.gameObject))
            {
                enemiesList.Add(other.gameObject);
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
