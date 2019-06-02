using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject exitPortal;

    public Vector3 ExitPortal
    {
        get => new Vector3 
            (exitPortal.transform.position.x, 
            exitPortal.transform.position.y, 
            exitPortal.transform.position.z);

        private set { }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            exitPortal.GetComponent<Animator>().Play("Exit");
            GetComponent<Animator>().Play("Triggered");
        }

    }
}