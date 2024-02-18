using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Movement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    public Text zaman, situation, theyareafter;
    public Button btn;
    public Button btn2;
    public Button btn3;
    public GameObject painting;


    bool gameResume = true;
    bool gameEnd = false;

    float zamanSayaci = 100;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb= GetComponent<Rigidbody>();
        


    }

    // Update is called once per frame
    void Update()
    {
        if(gameResume && !gameEnd)
        {
            zamanSayaci -= Time.deltaTime;
            zaman.text = (int)zamanSayaci + "";

        }
        else if(!gameEnd)
        {
            situation.text = "Yakalandýn. Tekrar Dene..";
            btn.gameObject.SetActive(true);
        }
        

        if(zamanSayaci<=0)
        {
            gameResume = false;
        }


        
    }

    private void FixedUpdate()
    {
        if ((gameResume && !gameEnd))
        {
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("isWalking", true);
                transform.Translate(new Vector3(0, 0, 10f) * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                animator.SetBool("isRunning", true);
                transform.Translate(new Vector3(0, 0, 16f) * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
            {
                animator.SetBool("isWalking", true);
                transform.Rotate(0, -60 * Time.deltaTime, 0);
                transform.Translate(new Vector3(0, 0, 10f) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
            {
                animator.SetBool("isWalking", true);
                transform.Rotate(0, 60 * Time.deltaTime, 0);
                transform.Translate(new Vector3(0, 0, 10f) * Time.deltaTime);
            }

        }
        else 
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);

        }

    }
    public void tryAgain()
    {
        SceneManager.LoadScene(3);
    }

    public void continueLevel()
    {
        SceneManager.LoadScene(4);
    }

    private void OnCollisionEnter(Collision other)
    {
        string objIsmi =other.gameObject.name;
        if (objIsmi.Equals("FinishPoint"))
        {
            print("You Won");
            gameEnd = true;
            situation.text = "UH..! Piramitten kaçmayý baþardýn. Þimdi takviye kuvvetler yolda, 'Akýncýlar' geliyor.";
            btn2.gameObject.SetActive(true);
        }

        if (objIsmi.Equals("FirstFinishPoint"))
        {
            print("You Won2");
            gameEnd = true;
            situation.text = "Piramide giden gizli bir geçit buldun. Takip etmelisin.";
            btn2.gameObject.SetActive(true);
        }

        if (objIsmi.Equals("FrontOfTable"))
        {
            print("Front Of Table");
            situation.text = "Gizli dosyayý buldun. Bunu vatan topraklarýna götürmen gerekiyor. Acele et.";
            btn3.gameObject.SetActive(true);
            
        }
    }
    public void ChangeSc()
    {
        SceneManager.LoadScene(3);
        print("You Won2");
        print("oldu");
        Debug.Log("ChangeSc fonksiyonu çaðrýldý!");
    }

    public void takeDir()
    {
        Destroy(painting);
        situation.text = "";
        btn3.gameObject.SetActive(false);
        theyareafter.text = "GÝZLÝ GEÇÝDÝ BULMAN GEREK!";
    }
}
