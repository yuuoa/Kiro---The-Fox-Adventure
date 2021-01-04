using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogTrigger : MonoBehaviour
{
    public dialog dialogs;

    private Collider2D collision;

    private void Start() {
        collision = GetComponent<Collider2D>();
    }

    public void TriggerDialog ()
    {
        OnTriggerEnter2D(collision);
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "dialog")
        {
            FindObjectOfType<dialogManager>().startDialog(dialogs);
        }
    }
}
