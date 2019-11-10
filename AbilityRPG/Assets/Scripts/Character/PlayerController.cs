using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public CharacterBase characterBase;
        
    protected Joystick joystick;
    protected Joybutton joybutton;
    
    public Animator animator;

    protected NavMeshAgent navAgent;
    public LayerMask layerMask;

    private Transform targetNPC;
    public bool isNpcTarget = false;
    public bool isTalk = false;
    public bool isPopup = false;

    // 임시로 플레이어 체력 감소 테스트용
    public PlayerHpBar playerHpBar;
    public bool isDead;

    public PlayerTargeting playerTargeting;

    void Start()
    {
        PlayerSetting();
    }

    public void PlayerSetting()
    {
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();

        animator = GetComponent<Animator>();

        navAgent = GetComponent<NavMeshAgent>();
        navAgent.stoppingDistance = 0.7f;
        navAgent.speed = characterBase.speed;

        isDead = false;

        // 임시로 플레이어 체력 감소 테스트용
        if (gameObject.transform.parent != null)
            playerHpBar = gameObject.transform.parent.transform.Find("Canvas").GetComponent<PlayerHpBar>();

        playerTargeting = GetComponent<PlayerTargeting>();
    }

    public void FixedUpdate()
    {
        // 팝업창이 떠있는 경우
        if (isPopup || isDead)
            return;

        if (IsPlayerMoving())
        {
            isNpcTarget = false;
            this.gameObject.GetComponent<PlayerTargeting>().getTarget = false;

            animator.SetBool("MOVE", true);
            animator.SetBool("THROW", false);
            animator.SetBool("IDLE", false);

            navAgent.SetDestination(this.transform.position);

            transform.Translate(Vector3.forward * characterBase.speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg, 0f);
        }
        else if (isNpcTarget)
        {
            animator.SetBool("MOVE", true);

            // 거리 측정
            float dist = Vector3.Distance(transform.position, targetNPC.position);

            // 도착
            if (navAgent.stoppingDistance >= dist)
            {
                animator.SetBool("MOVE", false);

                // 대화 진행
                isNpcTarget = false;
                isTalk = true;
            }
        }
        else
        {
            animator.SetBool("MOVE", false);
            animator.SetBool("THROW", false);
            animator.SetBool("IDLE", true);
        }

        // 전투 중이 아닐 경우에만
        if (playerTargeting.monsterList == null || playerTargeting.monsterList.Count == 0)
            NpcTargeting();
    }

    public bool IsPlayerMoving()
    {
        if(joystick == null)
            return false;

        if (joystick.Vertical != 0 || joystick.Horizontal != 0)
            return true;
        else
            return false;
    }

    public void PlayerWalkSound()
    {
        SoundManager.Instance.effectAudio.PlayOneShot(SoundManager.Instance.PlayerEFXSounds[0]);
    }

    // npc 클릭시 npc 한테 이동
    public void NpcTargeting()
    {
        // 게임 오브젝트 위일 경우만 PC에서는 -1 모바일에서는 0
        int pointerID;

    #if UNITY_EDITOR
        pointerID = -1;
    #elif UNITY_ANDROID
        pointerID = 0;
    #endif

        // ui 위에 없을 경우
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(pointerID))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, 100f, layerMask))
                {
                    if (hitInfo.transform.CompareTag("NPC"))
                    {
                        isNpcTarget = true;
                        targetNPC = hitInfo.transform;
                        transform.LookAt(hitInfo.transform);
                        navAgent.SetDestination(hitInfo.transform.position);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("NextScene"))
            SceneLoadManager.Instance.LoadScene("2_BattleField");

        if (other.transform.CompareTag("BattleStart"))
            FieldManager.Instance.BattleStart();

        if(other.transform.CompareTag("MonsterAtk"))
        {
            // 공격 받은거 처리
            Debug.Log("플레이어 데미지 받음");
            other.gameObject.SetActive(false);
            Instantiate(EffectSet.Instance.PlayerDmgEffect, playerTargeting.attackPoint.position, Quaternion.Euler(90, 0, 0));
            SoundManager.Instance.effectAudio.PlayOneShot(SoundManager.Instance.MonsterEFXSounds);

            // 몬스터 공격력으로 적용해야 함
            playerHpBar.Dmg(other.gameObject.transform.parent.GetComponent<EnemyBase>().damage);
            
            if(playerHpBar.currentHp <= 0)
            {
                animator.SetBool("MOVE", false);
                animator.SetBool("THROW", false);
                animator.SetBool("IDLE", false);
                animator.SetBool("DEAD", true);

                // 나중에 게임 매니저에서 처리해야 함
                joystick.gameObject.SetActive(false);
                isDead = true;
            }
        }
    }
}
