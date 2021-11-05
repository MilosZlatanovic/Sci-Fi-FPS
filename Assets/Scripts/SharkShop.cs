using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    [SerializeField]
    private AudioClip _WeaponPickUp;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    if (player.hasCoin == true)
                    {
                        UIManager uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                        if (uIManager != null)
                        {
                            uIManager.RemoveCoin();
                        }
                        AudioSource.PlayClipAtPoint(_WeaponPickUp, transform.position, 1f);
                        player.CollectedWeapon();
                        player.hasCoin = false;
                    }
                    else
                    {
                        Debug.Log("Get out of here!");
                    }


                }
            }
        }
    }
}
