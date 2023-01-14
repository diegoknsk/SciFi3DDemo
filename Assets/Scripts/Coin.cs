using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip _coinPcikUp;


    // Start is called before the first frame update
 

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
                if (player != null)
                {
                    player.hasCoin = true;
                    AudioSource.PlayClipAtPoint(_coinPcikUp, Camera.main.transform.position, 1f);
                    UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                    if (uiManager != null)
                    {
                        uiManager.CollectCoin();
                    }
                    Destroy(this.gameObject);
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
