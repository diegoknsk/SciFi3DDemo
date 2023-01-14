using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private GameObject _coin;
    // Start is called before the first frame update

    public void UpdateAmmo(int count)
    {
        _ammoText.text = "Munição: " + count;
    }

    public void CollectCoin()
    {
        _coin.SetActive(true);
    }

    public void RemoveCoin()
    {
        _coin.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
