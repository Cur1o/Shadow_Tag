using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Impact : MonoBehaviour
{
    [SerializeField] private VisualEffect hit;
    [SerializeField] private float livetime = 4;  
    // Start is called before the first frame update
    void Start()
    {
        hit.Play();
        StartCoroutine(Die());
    }
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(livetime);
        Destroy(gameObject);
    }
}
