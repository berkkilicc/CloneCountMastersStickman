using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    [Header("Instantiate,Move,Radius")]

    private Obstacle obstacle;

    public Transform player;
    public int NumberOfClone;
    public TextMeshPro Countertxt;
    [SerializeField] private GameObject Clone;
    [Range(0f, 1f)] [SerializeField] private float Distance, Radius;

    Animator anim;
    [SerializeField] private GameObject bloodEffects;
    [SerializeField] private GameObject bluebloodEffects;

    [Header("Raycast")]

    RaycastHit hit;
    public LayerMask Enemy;

    [Header("Move")]

    public bool Touch;
    public bool gameState;
    private Vector3 mouseStartPosition;
    private Vector3 playerStartPosition;
    public float playerTouchSpeed;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform Character;
    public float playerMoveSpeed;


    //[SerializeField] private Transform enemyarea;
    public bool attack;



    private void OnEnable()
    {
       
    }


    void Start()
    {
        player = transform;
        NumberOfClone = transform.childCount - 1;
        Countertxt.text = NumberOfClone.ToString();
        cam = Camera.main;
        gameState = false;
        attack = false;
        anim =GetComponent<Animator>();




    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameState = true;

        }


        MoveThePlayer();

        if (gameState)
        {
            player.Translate(player.forward * Time.deltaTime * playerMoveSpeed);

            for (int i = 1; i < transform.childCount; i++)
            {
             transform.GetChild(i).GetComponent<Animator>().SetBool("run", true);
            }

        }
        if (FindObjectOfType<Enemy>().enemyOfClone == 0)
        {
            attack = false;
            //playerMoveSpeed = 5f;
            
        }

        DestroyClone();
        RayCast();



    }



    public void MakeClone(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(Clone, transform.position, Quaternion.identity, transform);
        }

        NumberOfClone = transform.childCount - 1;
        Countertxt.text = NumberOfClone.ToString();

        FormatClone();
    }

    public void DestroyClone()
    {

        NumberOfClone = transform.childCount - 1;
        Countertxt.text = NumberOfClone.ToString();


    }

    private void OnTriggerEnter(Collider other)
    {
        //Gate Controller
        if (other.gameObject.tag == "gate")
        {
            other.transform.parent.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            other.transform.parent.GetChild(1).GetComponent<BoxCollider>().enabled = false;

            var gateManager = other.GetComponent<Gates>();


            if (gateManager.multiply)
            {
                MakeClone(NumberOfClone * gateManager.randomNumberMultiply - NumberOfClone);
            }
            else
            {
                MakeClone(NumberOfClone + gateManager.randomNumberIncrease - NumberOfClone);
            }
        }

        //Enemy Controller
        if (other.gameObject.tag == "Enemy" && FindObjectOfType<Enemy>().enemyOfClone >= 1)
        {

            Debug.Log("Enemy'e dokundu");
            attack = true;
            playerMoveSpeed = 1f;

            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Animator>().SetBool("run", true);
            }

        }
        else
        {
            attack = false;
            MoveThePlayer();

        }


        if (other.gameObject.tag == "EnemyArea")
        {

            Destroy(other.gameObject);
            FindObjectOfType<Enemy>().enemyOfClone--;
            FindObjectOfType<Enemy>().enemyCounterTxt.text.ToString();
            GameObject bloods = Instantiate(bloodEffects, transform.position, Quaternion.identity, transform);
            GameObject bluebloods = Instantiate(bluebloodEffects, transform.position, Quaternion.identity, transform);
        }

        //Obstacle P.E Controller
        if (other.gameObject.tag == "Obstacle")
        {
            GameObject bluebloods = Instantiate(bluebloodEffects, transform.position, Quaternion.identity, transform);
        }

        if (other.gameObject.tag == "Boss")
        {
            Destroy(other.gameObject,2f);
            playerMoveSpeed = 0;
            playerTouchSpeed = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Exit")
        {
            FormatClone();
        }
    }

    public void MoveThePlayer()
    {
        if (Input.GetMouseButtonDown(0) && gameState)
        {
            Touch = true;

            Plane plane = new Plane(Vector3.up, 0f);
            var ray = cam.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                mouseStartPosition = ray.GetPoint(distance + 1f);
                playerStartPosition = transform.position;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Touch = false;
        }

        if (Touch)
        {
            var plane = new Plane(Vector3.up, 0f);
            var ray = cam.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                Vector3 mousePos = ray.GetPoint(distance + 1f);
                Vector3 move = mousePos - mouseStartPosition;
                Vector3 control = playerStartPosition + move;
                if (NumberOfClone > 150)
                {
                    control.x = Mathf.Clamp(control.x, -0.681f, 0.681f);
                }
                else if (NumberOfClone > 100)  //numberof clone büyük 50 ise hareket mesafesini kod satýrý kadar kýsýtla 
                {
                    control.x = Mathf.Clamp(control.x, -1.68f, 1.68f);
                }
                else if (NumberOfClone > 50)  //numberof clone büyük 50 ise hareket mesafesini kod satýrý kadar kýsýtla 
                {
                    control.x = Mathf.Clamp(control.x, -1.468f, 1.468f);
                }
                else if (NumberOfClone > 40)
                {
                    control.x = Mathf.Clamp(control.x, -1.55f, 1.55f);
                }
                else if (NumberOfClone > 30)
                {
                    control.x = Mathf.Clamp(control.x, -1.711f, 1.711f);
                }
                else if (NumberOfClone > 10)
                {
                    control.x = Mathf.Clamp(control.x, -2.068f, 2.068f);
                }
                else//deðilse bu eksende kýsýtla
                {
                    control.x = Mathf.Clamp(control.x, -2.363f, 2.363f);
                }


                transform.position = new Vector3(Mathf.Lerp(transform.position.x, control.x, Time.deltaTime * playerTouchSpeed), transform.position.y, transform.position.z);
            }
        }



    }
    public void FormatClone()
    {
        for (int i = 1; i < player.childCount; i++)
        {
            float x = Distance * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
            float z = Distance * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);

            Vector3 newPos = new Vector3(x, -0.485f, z);

            player.transform.GetChild(i).DOLocalMove(newPos, 1f).SetEase(Ease.OutBack);


        }
    }

    public void RayCast()
    {

        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 3f, Enemy))
        {
            Debug.DrawRay(transform.position, Vector3.forward * hit.distance, Color.red);
            Debug.Log("Enemy görüldü");

        }
    }








}
