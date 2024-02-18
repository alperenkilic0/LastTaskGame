using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [Header("Plane Stats")]
    [Tooltip("How much the throttle ramps up or down.")]
    public float throttleIncrement = 0.1f;
    [Tooltip("Maximum Engine thrust when at 100% throttle")]
    public float maxThrust = 200f;
    [Tooltip("How responsive the plane is when rolling, pitching and yawing.")]
    public float responsiveness = 10f;

    private float throttle;
    private float roll;
    private float yaw;
    private float pitch;

    public GameObject pervane1;
    public GameObject pervane2;

    public GameObject bombPrefab; // Bomba prefabý
    public GameObject secondBombPrefab;

    public Transform bombSpawnPoint;
    public Transform bombSpawnPoint2;

    public float dropForce = 500f; // Düþme kuvvetini istediðiniz gibi ayarlayabilirsiniz
    public float bombMass = 2000f;
    public float bombVelocity = 2.25f;

    private bool isFirstBombDropped = false;


    public float rotationSpeed = 1000f; // Hýz 2 katýna çýkarýldý
    private float responseModifier
    {
        get
        {
            return (rb.mass / 10f)*responsiveness ;
        }
    }

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void HandleInputs () 
    {
        roll = Input.GetAxis("Vertical");
        pitch = Input.GetAxis("Horizontal");
        yaw = Input.GetAxis("Yaw");

        if (Input.GetKey(KeyCode.Space)) throttle += throttleIncrement;
        else if (Input.GetKey(KeyCode.LeftControl)) throttle -= throttleIncrement;
        throttle = Mathf.Clamp(throttle, 0, 100f);
    }

    private void Update () 
    {
    HandleInputs();

        if (Input.GetKey(KeyCode.Space))
        {

            pervane1.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
            pervane2.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isFirstBombDropped)
            {
                DropBomb();
                isFirstBombDropped = true;
            }
            else
            {
                DropSecondBomb();
            }
        }

    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.right * maxThrust * throttle*-1);
        rb.AddTorque(transform.up* yaw* responseModifier);
        rb.AddTorque(transform.right * pitch * responseModifier);
        rb.AddTorque(transform.forward * roll * responseModifier);
    }

    void DropBomb()
    {
        // Bomba prefabýndan bir kopya oluþtur
        GameObject newBomb = Instantiate(bombPrefab, bombSpawnPoint.position, Quaternion.identity);

        // Oluþturulan bombanýn parent'ýný belirle (Opsiyonel)
        Debug.Log("Rye basýyorsun");

        

        float scaleFactor = 75f; // Scale faktörünü istediðiniz gibi ayarlayabilirsiniz
        newBomb.transform.localScale *= scaleFactor;


        Rigidbody rbBomb = newBomb.GetComponent<Rigidbody>();
        CopyComponents(bombPrefab, newBomb);
        if (rbBomb == null)
        {
            rbBomb = newBomb.AddComponent<Rigidbody>();
        }
        bombMass = 2000f;
        newBomb.transform.rotation = bombPrefab.transform.rotation;
        rbBomb.useGravity = true; // Yer çekimini aktifleþtir
        rbBomb.mass = bombMass;
        rbBomb.velocity = rb.velocity*bombVelocity;
        rbBomb.angularVelocity = rb.angularVelocity;


        rbBomb.AddForce(Vector3.down * dropForce, ForceMode.Impulse);
        Destroy(bombPrefab);
    }

    void DropSecondBomb()
    {
        // Ýkinci bombayý at
        GameObject newBomb2 = Instantiate(secondBombPrefab, bombSpawnPoint2.position, Quaternion.identity);
        // Bombanýn diðer özelliklerini ayarla (örneðin boyut, kütle, hýz vs.)
        Rigidbody rbBomb2 = newBomb2.GetComponent<Rigidbody>();
        float scaleFactor = 75f; // Scale faktörünü istediðiniz gibi ayarlayabilirsiniz
        newBomb2.transform.localScale *= scaleFactor;
        CopyComponents(secondBombPrefab, newBomb2);
        if (rbBomb2 == null)
        {
            rbBomb2 = newBomb2.AddComponent<Rigidbody>();
        }
        newBomb2.transform.rotation = secondBombPrefab.transform.rotation;
        rbBomb2.useGravity = true;
        rbBomb2.mass = bombMass;
        rbBomb2.velocity = rb.velocity*bombVelocity;
        rbBomb2.angularVelocity = rb.angularVelocity;
        rbBomb2.AddForce(Vector3.down * dropForce, ForceMode.Impulse);

        Destroy(secondBombPrefab);
    }

    void CopyComponents(GameObject original, GameObject clone)
    {
        // Orijinal bombanýn tüm bileþenlerini al
        Component[] components = original.GetComponents<Component>();

        // Her bileþeni klonlanmýþ bombaya ekle
        foreach (Component component in components)
        {
            if (!(component is Transform))
            {
                // Transform bileþenini hariç tut
                UnityEngine.Component copy = clone.AddComponent(component.GetType());
                System.Reflection.FieldInfo[] fields = component.GetType().GetFields();
                foreach (System.Reflection.FieldInfo field in fields)
                {
                    field.SetValue(copy, field.GetValue(component));
                }
            }
        }
    }

    }
