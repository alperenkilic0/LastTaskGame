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
        // Bomba bir nesneye �arpt���nda
        if (collision.gameObject.CompareTag(targetTag))
        {
            situation.text = "Ba�ar�yla Oyunu Tamamlad�n �mer Halis. Bu vatan sana minettard�r..";
            Destroy(bomb);
            gameEnd = true;
            btn.gameObject.SetActive(true);
        }

        // Patlama efektini olu�tur ve bombay� yok et
        
        Destroy(bomb);
        GetComponent<Rigidbody>().detectCollisions = false;
        
    }

    public void startfrommain()
    {
        SceneManager.LoadScene(0);
    }
}
