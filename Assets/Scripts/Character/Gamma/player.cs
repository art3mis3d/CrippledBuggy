using UnityEngine;
public class player: MonoBehaviour
{
    [SerializeField]
    private InputReader inputReader;

    public Transform cam;
    private Animator anim;

    public float speed;

    public float turnsmoothtime = 0.1f;
    float turnsmoothvelocity;

    private Vector2 move;
    private Vector2 cameraorbit;
    //public bool Sprint { get; private set; } = false;

    private void OnEnable()
    {
        inputReader.MoveEvent += OnMove;
        inputReader.CameraEvent += OnCamera;
        inputReader.StartedSprinting += OnSprint;
    }

    public void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        Vector3 m = new Vector3(move.x, 0, move.y) * Time.deltaTime * speed;
        if((move.x*move.x + move.y * move.y) > 0)
        {
            walk();
        }
        else
        {
            idle();
        }

        //float targetangle = Mathf.Atan2(m.x, m.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothtime);
        transform.Translate(m, Space.World);
        /*transform.rotation = Quaternion.Euler(0f, targetangle, 0f);
        Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;*/

    }
    void walk()
    {
            anim.SetFloat("speed", Mathf.Clamp01(move.magnitude));
            //anim.SetBool("sprint", Sprint);
    }
    void idle()
    {
        anim.SetFloat("speed", 0f);
        //anim.SetBool("sprint", false);
    }

    private void OnDisable()
    {
        inputReader.MoveEvent -= OnMove;
        inputReader.CameraEvent -= OnCamera;
        inputReader.StartedSprinting -= OnSprint;
    }

    private void OnMove(Vector2 leftStick)
    {
        move = leftStick;
    }
    private void OnCamera(Vector2 rightStick)
    {
        cameraorbit = rightStick;
    }

    private void OnSprint()
    { 
        // Sprint = !Sprint;
    }
}
