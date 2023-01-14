using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    //private CharacterController _characterController;
    private NavMeshAgent _navMeshAgent;
    [SerializeField]
    private float _speed = 3.5f;
    private float _gravity = 9.81f;
    [SerializeField]
    private GameObject _muzzleFlash;
    [SerializeField]
    private GameObject _hitMarkerPrefab;
    [SerializeField]
    private AudioSource _weaponAudio;

    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 50;

    private bool _isReloading = false;
    private UIManager _uiManager;

    public bool hasCoin = false;

    [SerializeField]
    private GameObject _weapon;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //_characterController = GetComponent<CharacterController>();
        currentAmmo = maxAmmo;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && currentAmmo > 0 && _weapon.activeSelf)
        {
            Shoot();         
        }
        else
        {
            _muzzleFlash.SetActive(false);
            _weaponAudio.Stop();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _isReloading = true;
            StartCoroutine(Reload());            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        MovementCharacter();
    }

    private void Shoot()
    {
        _muzzleFlash.SetActive(true);
        currentAmmo--;
        _uiManager.UpdateAmmo(currentAmmo);
        if (!_weaponAudio.isPlaying)
        {
            _weaponAudio.Play();
        }
        //Ray rayOrigin = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Debug.Log("hit : " + hitInfo.transform.name);
            var hitMarker = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(hitMarker, 1f);

            DestructableBehaviour crate = hitInfo.transform.GetComponent<DestructableBehaviour>();
            if (crate != null)
            {
                crate.DestroyCrate();
            }
        }
    }

    private void MovementCharacter()
    {
        Vector3 velocity = CalculateMoviment();
        velocity = transform.transform.TransformDirection(velocity);  // convertendo local space para word space
        //_characterController.Move(velocity * Time.deltaTime);
        _navMeshAgent.Move(velocity * Time.deltaTime);
    }

    private Vector3 CalculateMoviment()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        var direction = new Vector3(horizontalInput, 0, verticalInput); //0 no Y pq o boneco não vai pular
        var velocity = direction * _speed;
        velocity.y -= _gravity; // aplicando gravidade ao boneco
        return velocity;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);
        _isReloading = false;
    }

    public void EnableWeapons()
    {
        _weapon.SetActive(true);
    }
}
