using UnityEngine;

public class Enemigo : MonoBehaviour {

    public GameObject explosionPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "JugadorMisil")
        {
            GameObject explosionInstancia = Instantiate(explosionPrefab);
            explosionInstancia.transform.SetParent(transform.parent.parent);
            explosionInstancia.transform.position = transform.position;

            Destroy(explosionInstancia, 1.5f);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
