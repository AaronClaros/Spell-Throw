using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Action { affect, cover, launch, expand, compress }

public class SpellManager : MonoBehaviour {

    public List<GameObject> enemiesList;

    public Transform actualTarget;
    public string targetName;
	// Use this for initialization
	void Start () {
        enemiesList = FindObjectOfType<GameManager>().enemiesList;
	}
	
	// Update is called once per frame
	void Update () {
        if (enemiesList == null | enemiesList.Count==0) {
            enemiesList = FindObjectOfType<GameManager>().enemiesList;
        }


        float hAxis = Input.GetAxis("Horizontal2");
        float vAxis = Input.GetAxis("Vertical2");
        if (hAxis != 0 | vAxis != 0) {
            actualTarget = GetClosestEnemy(enemiesList);
            targetName = actualTarget.GetComponent<EnemyScript>().eName;
        }
	}


    Transform GetClosestEnemy(List<GameObject> enemies)
    {
        Transform tMin = null;
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
        return tMin;
    }
}
