using System;
using UnityEngine;

public class MenuInGame : MonoBehaviour
{
    [SerializeField] private Canvas menuCanvas;

    private void Start()
    {
        menuCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuCanvas.enabled = !menuCanvas.enabled;
        }
    }
}
