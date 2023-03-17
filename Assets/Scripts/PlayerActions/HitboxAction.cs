using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class HitboxAction : PlayerAction
{

    public Vector3 clickPos;
    [SerializeField]
    protected Vector2 direction;
    //The parent object of the attack (the attack prefab)
    [SerializeField] 
    private GameObject attackPrefab;
    private GameObject localAttackPrefab;

    [SerializeField]
    float _duration = .10f;

    public override void OnStart()
    {
        // Debug.Log("Hitbox Activated");
        clickPos = player.transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        float angle = Mathf.Atan2(clickPos.y, clickPos.x) * Mathf.Rad2Deg;

        localAttackPrefab = UnityEngine.Object.Instantiate(attackPrefab, player.transform, false);
        //set the parent object's rotation to the angle that the player is facing the mouse
        localAttackPrefab.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public override IEnumerator Apply()
    {
        yield return new WaitForSeconds(_duration);
    }

    public override void OnFinish()
    {
        UnityEngine.Object.Destroy(localAttackPrefab.gameObject);
    }
}
