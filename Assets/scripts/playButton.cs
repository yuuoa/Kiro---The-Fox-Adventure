using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playButton : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButton("Submit"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
