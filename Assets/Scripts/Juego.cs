using UnityEngine;
using UnityEngine.SceneManagement;

public class Juego : MonoBehaviour {

    public float enemigoDisparoIntervalo = 3f;
    public float enemigoVelocidadDisparo = 2f;
    public GameObject enemigoMisilPrefab;
    public GameObject enemigosCaja;
    public Jugador jugador;
    public float enemigosMaximaVelocidadMovimiento = 0.5f;
    public float enemigosMinimaVelocidadMovimiento = 0.08f;
    public float enemigosDistanciaMovimiento = 0.1f;
    public float limiteHorizontal = 2.5f;

    private float disparoContador;
    private float movimientoContador;
    private float movimientoDireccion = 1f;
    private float movimientoIntervalo;
    private int enemigosContador;

    // Use this for initialization
    void Start ()
    {    
        disparoContador = enemigoDisparoIntervalo;
        movimientoIntervalo = enemigosMaximaVelocidadMovimiento;
        enemigosContador = GetComponentsInChildren<Enemigo>().Length;
    }
	
	// Update is called once per frame
	void Update ()
    {
        int enemigosActual = GetComponentsInChildren<Enemigo>().Length;

        disparoContador -= Time.deltaTime;

        if (enemigosActual > 0 && disparoContador <= 0f)
        {
            disparoContador = enemigoDisparoIntervalo;

            Enemigo[] enemigos = GetComponentsInChildren<Enemigo>();
            Enemigo enemigoAzar = enemigos[Random.Range(0, enemigos.Length)];

            GameObject misilInstancia = Instantiate(enemigoMisilPrefab);
            misilInstancia.transform.SetParent(transform.parent);
            misilInstancia.transform.position = enemigoAzar.transform.position;
            misilInstancia.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemigoVelocidadDisparo);
            Destroy(misilInstancia, 3f);
        }

        movimientoContador -= Time.deltaTime;

        if (movimientoContador <= 0f)
        {
            float dificultad = 1f - (float) enemigosActual / enemigosContador;

            movimientoIntervalo = enemigosMaximaVelocidadMovimiento - (enemigosMaximaVelocidadMovimiento - enemigosMinimaVelocidadMovimiento) * dificultad;
            movimientoContador = movimientoIntervalo;

            enemigosCaja.transform.position = new Vector2(
                enemigosCaja.transform.position.x + (enemigosDistanciaMovimiento * movimientoDireccion),
                enemigosCaja.transform.position.y);

            if (movimientoDireccion > 0)
            {
                float enemigoDerecha = 0f;

                foreach (Enemigo enemigo in GetComponentsInChildren<Enemigo>())
                {
                    if (enemigo.transform.position.x > enemigoDerecha)
                    {
                        enemigoDerecha = enemigo.transform.position.x;
                    }
                }

                if (enemigoDerecha > limiteHorizontal)
                {
                    movimientoDireccion *= -1;

                    enemigosCaja.transform.position = new Vector2(
                       enemigosCaja.transform.position.x,
                       enemigosCaja.transform.position.y - enemigosDistanciaMovimiento);
                }
            }
            else
            {
                float enemigoIzquierda = 0f;

                foreach (Enemigo enemigo in GetComponentsInChildren<Enemigo>())
                {
                    if (enemigo.transform.position.x < enemigoIzquierda)
                    {
                        enemigoIzquierda = enemigo.transform.position.x;
                    }
                }

                if (enemigoIzquierda < -limiteHorizontal)
                {
                    movimientoDireccion *= -1;

                    enemigosCaja.transform.position = new Vector2(
                       enemigosCaja.transform.position.x,
                       enemigosCaja.transform.position.y - enemigosDistanciaMovimiento);
                }
            }
        }


        if (enemigosActual == 0 || jugador == null)
        {
            SceneManager.LoadScene("Juego");
        }
    }
}
