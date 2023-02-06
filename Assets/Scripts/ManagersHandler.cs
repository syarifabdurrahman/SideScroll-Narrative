using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersHandler : MonoBehaviour
{
    public static ManagersHandler Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
