using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string areaTransitionName;

    private void Start()
    {
        // adjust camera
        PlayerMovementController.instance.PlayerInit();

        if (areaTransitionName == PlayerMovementController.instance.areaTransitionName)
        {
            PlayerMovementController.instance.transform.position = transform.position;
        }
    }
}
