using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShopBehaviour : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
                if (player.hasCoin)
                {
                    player.hasCoin = false;
                    UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                    if (uiManager != null)
                    {
                        uiManager.RemoveCoin();
                    }

                    AudioSource audio = GetComponent<AudioSource>();
                    audio.Play();
                    player.EnableWeapons();
                }
                else
                {
                    Debug.Log("Get out of here!");
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
