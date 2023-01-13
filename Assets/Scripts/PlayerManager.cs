using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    private Obstacle obstacle;
    Animator anim;
    public Transform player;
    public int NumberOfClone;
    public TextMeshPro Countertxt;
    [SerializeField] private GameObject Clone;
    [Range(0f, 1f)] [SerializeField] private float Distance, Radius;

    [Header("Move")]

    public bool Touch;
    public bool gameState;
    private Vector3 mouseStartPosition;
    private Vector3 playerStartPosition;
    public float playerTouchSpeed;
    private Camera camera;
    [SerializeField] private Transform Character;
    public float playerMoveSpeed;

    private void OnEnable()
    {
        obstacle = GetComponent<Obstacle>();
    }


    void Start()
    {
        player = transform;
        NumberOfClone = transform.childCount - 1;
        Countertxt.text = NumberOfClone.ToString();
        camera = Camera.main;
        gameState = false;

        


    }

    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();

        if (Input.GetMouseButtonDown(0))
        {
            gameState = true;

        }
        DestroyClone();



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
                MakeClone(NumberOfClone + gateManager.randomNumberIncrease -NumberOfClone);
            }
        }

       
    }

    private void MoveThePlayer()
    {
        if (Input.GetMouseButtonDown(0) && gameState)
        {
            Touch = true;

            Plane plane = new Plane(Vector3.up, 0f);
            var ray = camera.ScreenPointToRay(Input.mousePosition);

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
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                Vector3 mousePos = ray.GetPoint(distance + 1f);
                Vector3 move = mousePos - mouseStartPosition;
                Vector3 control = playerStartPosition + move;
                if (NumberOfClone > 150)
                {
                    control.x = Mathf.Clamp(control.x, -0.59f, 0.59f);
                }
                else if (NumberOfClone > 50)  //numberof clone büyük 50 ise hareket mesafesini kod satýrý kadar kýsýtla 
                {
                    control.x = Mathf.Clamp(control.x, -2.10f, 2.10f);
                }
                else if (NumberOfClone > 40)
                {
                    control.x = Mathf.Clamp(control.x, -2.96f, 2.96f);
                }
                else if (NumberOfClone > 30)
                {
                    control.x = Mathf.Clamp(control.x, -3.46f, 3.46f);
                }
                else if (NumberOfClone > 10)
                {
                    control.x = Mathf.Clamp(control.x, -3.9f, 3.9f);
                }
                else//deðilse bu eksende kýsýtla
                {
                    control.x = Mathf.Clamp(control.x, -5.30f, 5.30f);
                }


                transform.position = new Vector3(Mathf.Lerp(transform.position.x, control.x, Time.deltaTime * playerTouchSpeed), transform.position.y, transform.position.z);
            }
        }

        if (gameState)
        {
            player.Translate(player.forward * Time.deltaTime * playerMoveSpeed);
            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Animator>().SetBool("run", true);
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


}
