using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private int _iyiToplanabilirDeger;

    [SerializeField] private int _kötüToplanabilirDeger;

    [SerializeField] private GameObject _karakterPaketi;

    [SerializeField] private GameObject _featherObject;

    [SerializeField] private List<GameObject> _featherSpawnPoint = new List<GameObject>();

    [SerializeField] private GameObject _featherParent;

    [SerializeField] private GameObject _arkaDuvar;

    private int _elmasSayisi;

    private GameObject _player;

    private UIController _uiController;

    private int _toplananElmasSayisi;

    private int _spawnPointNumber;



    void Start()
    {
        LevelStart();

        _uiController = GameObject.Find("UIController").GetComponent<UIController>();

    }




    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Elmas")
        {
            _elmasSayisi += 1;
            _toplananElmasSayisi += 1;
            PlayerPrefs.SetInt("ElmasSayisi", _elmasSayisi);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "FeatherCollectable")
        {
            FeatherSpawn();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "DegersizEsya")
        {
            if (_featherParent.transform.childCount > 0)
            {
                _featherParent.transform.GetChild(_featherParent.transform.childCount - 1).parent = null;
                _arkaDuvar.SetActive(false);
                Invoke("DuvarAc", 0.5f);
                //_featherParent.transform.GetChild(_featherParent.transform.childCount - 1).gameObject.GetComponent<BoxCollider>().isTrigger = true;
            }
            else
            {

            }

        }
        else
        {

        }
    }

    private void DuvarAc()
    {
        _arkaDuvar.SetActive(true);
    }

    private void WinScreenAc()
    {
        _uiController.WinScreenPanelOpen();
    }

    private void LoseScreenAc()
    {
        _uiController.LoseScreenPanelOpen();
    }

    private void FeatherSpawn()
    {
        if (_spawnPointNumber == 0)
        {
            GameObject feather = Instantiate(_featherObject, _featherSpawnPoint[0].transform.position, Quaternion.identity);
            feather.transform.parent = _featherParent.transform;
            _spawnPointNumber = 1;
        }
        else if (_spawnPointNumber == 1)
        {
            GameObject feather = Instantiate(_featherObject, _featherSpawnPoint[1].transform.position, Quaternion.identity);
            feather.transform.parent = _featherParent.transform;
            _spawnPointNumber = 2;
        }
        else if (_spawnPointNumber == 2)
        {
            GameObject feather = Instantiate(_featherObject, _featherSpawnPoint[2].transform.position, Quaternion.identity);
            feather.transform.parent = _featherParent.transform;
            _spawnPointNumber = 0;
        }
        else
        {

        }
    }


    public void LevelStart()
    {
        _toplananElmasSayisi = 1;
        _elmasSayisi = PlayerPrefs.GetInt("ElmasSayisi");
        _karakterPaketi.transform.position = new Vector3(0, 0, 0);
        _karakterPaketi.transform.rotation = Quaternion.Euler(0, 0, 0);
        _player = GameObject.FindWithTag("Player");
        _player.transform.localPosition = new Vector3(0, 1, 0);
        _spawnPointNumber = 0;
        _arkaDuvar.SetActive(true);
    }



}
