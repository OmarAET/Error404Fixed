using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class takeDamage : MonoBehaviour
{
    public Text Text;
    public Text Text1;
    public int playerHealth = 100;
    int damage = 10;
    int fishcount = 0;
    private void Start()
    {
        print(playerHealth);
        Text.text = "Health";
        Text1.text = "FishCount";
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dmg")
        {
            playerHealth -= damage;
            Debug.Log("Damage Taken");
        }
        if (playerHealth == 0)
        {
            SceneManager.LoadScene(0);
        }
        if (other.tag == "Fish")
        {
            fishcount += 1;
            Destroy(other.gameObject);
        }
    }
    void Update()
    {
        Text1.text = "Fishs: " + fishcount;
        Text.text = "Health: " + playerHealth.ToString();

        if (playerHealth <= 0)
        {

        }
            //Destroy(gameObject);
    }
}
