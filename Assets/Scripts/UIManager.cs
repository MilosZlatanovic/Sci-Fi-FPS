using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public Text _ammoText;
    [SerializeField]
    private GameObject _coin;
    public void UpdateAmmo(int _currentAmmo )
    {
        _ammoText.text = "AMMO: " + _currentAmmo;
        
    }

    public void CollectedCoin()
    {
        _coin.SetActive(true);
    }
    public void RemoveCoin()
    {
        _coin.SetActive(false);
    }
}

