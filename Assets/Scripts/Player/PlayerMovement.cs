using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3;
    private Rigidbody2D rb;

    private PlayerMaster master;
    private CharacterControls controls;
    private CameraController cam;
    private Vector2 movement;

    [SerializeField] Transform looker;
    [SerializeField] Transform body;

    private bool combatMode = false;
    [SerializeField] Collider2D combatCollider;
    private Vector2 mousePos;

    private void Awake()
    {
        controls = new CharacterControls();
    }
    public void WakeUp()
    {
        master = GetComponentInParent<PlayerMaster>();
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main.GetComponent<CameraController>();

        speed = 5 + master.stats.GetAgility() * .1f;

        controls.Default.movement.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Default.movement.canceled += ctx => movement = Vector2.zero;

        controls.Default.mouse.performed += ctx => mousePos = ctx.ReadValue<Vector2>();
        controls.Default.mouse.canceled += ctx => mousePos = Vector2.zero;

        controls.Default.leftClick.performed += ctx => Attack();

        controls.Default.enteract.performed += ctx => Enteract();
        controls.Default.OpenInventory.performed += ctx => OpenInventory();
        controls.Default.combatMode.canceled += ctx => ToggleCombatMode();
    }

    private void ToggleCombatMode(){
        combatMode = !combatMode;
        HUDManager.I.ToggleCombatMenu(combatMode);

    }

    public void IncreaseStats()
    {
        speed = 5 + master.stats.GetAgility() * .1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 smoothFollow = Vector2.Lerp(transform.position,
        (Vector2)transform.position + (movement * speed/5), .1f);

        rb.MovePosition(smoothFollow);

        if (!combatMode)
        {
            looker.localPosition = movement * 5;
            if (movement == Vector2.zero) { return; }
            Vector2 direction = looker.position - body.position;
            body.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        }
        else
        {
            looker.position = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 direction = looker.position - body.position;
            body.rotation = Quaternion.FromToRotation(Vector3.up, direction);

            if (currentAttackCoolDown > 0)
            {
                currentAttackCoolDown -= Time.deltaTime;
                if (currentAttackCoolDown <= 0)
                {
                    combatCollider.enabled = false;
                    canAttack = true;
                }
            }
        }
    }
    public void SetPosition(Vector3 worldPos, Quaternion rotation)
    {
        cam.SetPosition(worldPos);
        rb.isKinematic = true;
        transform.position = worldPos;
        transform.rotation = rotation;
        rb.isKinematic = false;
    }
    [SerializeField] LayerMask enteractLayerMask;
    private void Enteract()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, body.up,2, enteractLayerMask);

        // If it hits something...
        if (hit.collider != null)
        {
            hit.collider.GetComponent<IEnteractable>()?.AttemptEnteract(this.master);
        }
    }

    public void LockMovement(bool unlock)
    {
        if (unlock)
        {
            controls.Enable();
        }
        else
        {
            controls.Disable();
        }
    }

    void OpenInventory()
    {
        HUDManager.I.OpenInventory(master.inventory.GetInventory());
    }

    bool canAttack = true;
    float attackCoolDowTime = .6f;
    float currentAttackCoolDown = 0;   


    void Attack()
    {
        if (!canAttack) { return; }
        canAttack = false;
        currentAttackCoolDown = attackCoolDowTime;
        combatCollider.enabled = true;
    }

    void Block()
    {

    }

    private void OnEnable() { 
        controls.Enable(); 
        body.gameObject.SetActive(true); 
        if(rb)rb.simulated = true; }

    private void OnDisable() { 
        controls.Disable(); 
        body.gameObject.SetActive(false);
        rb.simulated = false; }
}
