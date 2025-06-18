using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosskill : MonoBehaviour
{
    public GameObject fish;
    public GameObject portal;
    public void Update()
    {
        if (fish == null)
        {
            portal.gameObject.SetActive(true);
        }
    }
}
