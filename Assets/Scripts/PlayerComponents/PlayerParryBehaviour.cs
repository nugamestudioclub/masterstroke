using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryBehaviour : MonoBehaviour
{
    private PlayerController _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<PlayerController>();
        Destroy(GetComponent<PlayerParryStanceBehaviour>());
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
