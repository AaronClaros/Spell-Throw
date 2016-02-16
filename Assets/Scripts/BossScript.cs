using UnityEngine;
using System.Collections;
using UnityEngine.UI;

    public class BossScript : MonoBehaviour
    {
        public PlayerActions player;
        public string eName;
        public float speed;
        public float distanceMovement;
        public bool being_spelling;
        public GameObject invokeCircle;
        private SpriteRenderer spriteCircle;

        private Animator anim;
        public Text enemyText;

        public bool dead_flag;
        public bool move_flag;
        public bool playerNearby_flag;

        private GUIManager guiRef;

        public Color sprRefColor;

        Vector3 lastPlayerPos;
        float journeyLength;
        float startTime;

        float distCovered;
        float fracJourney;

        public GameMode difficult;
        private Color imp1ColorRef;
        private Color imp2ColorRef;

        private Transform imp1Ref;
        private Transform imp2Ref;
        private Transform imp3Ref;

        public GameObject impPrefab;

        public bool attack_flag;

        private CircleCollider2D colliderRef;
        // Use this for initialization
        void Start()
        {
            player = FindObjectOfType<PlayerActions>();
            name = gameObject.name;
            invokeCircle = transform.FindChild("Invoke Circle").gameObject;

            //spriteCircle = invokeCircle.GetComponent<SpriteRenderer>();
            invokeCircle.SetActive(false);
            anim = GetComponent<Animator>();
            enemyText = transform.FindChild("Canvas Enemy").GetComponentInChildren<Text>();
            enemyText.text = eName.ToLower();
            enemyText.font = GameManager.instance.weird_Font;

            sprRefColor = GetComponent<SpriteRenderer>().color;

            imp1ColorRef = transform.FindChild("imp1").GetComponent<SpriteRenderer>().color;
            imp2ColorRef = difficult == GameMode.easy ? imp2ColorRef = Color.white : imp2ColorRef = transform.FindChild("imp2").GetComponent<SpriteRenderer>().color; ;

            imp1Ref = transform.FindChild("imp1");
            if (difficult == GameMode.normal)
                imp2Ref = transform.FindChild("imp2");

            colliderRef = GetComponent<CircleCollider2D>();
            /*
            
            lastPlayerPos = player.transform.position;
            journeyLength = Vector3.Distance(transform.position, player.transform.position);
            startTime = Time.time;
            move_flag = false;
            playerNearby_flag = false;*/
        }

        // Update is called once per frame
        void Update()
        {
            if (being_spelling)
            {
                //Debug.Log ("hOI, am "+name+" you touch me?");
                invokeCircle.SetActive(true);
            }
            else
            {
                //Debug.Log("Were you go??");
                invokeCircle.SetActive(false);
            }


            //Check player nearby
            bool check = CheckPlayerNearby(transform.position, player.transform.position, distanceMovement);
            if (check & !playerNearby_flag)
            {
                playerNearby_flag = true;
                startTime = Time.time;
                lastPlayerPos = player.transform.position;
                journeyLength = Vector3.Distance(transform.position, player.transform.position);
            }
            //Move to Player

            if (!move_flag)
            {
                move_flag = true;
                startTime = Time.time;
                journeyLength = Vector3.Distance(transform.position, player.transform.position);
                lastPlayerPos = player.transform.position;

            }
            else
            {
                if (playerNearby_flag)
                {
                    float ranAction = Random.value;
                    if (ranAction > 0.5f)
                        MovingToPlayer("jumping", speed);
                    //else
                        //SlimeAttack();
                }
            }



        }

        void OnMouseDown()
        {
            being_spelling = true;
            player.actualTarget = gameObject;
        }

        void OnMouseUp()
        {
            being_spelling = false;
            player.actualTarget = null;
        }

        public IEnumerator DeadOnce()
        {
            anim.SetTrigger("dead");
            enemyText.font = GameManager.instance.readable_Font;
            enemyText.text = eName.ToUpper();
            while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.95f)
            {
                yield return null;
            }
            dead_flag = true;
            yield return new WaitForSeconds(1f);
            Debug.Log(eName + ": I am dead");
            gameObject.SetActive(false);
        }

        IEnumerator JumpToPlayer(float IntervalTime)
        {
            yield return new WaitForSeconds(IntervalTime);
            move_flag = true;
            Vector2 playerPos = player.transform.position;
            Vector2 playerDirection = Vector2.MoveTowards(transform.position, playerPos, distanceMovement);
            transform.Translate(playerDirection);
            //float distance = Vector3.Distance(enemyPos, playerPos);


        }

        bool CheckPlayerNearby(Vector3 enemyPos, Vector3 playerPos, float DistanceToTrigger)
        {
            if (!playerNearby_flag)
            {
                float distance = Vector3.Distance(enemyPos, playerPos);
                //Debug.Log("You are at: "+ distance+"u Against me");
                if (distance <= DistanceToTrigger)
                    return true;
                else
                    return false;
            }
            else
            {
                return true;
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                playerNearby_flag = false;
            }
        }

        void MovingToPlayer(string animName, float moveSpeed)
        {
            anim.SetBool(animName, true);
            distCovered = (Time.time - startTime) * moveSpeed;
            fracJourney = distCovered / journeyLength;
            transform.position = Vector2.Lerp(transform.position, lastPlayerPos, fracJourney);
            //Debug.Log("Lerping to: " + lastPlayerPos);
            float percentageDifferenceAllowed = 0.01f;// is 1%
            if ((transform.position - lastPlayerPos).sqrMagnitude <= (transform.position * percentageDifferenceAllowed).sqrMagnitude)
            {
                //Debug.Log(eName + ": Stay quiet!!");
                move_flag = false;
                playerNearby_flag = false;
                anim.SetBool(animName, false);
            }
        }

        public void SetColorSprite(Color c)
        {
            this.GetComponent<SpriteRenderer>().color = c;
        }

        /*
        public void SlimeAttack(){
            if(!attack_flag){
                attack_flag = true;
                colliderRef.isTrigger = true;
                var imp = Instantiate(impPrefab);
                imp.GetComponent<SpriteRenderer>().color = difficult == GameMode.easy? imp1ColorRef: imp2ColorRef;
                imp.transform.parent = null;
                imp.transform.localScale = new Vector2(2f, 2f);
                imp.transform.position = imp1Ref.position;
                var impScript = imp.GetComponent<EnemyScript>();
                var playerPos = PlayerMovement.instance.transform.position;
                impScript.MovingToPlayer("rolling", 1f, PlayerMovement.instance.transform.position); 
            }
        }*/
    }

