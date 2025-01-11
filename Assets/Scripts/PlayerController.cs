using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private LayerMask _layerMask;

    public float speed;
    public float rotationSpeed;
    public Transform backPack;
    private float x = 0;
    public GameObject[] stairs;
    void Start()
    {
        _layerMask = LayerMask.GetMask("Ground");
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        RaycastHit hit;
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
        {
            transform.position = Vector3.Lerp(transform.position, hit.point, speed * Time.deltaTime);

            Vector3 dir = hit.point - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir),
                rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BlueBlock"))
        {
            Debug.Log("Block");
            Destroy(other.gameObject);
            MoveBlocksToBack(other.gameObject);
        }

        if (other.gameObject.CompareTag("Stair"))
        {
            if (backPack.transform.childCount <= 0)
            {
                return;
            }
            other.GetComponent<Renderer>().enabled = true;
            other.GetComponent<Renderer>().material.color = Color.blue;
            other.GetComponent<Collider>().enabled = false;
            int lastChildIndex = backPack.childCount -1;
            Destroy(backPack.transform.GetChild(lastChildIndex).gameObject);
        }
    }
    
    private void MoveBlocksToBack(GameObject block)
    {
        x = (backPack.transform.childCount -1) * .14f;
        Vector3 pos = backPack.position + new Vector3(0, x, 0);
        Instantiate(block, pos, transform.rotation, backPack);
        
    }
}
