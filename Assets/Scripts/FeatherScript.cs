using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherScript : MonoBehaviour
{
    [SerializeField] private List<Material> _materials = new List<Material>();

    private Renderer _material;

    void Start()
    {
        _material = GetComponent<Renderer>();
        _material.material = _materials[0];
    }

    private void Update()
    {
        if (transform.parent == null)
        {
            Destroy(gameObject, 2f);
        }
        else
        {

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BlueDoor")
        {
            //Debug.Log("Mavi Degdi");
            _material.material = _materials[1];

        }
        else if (other.gameObject.tag == "GreenDoor")
        {
            //Debug.Log("Yesil Degdi");
            _material.material = _materials[2];

        }
        else if (other.gameObject.tag == "RedDoor")
        {
            //Debug.Log("Kirmizi Degdi");
            _material.material = _materials[3];

        }
        else if (other.gameObject.tag == "YellowDoor")
        {
            //Debug.Log("Sari Degdi");
            _material.material = _materials[4];

        }
        else
        {

        }
    }
}
