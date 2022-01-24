using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTakipObjesiScript : MonoBehaviour
{

    [SerializeField] private GameObject _confetti1;
    [SerializeField] private GameObject _confetti2;

    private void Start()
    {
        _confetti1.SetActive(false);
        _confetti2.SetActive(false);

        transform.localPosition = new Vector3(0.15f, 0, 0);
    }

    void Update()
    {
        if (GameController._finishTakip == true && GameController._oyunAktif == false)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 3f);
        }
    }

    public void ConfettiPatlat()
    {
        _confetti1.SetActive(true);
        _confetti2.SetActive(true);
    }
}
