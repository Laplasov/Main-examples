using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField]
    private float m_speed = 10.0f;
    [SerializeField]
    private float m_gravity = 0.5f;
    [SerializeField]
    private float m_jumpHight = 3f;
    [SerializeField]
    private float m_jump = 0.5f;
    [SerializeField]
    private float m_runMultiplier = 2f;

    [SerializeField]
    private StaminaSO m_staminaSO;

    private float m_Ypos = 0f;
    private float m_relativeJumpHight;
    private bool isJumping = false;
    private float m_speedInit;
    private bool m_exhausted = false;

    private string[] directions = { "Forward", "Right", "Backward", "Left" };
    private int currentDirectionIndex = 0;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        m_Ypos = -m_gravity;
        m_speedInit = m_speed;
        m_staminaSO.StaminaInit = m_staminaSO.Stamina;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            ChangeDirection(1); 

        if (Input.GetKeyDown(KeyCode.Q))
            ChangeDirection(-1); 
    }
    private void FixedUpdate()
    {
        Vector3 move = DeterminAxes();

        if (move.magnitude > 1)
            move.Normalize();

        if (Input.GetKey(KeyCode.LeftShift) && m_staminaSO.Stamina > 0 && !m_exhausted)
        {
            m_speedInit = m_speed * m_runMultiplier;
            m_staminaSO.Stamina -= m_staminaSO.StaminaDepletionRate * Time.deltaTime;

            if (m_staminaSO.Stamina < 0)
            {
                m_exhausted = true;
                m_speedInit = m_speed;
            }
        }

        if (!Input.GetKey(KeyCode.LeftShift) || m_exhausted)
        {
            m_speedInit = m_speed;
            if (m_staminaSO.Stamina < m_staminaSO.StaminaInit)
                m_staminaSO.Stamina += m_staminaSO.StaminaRecoveryRate * Time.deltaTime;
            if (m_staminaSO.Stamina >= m_staminaSO.StaminaInit)
                m_exhausted = false;
        }

        characterController.Move(move * m_speedInit * Time.deltaTime);

        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            m_relativeJumpHight = transform.position.y + m_jumpHight;
            isJumping = true;
        }

        if (isJumping)
            m_Ypos = m_jump;
        else
            m_Ypos = -m_gravity;

        if (transform.position.y > m_relativeJumpHight)
            isJumping = false;

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) 
        { 
        Quaternion targetRotation = Quaternion.LookRotation(move);
        targetRotation.x = 0;
        targetRotation.z = 0;
        transform.rotation = targetRotation; 
        }
    }

    private Vector3 DeterminAxes()
    {
        if (directions[currentDirectionIndex] == "Forward")
            return new Vector3(Input.GetAxis("Horizontal"), m_Ypos, Input.GetAxis("Vertical"));

        if (directions[currentDirectionIndex] == "Right")
            return new Vector3(-Input.GetAxis("Vertical"), m_Ypos, Input.GetAxis("Horizontal"));

        if (directions[currentDirectionIndex] == "Backward")
            return new Vector3(-Input.GetAxis("Horizontal"), m_Ypos, -Input.GetAxis("Vertical"));

        if (directions[currentDirectionIndex] == "Left")
            return new Vector3(Input.GetAxis("Vertical"), m_Ypos, -Input.GetAxis("Horizontal"));

        return new Vector3(Input.GetAxis("Horizontal"), m_Ypos, Input.GetAxis("Vertical"));
    }
    private void ChangeDirection(int directionChange)
    {
        currentDirectionIndex = (currentDirectionIndex + directionChange) % directions.Length;

        if (currentDirectionIndex < 0)
            currentDirectionIndex += directions.Length;

    }

}
