using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CinematicController : MonoBehaviour
{
    public Text situation;
    public Button btn;

    public void UpdateText()
    {
        situation.text = "Oyunumuzun ana karakteri �mer Halis'tir. �mer Halis bir M�T ajan�d�r ve devletimiz i�in k�ymeti bulunan bir dosyay� vatan topraklar�na ula�t�rmas� gerekmektedir.";
    }

    public void UpdateText2()
    {
        situation.text = "Oyunda sizlere yol g�stermesi i�in kan izleri b�rak�lm��t�r. Bu kan izleri hikayeyle ba�lant�l�d�r.";
        btn.gameObject.SetActive(true);
    }

    public void SkipCinematic()
    {
        SceneManager.LoadScene(2);
    }

    public void FixedUpdate()
    {
        situation.text = "Bu oyun Alperen KILI� taraf�ndan bir savunma sanayi oyunu i�in kurgulanm�� ve geli�tirilmi�tir. T�m �ehitlerimize Allah'tan rahmet yak�nlar�na ba�sa�l��� dileriz.";
        Invoke("UpdateText", 5f);
        Invoke("UpdateText2", 12f);
        


    }

}
