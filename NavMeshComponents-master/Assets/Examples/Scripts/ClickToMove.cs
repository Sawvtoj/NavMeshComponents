using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Use physics raycast hit from mouse click to set agent destination
[RequireComponent(typeof(NavMeshAgent))]
public class ClickToMove : MonoBehaviour
{
    public ThirdPersonCharacter character;
    NavMeshAgent m_Agent;
    RaycastHit m_HitInfo = new RaycastHit();
    public Text scoreText;
    public Text livesText;
    private int count = 0;
    public int lives;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.updateRotation = false;
        lives = 3;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
                m_Agent.destination = m_HitInfo.point;
        }

        if(m_Agent.remainingDistance > m_Agent.stoppingDistance)
        {
            character.Move(m_Agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
        
        livesText.text = "Lives: " + lives;

        if (count == 180)
        {
            Debug.Log("End Game");
            SceneManager.LoadScene("End");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            other.gameObject.SetActive(false);
            count += 10;
            scoreText.text = "Score: " + count;
            
        }

        if (other.gameObject.tag == "Enemy")
        {
            //other.gameObject.SetActive(false);
            lives -= 1;
            Debug.Log("You were Attacked");

            if(lives == 0 || count == 180)
            {
                Debug.Log("End Game");
                SceneManager.LoadScene("End");
            }
        }
    }
}
