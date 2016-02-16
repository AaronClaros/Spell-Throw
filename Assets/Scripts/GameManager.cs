using UnityEngine;
using System.Collections.Generic;
using System.Collections;

    public enum GameMode {easy, normal}

	public class GameManager : MonoBehaviour {

		public static GameManager instance = null;
		public BoardManager boardScript;

        public int level;
        public GameMode difficult;
		public Font readable_Font;
		public Font weird_Font;

		public GUIManager guiCanvas;

        public GameObject EnemyPrefab;
        //[HideInInspector]
        public List<GameObject> enemiesList;
        int impCount;

        public GameObject BossPrefab;
        public List<GameObject> bossList;

        public int listsCount;

        public int enemyIndex;

        public bool levelComplete;

        bool spawning_flag;
            
        [SerializeField]
        public List<Color> colorList;
        private char[] vocals = {'a','e','i','o','u'};
        private char[] consonants = { 'b', 'c', 'd', 'f', 'g','h', 'j', 'k', 'l', 'm','n', 'p', 'q', 'r', 's','t', 'v', 'w', 'x', 'y','z'};

		void Awake()
		{
			if (instance == null)
				instance = this;
			else if (instance != this)
				Destroy(gameObject);	

			DontDestroyOnLoad(gameObject);
            Random.seed = (int)System.DateTime.Now.Ticks;

			boardScript = GetComponent<BoardManager>();
			
            impCount = 0;
            Time.timeScale = 0;
            levelComplete = false;
            guiCanvas.gameObject.SetActive(true);
            //InitGame();
		}

		void InitGame(){
            
            colorList = CreateRandomColorList(difficult);
            CreateEnemiesList(enemiesList, colorList, level);
		}

		void Start(){
            enemiesList = new List<GameObject>();
            colorList = new List<Color>();
            InitGame();

			guiCanvas = FindObjectOfType<GUIManager> ();
            var plRef = FindObjectOfType<PlayerMovement>();
            plRef.spelling_flag = true;
            levelComplete = false;
            
		}

		void Update(){/*
			if (Input.GetKeyDown (KeyCode.Escape)) {
				guiCanvas.gameObject.SetActive (true);
                guiCanvas.anim_end = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape)){
				guiCanvas.gameObject.SetActive (false);
                guiCanvas.anim_end = true ;
            }*/
            
            StartCoroutine(RandomSpawn(2f + (Time.time / 100)));

            if (Input.GetKeyDown(KeyCode.Escape)) {
                Application.Quit();
            }

            if (levelComplete) {
                StartCoroutine(Passlevel(1f));
            }
		}

        void CreateEnemiesList(List<GameObject> eList, List<Color> cList, int actualLevel ) {
            Debug.Log("setup eney list");
            for (int i = 0; i <= actualLevel; i++) {
                var newEnemy = Instantiate(EnemyPrefab);
                newEnemy.transform.position = new Vector2(100, 100);
                var enemyScript = newEnemy.GetComponent<EnemyScript>();
                enemyScript.eName = GenerateRandomName(actualLevel);
                int randomIndex = Random.Range(0, cList.Count);
                Color color = cList[randomIndex];
                enemyScript.GetComponent<SpriteRenderer>().color = color;
                newEnemy.SetActive(false);
                eList.Add(newEnemy);
            }

            listsCount = eList.Count;
        }

        string GenerateRandomName(int actualLevel) {
            string rName = "";
            for (int i = 0; i <= actualLevel; i++){
                string rLetter = Random.Range(-1,1) >= 0? rLetter = 
                                    vocals[Random.Range(0, vocals.Length)].ToString(): 
                                    consonants[Random.Range(0, consonants.Length)].ToString();
                if (i % 2 == 0) {
                    rName = rName + consonants[Random.Range(0, consonants.Length)].ToString();
                }
                else if (i % 2 != 0) {
                    rName = rName + rLetter;
                }
            }
            return rName;
        }

        List<Color> CreateRandomColorList(GameMode actDifficult) {
            List<Color> colorL = new List<Color>();
            int listSize = difficult == GameMode.easy ? level + 3 : level + 6;
            Color rColor = new Color(1,1,1,1);
            for (int i = 0; i <= listSize; i++) {
                rColor = new Color(Random.value, Random.value, Random.value, 1f);
                colorL.Add(rColor);
            }
            
            return colorL;
        }

        void CreateBossList(List<GameObject> bList, int actualLevel, GameMode difficult) {
            impCount = difficult == GameMode.easy ? 1 : 2;
            if (level%5 == 0){
                impCount = difficult == GameMode.easy ? impCount+1 : impCount+2;
            }
            for (int i = 0; i <= actualLevel; i++)
            {/*
                var newEnemy = Instantiate(EnemyPrefab);
                newEnemy.transform.position = new Vector2(100, 100);
                var enemyScript = newEnemy.GetComponent<EnemyScript>();
                enemyScript.eName = GenerateRandomName(actualLevel);
                int randomIndex = Random.Range(0, cList.Count);
                Color color = cList[randomIndex];
                enemyScript.GetComponent<SpriteRenderer>().color = color;
                newEnemy.SetActive(false);
                eList.Add(newEnemy);*/
            }
        }

        IEnumerator RandomSpawn(float intervalTime) {
            yield return new WaitForSeconds(intervalTime);
            spawning_flag = true;
            if (enemyIndex < enemiesList.Count) {
                var enemy = enemiesList[enemyIndex++];
                enemy.transform.position = new Vector2(Random.Range(-3.5f, 3.5f), Random.Range(-2.8f, 2.8f));
                enemy.SetActive(true);
                yield return 0;
            }
            
        }


        IEnumerator Passlevel(float intervalTime) {
            
            yield return new WaitForSeconds(intervalTime);
            Application.LoadLevel(0);
            Debug.Log("End Level");
            level++;
            levelComplete = false;
            yield return 0;
            StopAllCoroutines();
        }

        bool CheckWinState() {
            if (Time.timeScale != 0) {
                var enemiesOnRoom = FindObjectsOfType<EnemyScript>();
                if (enemiesOnRoom.Length <= enemiesList.Count)
                    return true;
                else
                    return false;
            }
            return false;
        }

	}

