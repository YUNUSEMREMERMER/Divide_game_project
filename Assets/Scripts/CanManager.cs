using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanManager : MonoBehaviour
{
    [SerializeField]
    private GameObject kalanCan1, kalanCan2, kalanCan3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void kalanCanlarýKontrolEt(int kalanCan)
    {
        switch (kalanCan)
        {
            case 3:
                kalanCan1.SetActive(true);
                kalanCan2.SetActive(true);
                kalanCan3.SetActive(true);
                break;

            case 2:
                kalanCan1.SetActive(true);
                kalanCan2.SetActive(true);
                kalanCan3.SetActive(false);
                break;

            case 1:
                kalanCan1.SetActive(true);
                kalanCan2.SetActive(false);
                kalanCan3.SetActive(false);
                break;

            case 0:
                kalanCan1.SetActive(false);
                kalanCan2.SetActive(false);
                kalanCan3.SetActive(false);
                break;

            default:
                break;
        }

    }
}
