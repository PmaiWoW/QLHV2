using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // Colliders
    [SerializeField] private BoxCollider2D upCol, downCol, leftCol, rightCol;
    [SerializeField] private BoxCollider2D centerCol;
    private Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        EnableDisableColliders(false);
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            EnableDisableColliders(col.GetComponent<Player>().Direction);
            //anim.Play("Standing");
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player")
            StartCoroutine(DeactivateColsAfter());
    }

    // Disable helping colliders
    private void EnableDisableColliders(bool enabled)
    {
        upCol.enabled = enabled;
        downCol.enabled = enabled;
        leftCol.enabled = enabled;
        rightCol.enabled = enabled;
    }

    private void EnableDisableColliders(string direction)
    {
        switch (direction)
        {
            case "Up":
                upCol.enabled = true;
                break;
            case "Down":
                downCol.enabled = true;
                break;
            case "Right":
                rightCol.enabled = true;
                break;
            case "Left":
                leftCol.enabled = true;
                break;
        }
    }

    // Wait coliders to complete
    public IEnumerator DeactivateColsAfter()
    {
        yield return new WaitForSeconds(0.1f);
        EnableDisableColliders(false);
        // To do after
    }
}
