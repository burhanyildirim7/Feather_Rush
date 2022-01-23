using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private GameObject Player;

    private GameObject _finishTakipObject;

    Vector3 aradakiFark;

    private bool _resetlendi;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        aradakiFark = transform.position - Player.transform.position;
        _resetlendi = true;
    }


    void FixedUpdate()
    {
        if (GameController._oyunAktif == false && _resetlendi == false)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(_finishTakipObject.transform.position.x, _finishTakipObject.transform.position.y, _finishTakipObject.transform.position.z - 9), Time.deltaTime * 5f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y + aradakiFark.y, Player.transform.position.z + aradakiFark.z), Time.deltaTime * 5f);
        }
    }

    public void FinishTakipObjesiBul()
    {
        _resetlendi = false;
        _finishTakipObject = GameObject.FindGameObjectWithTag("FinishTakipObjesi");
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = Vector3.Lerp(transform.position, new Vector3(_finishTakipObject.transform.position.x, _finishTakipObject.transform.position.y, _finishTakipObject.transform.position.z - 9), Time.deltaTime * 5f);
    }

    public void KameraResetle()
    {
        _resetlendi = true;
        transform.position = new Vector3(0, 7, -7);
        transform.rotation = Quaternion.Euler(30, 0, 0);
        Player = GameObject.FindGameObjectWithTag("Player");
        aradakiFark = transform.position - Player.transform.position;
    }

}
