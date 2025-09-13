using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacterMovement : MonoBehaviour
{
    public static PlayerCharacterMovement Instance;

    [SerializeField]
    bool enableCheatConsole;

    [Header("Movement")]
    public float currentDashCooldown;
    [SerializeField]
    float maxSpeed;
    public float Speed, SpeedOverMax;
    public LayerMask GroundMask;


    [Space]
    [SerializeField]
    Camera mainCam;

    [Header("Health And Collision")]
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    Transform playerHitBox;
    public int HealthPoints
    {
        get => m_healthPoints;
        set
        {
            if (value <= 0)
            {
                Die();
                isInvincible = true;
                StartCoroutine(TurnInvincible());
                m_healthPoints = 0;
            }
            else if (value < m_healthPoints)
            {
                isInvincible = true;
                StartCoroutine(TurnInvincible());
                m_healthPoints = value;
            }
            else if (value > MaxHP)
            {
                m_healthPoints = MaxHP;
            }
            else
            {
                m_healthPoints = value;
            }
            OnHealthChange?.Invoke();
        }
    }
    [SerializeField]
    private int m_healthPoints;
    public int MaxHP;
    public bool isInvincible;
    [SerializeField]
    float invincibleTimer;

    public GameObject Projectile;
    public float ProjectileDamage, ProjectileSpeed;
    Vector2 m_moveDir = new Vector2();
    float walkingDistance;
    [HideInInspector]
    public UnityEvent OnHealthChange;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        HealthPoints = m_healthPoints;
    }

    void Update()
    {
        //Makes the player look to the mouse
        Ray cameraRay = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(cameraRay, out hit, 200, GroundMask))
        {
            Vector3 pointToLook = hit.point;
            playerHitBox.transform.LookAt(new Vector3(pointToLook.x, playerHitBox.transform.position.y, pointToLook.z), Vector3.up);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot1();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Shoot2();
        }
    }

    private void FixedUpdate()
    {
        Movement();
        MovePlayer();
    }

    private void MovePlayer()
    {
        //rb.AddForce(new Vector3((m_moveDir.y * -0.66f) + (m_moveDir.x * 0.66f), 0, (m_moveDir.y * 0.66f) + (m_moveDir.x * 0.66f)) * Speed, ForceMode.VelocityChange);
        if (maxSpeed < rb.velocity.sqrMagnitude)
        {
            rb.AddForce(new Vector3(m_moveDir.x, 0, m_moveDir.y) * SpeedOverMax, ForceMode.VelocityChange);
        }
        else
        {
            rb.AddForce(new Vector3(m_moveDir.x, 0, m_moveDir.y) * Speed, ForceMode.VelocityChange);
        }

        if (m_moveDir != Vector2.zero)
        {
            walkingDistance += Time.deltaTime;
            if (walkingDistance >= 0.3f)
            {
                walkingDistance = 0;
            }
        }
    }

    public void Die()
    {
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            HealthPoints -= damage;
        }
    }

    IEnumerator TurnInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTimer);
        isInvincible = false;
    }

    public void HealFull()
    {
        HealthPoints = MaxHP;
    }

    public void Heal(int _amount)
    {
        HealthPoints += _amount;
    }

    public void Shoot1()
    {

    }

    public void Shoot2()
    {

    }


    #region InputMethods
    public void Movement()
    {
        Vector2 mDir = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            mDir.y += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            mDir.x -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            mDir.y -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            mDir.x += 1;
        }
        m_moveDir = mDir;
    }
}

#endregion