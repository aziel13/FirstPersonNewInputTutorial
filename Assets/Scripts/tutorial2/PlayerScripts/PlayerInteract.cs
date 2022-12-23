using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class PlayerInteract : MonoBehaviour
{

    private Camera cam;
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask mask;
    private PlayerUI _playerUI;
    private InputManager2 _inputManager2;
    
    
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        _playerUI = GetComponent<PlayerUI>();
        _inputManager2 = GetComponent<InputManager2>();
    }

    private void Update()
    {
        _playerUI.UpdateText(string.Empty);
        
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin,ray.direction * distance);
        RaycastHit hitinfo;

        if (Physics.Raycast(ray, out hitinfo, distance, mask))
        {
            if (hitinfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitinfo.collider.GetComponent<Interactable>();
                
                _playerUI.UpdateText(interactable.promptMessage);
                if (_inputManager2.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }

    }
}