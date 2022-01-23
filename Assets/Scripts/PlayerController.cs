using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using DG.Tweening;

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
            if (GameController._oyunAktif == true)
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
                    GameController._oyunAktif = false;
                    Invoke("LoseScreenAc", 1f);
                }
            }
            else
            {

            }


        }
        else if (other.gameObject.tag == "FinishCizgisi")
        {
            _player.transform.localPosition = new Vector3(0, 1, 0);
            GameController._oyunAktif = false;

            StartCoroutine("FeatherYerlestirme", 1);

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

    private IEnumerator FeatherYerlestirme()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>().FinishTakipObjesiBul();
        yield return new WaitForSeconds(1f);
        GameController._finishTakip = true;

        for (int i = 0; i < _featherParent.transform.childCount; i++)
        {
            if (i == _featherParent.transform.childCount - 1)
            {
                GameController._finishTakip = false;
                yield return new WaitForSeconds(0.2f);
                _featherParent.transform.GetChild(_featherParent.transform.childCount - i - 1).gameObject.transform.DOMove(GameObject.FindGameObjectWithTag("FinishFeatherNoktalari").GetComponent<FinishFeatherNoktalari>()._finishFeatherNoktalari[i].transform.position, 1f);
                _featherParent.transform.GetChild(_featherParent.transform.childCount - i - 1).gameObject.GetComponent<Rigidbody>().isKinematic = true;
                _featherParent.transform.GetChild(_featherParent.transform.childCount - i - 1).gameObject.transform.rotation = Quaternion.Euler(90f, 0, 0);
                Invoke("WinScreenAc", 2f);
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
                _featherParent.transform.GetChild(_featherParent.transform.childCount - i - 1).gameObject.transform.DOMove(GameObject.FindGameObjectWithTag("FinishFeatherNoktalari").GetComponent<FinishFeatherNoktalari>()._finishFeatherNoktalari[i].transform.position, 1f);
                _featherParent.transform.GetChild(_featherParent.transform.childCount - i - 1).gameObject.GetComponent<Rigidbody>().isKinematic = true;
                _featherParent.transform.GetChild(_featherParent.transform.childCount - i - 1).gameObject.transform.rotation = Quaternion.Euler(90f, 0, 0);
            }

        }


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

    private void FeatherTemizle()
    {
        for (int i = 0; i < _featherParent.transform.childCount; i++)
        {
            Destroy(_featherParent.transform.GetChild(i).gameObject);

        }
    }

    public void LevelStart()
    {

        FeatherTemizle();
        _toplananElmasSayisi = 1;
        _elmasSayisi = PlayerPrefs.GetInt("ElmasSayisi");
        _karakterPaketi.transform.position = new Vector3(0, 0, 0);
        _karakterPaketi.transform.rotation = Quaternion.Euler(0, 0, 0);
        _player = GameObject.FindWithTag("Player");
        _player.transform.localPosition = new Vector3(0, 1, 0);
        _spawnPointNumber = 0;
        _arkaDuvar.SetActive(true);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>().KameraResetle();
    }



}
