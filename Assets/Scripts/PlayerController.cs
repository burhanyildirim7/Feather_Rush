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

    [SerializeField] private ParticleSystem _featherParticle;

    private int _elmasSayisi;

    private GameObject _player;

    private UIController _uiController;

    private int _toplananElmasSayisi;

    private int _spawnPointNumber;

    private int elmassayisi;



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
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            FeatherSpawn();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "DegersizEsya")
        {

            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);


            if (GameController._oyunAktif == true)
            {
                _featherParticle.Play();

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
                    _uiController.LevelSonuElmasSayisi(0);
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

            int tuysayisi = _featherParent.transform.childCount;
            int xdegeri = (int)(tuysayisi / 6);

            elmassayisi = tuysayisi * xdegeri;

            _uiController.LevelSonuElmasSayisi(elmassayisi);

            StartCoroutine("FeatherYerlestirme", 1);

        }
        else if (other.gameObject.tag == "BlueDoor")
        {

            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            //Debug.Log("Mavi Degdi");
            //_material.material = _materials[1];

        }
        else if (other.gameObject.tag == "GreenDoor")
        {

            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            //Debug.Log("Yesil Degdi");
            //_material.material = _materials[2];

        }
        else if (other.gameObject.tag == "RedDoor")
        {

            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            //Debug.Log("Kirmizi Degdi");
            //_material.material = _materials[3];

        }
        else if (other.gameObject.tag == "YellowDoor")
        {

            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            //Debug.Log("Sari Degdi");
            //_material.material = _materials[4];

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
                _featherParent.transform.GetChild(_featherParent.transform.childCount - i - 1).gameObject.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                _featherParent.transform.GetChild(_featherParent.transform.childCount - i - 1).gameObject.transform.DOMove(GameObject.FindGameObjectWithTag("FinishFeatherNoktalari").GetComponent<FinishFeatherNoktalari>()._finishFeatherNoktalari[i].transform.position, 1f);
                _featherParent.transform.GetChild(_featherParent.transform.childCount - i - 1).gameObject.GetComponent<Rigidbody>().isKinematic = true;
                _featherParent.transform.GetChild(_featherParent.transform.childCount - i - 1).gameObject.transform.rotation = Quaternion.Euler(90f, 0, 0);
                GameObject.FindGameObjectWithTag("FinishTakipObjesi").GetComponent<FinishTakipObjesiScript>().ConfettiPatlat();
                Invoke("WinScreenAc", 2f);
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
                _featherParent.transform.GetChild(_featherParent.transform.childCount - i - 1).gameObject.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
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
        elmassayisi = 0;
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
