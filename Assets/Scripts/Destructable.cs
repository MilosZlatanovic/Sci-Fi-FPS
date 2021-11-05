using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField]
    private GameObject _crateDestroy;

    public void DestroyCrate()
    {
        Instantiate(_crateDestroy, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
