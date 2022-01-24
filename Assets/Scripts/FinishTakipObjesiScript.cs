using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTakipObjesiScript : MonoBehaviour
{
    private void Start()
    {
        transform.localPosition = new Vector3(0.15f, 0, 0);
    }

    void Update()
    {
        if (GameController._finishTakip == true && GameController._oyunAktif == false)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 3f);
        }
    }
}
