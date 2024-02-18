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
        situation.text = "Oyunumuzun ana karakteri Ömer Halis'tir. Ömer Halis bir MÝT ajanýdýr ve devletimiz için kýymeti bulunan bir dosyayý vatan topraklarýna ulaþtýrmasý gerekmektedir.";
    }

    public void UpdateText2()
    {
        situation.text = "Oyunda sizlere yol göstermesi için kan izleri býrakýlmýþtýr. Bu kan izleri hikayeyle baðlantýlýdýr.";
        btn.gameObject.SetActive(true);
    }

    public void SkipCinematic()
    {
        SceneManager.LoadScene(2);
    }

    public void FixedUpdate()
    {
        situation.text = "Bu oyun Alperen KILIÇ tarafýndan bir savunma sanayi oyunu için kurgulanmýþ ve geliþtirilmiþtir. Tüm Þehitlerimize Allah'tan rahmet yakýnlarýna baþsaðlýðý dileriz.";
        Invoke("UpdateText", 5f);
        Invoke("UpdateText2", 12f);
        


    }

}
