using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float m_speed = 4f;
    
    Player m_player;
    Animator m_animator;
    
    void Start()
    {
        m_player = GameObject.Find("Player").GetComponent<Player>();
        m_animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        EnemyMovement();

    }

    private void EnemyMovement()
    {

        transform.Translate(Vector3.down * m_speed * Time.deltaTime);
        // if it reaches bottom of screen, respawn it with a random origin.

        if (transform.position.y <= -6f)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Using Debug.Log(other.tag), We can see who collides with the enemy.
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            m_animator.SetTrigger("OnEnemyDeath");
            m_speed = 0;
            Destroy(this.gameObject, 1f);
            
          
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (m_player != null)
            {
                m_player.AddScore();               
            }
            m_animator.SetTrigger("OnEnemyDeath");
            m_speed = 0;
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 1f);
            


        }
    }
}
