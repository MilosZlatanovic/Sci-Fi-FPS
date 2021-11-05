using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 3.5f;
    private float _gravity = 9.81f;
    public bool lockCursor = true;
    [SerializeField]
    private GameObject _muzzleFlash;
    [SerializeField]
    private GameObject _hitMarkerPrefab;

    [SerializeField]
    private AudioSource _weaponAudio;
    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 50;
    private bool isReloading = false;

    private UIManager _uIManager;
    public bool hasCoin = false;
    [SerializeField]
    private GameObject _weapon;


    void Start()
    {
        _controller = GetComponent<CharacterController>();

        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uIManager._ammoText.text = "";

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _muzzleFlash.SetActive(false);
        isReloading = true;
    }

    void Update()
    {
        // Shoting
        if (Input.GetMouseButton(0) && currentAmmo > 0 && isReloading == false)
        {
            Shooting();
            AmmoCount();
        }
        else
        {
            _muzzleFlash.SetActive(false);
            _weaponAudio.Stop();
        }
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false)
        {
            isReloading = true;
            StartCoroutine(Reload());
        }
        // pressing esc toggles between hide/show
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            /* Cursor.visible = true;
             Cursor.lockState = CursorLockMode.None;*/
            Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !lockCursor;
        }
        /* Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
                Cursor.visible = !lockCursor;*/
        CalculateMovement();
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;

        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);

    }
    void Shooting()
    {
        _muzzleFlash.SetActive(true);
        currentAmmo--;

        Ray rayOrgin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (_weaponAudio.isPlaying == false)
        {
            _weaponAudio.Play();
        }

        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrgin, out hitInfo))
        {
          // Debug.Log("Hit: " + hitInfo.transform.name);
            GameObject hits = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(hits, 1f);

            Destructable destructable = hitInfo.transform.GetComponent<Destructable>();
            if (destructable != null)
            {
                destructable.DestroyCrate();
            }
        }

    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);

        currentAmmo = maxAmmo;
        AmmoCount();
        isReloading = false;

    }
    public void AmmoCount()
    {
        _uIManager.UpdateAmmo(currentAmmo);
    }
    public void CollectedWeapon()
    {
        _weapon.SetActive(true);
        currentAmmo = maxAmmo;
        AmmoCount();
        isReloading = false;
    }
}
