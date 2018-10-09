using UnityEngine;

public class Jugador : MonoBehaviour {

    public float velocidadNave = 1.5f;
    public float limiteHorizontal = 2.5f;
    public float velocidadDisparo = 3f;
    public float cooldownDisparo = 1f;
    public GameObject misilPrefab;
    public GameObject explosionPrefab;

    private float cooldownTiempo;

	// Use this for initialization
	void Start () {
		
	}

	void Update ()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal") * velocidadNave ,0);

        if (transform.position.x > limiteHorizontal)
        {
            transform.position = new Vector3(limiteHorizontal, transform.position.y, transform.position.z);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (transform.position.x < -limiteHorizontal)
        {
            transform.position = new Vector3(-limiteHorizontal, transform.position.y, transform.position.z);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        cooldownTiempo -= Time.deltaTime;
        if (cooldownTiempo <= 0 && Input.GetAxis("Fire1") == 1f)
        {
            cooldownTiempo = cooldownDisparo;

            GameObject misilInstancia = Instantiate(misilPrefab);
            misilInstancia.transform.SetParent(transform.parent);
            misilInstancia.transform.position = transform.position;
            misilInstancia.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocidadDisparo);
            Destroy(misilInstancia, 5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemigoMisil" || collision.tag == "Enemigo")
        {
            GameObject explosionInstancia = Instantiate(explosionPrefab);
            explosionInstancia.transform.SetParent(transform.parent);
            explosionInstancia.transform.position = transform.position;

            Destroy(explosionInstancia, 1.5f);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
