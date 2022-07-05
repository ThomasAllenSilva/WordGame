using System.Collections;
using UnityEngine;

public class Deletar : MonoBehaviour
{
   public GameObject[] kk;


    private void Start()
    {
        StartCoroutine(Enable());
    }
    private IEnumerator Enable()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < kk.Length; i++)
        {
            kk[i].SetActive(true);
        }
    }
}
