using System.Collections;
using UnityEngine;
using NaughtyAttributes;

public class PlayerMovement : MonoBehaviour
{
    [BoxGroup("Settings")]
    public CharacterController controller;
    [BoxGroup("Settings")]
    public Animator animator;
    [BoxGroup("Settings")]
    public float walkSpeed = 12f;
    [BoxGroup("Settings")]
    public float sprintSpeed = 16f;
    [BoxGroup("Settings")]
    public Transform groundCheck;
    [BoxGroup("Settings")]
    public float groundDistance = 0.4f;
    [BoxGroup("Settings")]
    public LayerMask groundMask;
    [BoxGroup("Settings")]
    public float gravity = -9.81f;
    [BoxGroup("Settings")]
    public float jumpHeight = 5f;
    [BoxGroup("Settings (Sprint)")]
    public PeakSyncMeter peakSyncMeter;
    [BoxGroup("Settings (Sprint)")]
    public float sprintFOV = 58f;
    [BoxGroup("Settings (Head Bob)")]
    public Transform playerCam;
    [BoxGroup("Settings (Head Bob)")]
    public float frequency = 3;
    [BoxGroup("Settings (Head Bob)")]
    public float amplitudeX = 0.1f;
    [BoxGroup("Settings (Head Bob)")]
    public float amplitudeY = 0.025f;
    [BoxGroup("Settings (Head Bob)")]
    public float bobRevertTime = 0.75f;
    [BoxGroup("Settings (Head Bob)")]
    [ReadOnly]
    public float x;
    [BoxGroup("Settings (Head Bob)")]
    [ReadOnly]
    public float y;
    [BoxGroup("Settings (Crouch)")]
    public float crouchSpeed = 2f;
    [BoxGroup("Settings (Crouch)")]
    public float transitionDuration = 1f;
    [BoxGroup("Settings (Crouch)")]
    public float crouchXAmplitude = 0.2f;
    [BoxGroup("Settings (Crouch)")]
    public float crouchZAmplitude = 0.2f;
    [BoxGroup("Settings (Crouch)")]
    public Vector3 reducedPosition;
    [BoxGroup("Settings (Crouch)")]
    public Transform playerGFX;
    [BoxGroup("Settings (Crouch)")]
    public Vector3 reducedGFXPosition;
    [BoxGroup("Settings (Crouch)")]
    public Transform crouchHeadPoint;
    [BoxGroup("Settings (Crouch)")]
    public float crouchHeadRadius;
    [BoxGroup("Settings (Crouch)")]
    public LayerMask obstacleMask;
    [BoxGroup("Settings (Crouch)")]
    public KeyCode crouchKey;

    [BoxGroup("Controls")]
    public KeyCode sprintKey;

    [ReadOnly]
    public float speed;
    private float prevFrameSpeed;

    private bool prevFrameWasGoingToMove;

    private float defaultFOV;

    private Vector3 initialPlayerCamPosition;
    private Vector3 initialPlayerGFXPosition;
    private float startTime;

    private Vector3 velocity;
    private bool isGrounded;
    private PlayerEvents playerEvents;
    private Camera cam;

    private bool isCrouching;

    private void Awake()
    {
        playerEvents = GetComponent<PlayerEvents>();
    }

    private void Start()
    {
        peakSyncMeter.SetSync(true);
        cam = Camera.main;
        defaultFOV = cam.fieldOfView;
        initialPlayerCamPosition = playerCam.localPosition;
        initialPlayerGFXPosition = playerGFX.localPosition;
        isCrouching = false;
    }

    private void Update()
    {
        if(!PlayerManager.instance.isPlayerAllowedToMove)
            return;

        MakeGrounded();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool isGoingToMove = x != 0 || z != 0;

        GizmosAndEvents(isGoingToMove);

        CheckSprint(isGoingToMove);
        
        if(PlayerManager.instance.isPlayerAllowedToCrouch)
            Crouch(isGoingToMove);

        CheckBob(isGoingToMove);

        MovePlayer(x, z);

        JumpPlayer();

        prevFrameSpeed = speed;
        prevFrameWasGoingToMove = isGoingToMove;
    }

    private void MakeGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
    }

    private void GizmosAndEvents(bool isGoingToMove)
    {
        if(isGoingToMove)
        {
            playerEvents.InvokeMoved(this);
        }
        else
        {
            playerEvents.InvokeNotMoved(this);
        }
    }

    private void MovePlayer(float x, float z)
    {
        Vector3 direction = transform.forward * z + transform.right * x;
        controller.Move(speed * Time.deltaTime * direction);
        animator.SetFloat("normalizedSpeed", controller.velocity.magnitude / sprintSpeed);
    }

    private void JumpPlayer()
    {
        if(isCrouching)
            return;

        bool jumpKeyPressed = Input.GetButtonDown("Jump");

        if(jumpKeyPressed)
        {
            playerEvents.InvokeJumped(this);
        }

        if(jumpKeyPressed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);     // gt^2
    }

    private void CheckSprint(bool isGoingToMove)
    {
        if(isCrouching)
            return;

        ChangeSpeed(isGoingToMove);
        CheckTransitions();
    }

    private void CheckBob(bool isGoingToMove)
    {
        Bob(isGoingToMove);
        Stablise(isGoingToMove);
    }

    private void Crouch(bool isGoingToMove)
    {
        bool isKeyPressed = Input.GetKeyDown(crouchKey);
        
        if(!isKeyPressed || isGoingToMove || prevFrameWasGoingToMove || Physics.Raycast(crouchHeadPoint.position, transform.up, crouchHeadRadius, obstacleMask))
            return;
        
        PlayerManager.instance.isPlayerAllowedToMove = PlayerManager.instance.isPlayerAllowedToLook = false;

        isCrouching = !isCrouching;

        Timer.instance.ClearAllTimers("crouch");
        Timer.instance.ClearAllTimers("bob");
        TimerInstance timerInstance = Timer.instance.CreateTimer(transitionDuration, 1, "crouch");
        timerInstance.timerStart += CrouchTransition;
    }

    public IEnumerator CrouchTransition(TimerInstance timer)
    {
        Vector3 camStartPos = playerCam.localPosition;
        Vector3 camEndPos = isCrouching ? reducedPosition : initialPlayerCamPosition;
        Vector3 playerGFXStartPos = playerGFX.localPosition;
        Vector3 playerGFXEndPos = isCrouching ? reducedGFXPosition : initialPlayerGFXPosition;
        int[] r = new int[2] {-1, 1};
        int sign = r[Random.Range(0, 2)];
        while(timer.NormalizedValue() < 1f)
        {
            float t = timer.NormalizedValue();
            Vector3 instantaneousCamPos = playerCam.localPosition;
            Vector3 instantaneousGFXPos = playerGFX.localPosition;
            instantaneousCamPos.y = Mathf.Lerp(camStartPos.y, camEndPos.y, t);
            instantaneousGFXPos.y = Mathf.Lerp(playerGFXStartPos.y, playerGFXEndPos.y, t);
            instantaneousCamPos.x = camStartPos.x + crouchXAmplitude * Mathf.Sin(sign * t * Mathf.PI);
            instantaneousCamPos.z = camStartPos.z + crouchZAmplitude * Mathf.Sin(t * Mathf.PI);
            playerCam.localPosition = instantaneousCamPos;
            playerGFX.localPosition = instantaneousGFXPos;
            yield return null;
        }

        playerCam.localPosition = camEndPos;
        playerGFX.localPosition = playerGFXEndPos;

        float f = isCrouching ? 0.5f : 2f;

        controller.height *= f;
        controller.center = new Vector3(controller.center.x, controller.center.y*f, controller.center.z);
        
        speed = isCrouching ? crouchSpeed : walkSpeed;

        PlayerManager.instance.isPlayerAllowedToMove = PlayerManager.instance.isPlayerAllowedToLook = true;
    }

    private void ChangeSpeed(bool isGoingToMove)
    {
        speed = walkSpeed;
        peakSyncMeter.SetSync(!isGoingToMove);

        if(!Input.GetKey(sprintKey) || peakSyncMeter.GetValue() <= 0f || !isGoingToMove)
            return;
        
        speed = sprintSpeed;
        peakSyncMeter.Decrement(1f * Time.deltaTime);
    }

    private void CheckTransitions()
    {
        if(prevFrameSpeed == speed)
            return;
        
        Timer.instance.ClearAllTimers("sprint");
        TimerInstance instance = Timer.instance.CreateTimer(0.2f, 1, "sprint");

        if(speed == sprintSpeed)
        {
            instance.timerStart += SprintTransition;
        }
        else
        {
            instance.timerStart += WalkTransition;
        }
    }

    public IEnumerator SprintTransition(TimerInstance timer)
    {
        while(!Mathf.Approximately(timer.NormalizedValue(), 1f))
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, sprintFOV, timer.NormalizedValue());
            yield return null;
        }
    }

    public IEnumerator WalkTransition(TimerInstance timer)
    {
        while(!Mathf.Approximately(timer.NormalizedValue(), 1f))
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, defaultFOV, timer.NormalizedValue());
            yield return null;
        }
    }

    private void Bob(bool isGoingToMove)
    {
        if(!isGoingToMove)
            return;

        if(isGoingToMove && !prevFrameWasGoingToMove)
            startTime = Time.time;

        x = amplitudeX * Mathf.Sin(2 * frequency * speed * (Time.time - startTime));
        y = amplitudeY * Mathf.Abs(Mathf.Sin(frequency * speed * (Time.time - startTime)));
        Vector3 pos = isCrouching ? reducedPosition : initialPlayerCamPosition;
        pos.y += y;
        pos.x += x;
        playerCam.localPosition = pos;
    }

    private void Stablise(bool isGoingToMove)
    {
        if(isGoingToMove || !prevFrameWasGoingToMove)
            return;
            
        Timer.instance.ClearAllTimers("bob");
        TimerInstance timer = Timer.instance.CreateTimer(bobRevertTime * (Vector3.Distance(playerCam.localPosition, isCrouching ? reducedPosition : initialPlayerCamPosition) / amplitudeY), 1, "bob");
        timer.timerStart += Stablization;
    }

    public IEnumerator Stablization(TimerInstance timer)
    {
        Vector3 startPos = playerCam.localPosition;
        Vector3 endPos = isCrouching ? reducedPosition : initialPlayerCamPosition;
        while(!Mathf.Approximately(timer.NormalizedValue(), 1f))
        {
            playerCam.localPosition = Vector3.Lerp(startPos, endPos, timer.NormalizedValue());
            yield return null;
        }
    }
}