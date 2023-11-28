using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float speed;
    public GameManager manager;

    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirVec;
    Animator anim;
    GameObject scanObject;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MovePlayer();

        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
            anim.SetBool("isHorizontal", isHorizonMove);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
            anim.SetBool("isHorizontal", isHorizonMove);
        }
        else
        {
            anim.SetBool("isChange", false);
        }

        bool hHold = Input.GetButton("Horizontal");
        bool vHold = Input.GetButton("Vertical");
        if (hHold && isHorizonMove && h != 0)
        {
            dirVec = new Vector3((int)h, 0, 0);
        }
        else if (vHold && !isHorizonMove && v != 0)
        {
            dirVec = new Vector3(0, (int)v, 0);
        }

        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            manager.Action(scanObject);
        }

    }

    private void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;

        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else { scanObject = null; }
    }


    void MovePlayer()
    {
        if (manager.isAction)
        {
            h = 0;
            v = 0;
            return;
        }

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");


        if (hDown || vUp)
        {
            isHorizonMove = true;

        }
        else if (vDown || hUp)
        {

            isHorizonMove = false;

        }

        if (h == 0 && v != 0)
        {
            isHorizonMove = false;
        }
        else if (h != 0 && v == 0)
        {
            isHorizonMove = true;
        }
    }

}
