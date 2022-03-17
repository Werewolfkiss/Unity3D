using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    private MeshRenderer component;
    Color originalColor;

    private void Start()
    {
        component = GetComponent<MeshRenderer>();
        originalColor = component.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            component.material.color = new Color(1f, 0.25f, 0.75f);
        }
    }

    private async void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            await Task.Delay(500);
            var component = GetComponent<MeshRenderer>();
            component.material.color = originalColor;
        }
    }
}
