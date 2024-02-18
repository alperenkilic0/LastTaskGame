using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BombController : MonoBehaviour
{
    public GameObject explosionEffect; // Patlama efekti
    public GameObject bomb;
    public Button btn;
    public string targetTag = "Pyramid"; // Hedef nesne etiketi
    public Text situation;

    bool gameResume = true;
    bool gameEnd = false;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        // Bomba bir nesneye çarptýðýnda
        if (collision.gameObject.CompareTag(targetTag))
        {
            situation.text = "Baþarýyla Oyunu Tamamladýn Ömer Halis. Bu vatan sana minettardýr..";
            Destroy(bomb);
            gameEnd = true;
            btn.gameObject.SetActive(true);
        }

        // Patlama efektini oluþtur ve bombayý yok et
        
        Destroy(bomb);
        GetComponent<Rigidbody>().detectCollisions = false;
        
    }

    public void startfrommain()
    {
        SceneManager.LoadScene(0);
    }
}
