using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class takeDamage : MonoBehaviour
{
    public Text Text;
    public int playerHealth = 100;
    int damage = 10;
    private void Start()
    {
        print(playerHealth);
        Text.text = "Health";
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
            SceneManager.LoadScene("0");
        }
    }
    void Update()
    {
        Text.text = "Health: " + playerHealth.ToString();

        if (playerHealth <= 0)
        {

        }
            //Destroy(gameObject);
    }
}
