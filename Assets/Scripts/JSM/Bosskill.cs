using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosskill : MonoBehaviour
{
    public GameObject fish;
    public GameObject portal;
    [SerializeField] private bool boss = false;
    public void Update()
    {
        if (fish != null && boss)
        {
            portal.gameObject.SetActive(true);
        }
    }
}
