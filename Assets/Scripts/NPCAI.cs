using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    //[SerializeField] private GameObject destination;
    //private NavMeshAgent _agent;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    _agent = GetComponent<NavMeshAgent>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    _agent.SetDestination(destination.transform.position);
    //}

    public NavMeshAgent _agent;
    [SerializeField] Transform _player;
    public LayerMask ground, player;

    public Vector3 destinationPoint; //belirli bir alanda hareket için yürüyüs noktası
    bool destinationPointSet; //Bu noktanın set edilip edilmediğini göreceğimiz bool
    public float walkPointRange; //Hareket edebileceğimiz mesafe

    //Saldırı planlaması yapılacağında bu kısıma dönülecek
    public float timeBetweensAttacks; //Atışlar arası süre için
    bool alreadyAttacked; //Saldırı
    public GameObject bullet;

    public float sightRange, attackRange; // NPC'nin saldırı ve görüş alanı
    public bool playerInSightRange, playerAttackRange;//Player için

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player);
        playerAttackRange = Physics.CheckSphere(transform.position, attackRange, player);

        //Patrol / Chase / Attack
        if(!playerAttackRange && !playerInSightRange) Patroling(); //Gezinme

        if (playerInSightRange && !playerAttackRange) ChasePlayer(); //Kovalama

        if (playerInSightRange && playerAttackRange) AttackPlayer();
    }

    void Patroling()
    {
        if (!destinationPointSet)
        {
            SearchWalkPoint();
        }

        if (destinationPointSet)
        {
            _agent.SetDestination(destinationPoint);
        }

        //DestinationPoint'e gidip gitmediğimizi anlamak için
        Vector3 distanceToDestinationPoint = transform.position - destinationPoint;
        if(distanceToDestinationPoint.magnitude < 1.0f)
        { 
            destinationPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        destinationPoint = new Vector3(transform.position.x + randomX, transform.position.y,
            transform.position.z + randomZ);

        if(Physics.Raycast(destinationPoint, -transform.up, 2.0f, ground))
        {
            destinationPointSet = true;
        }
    }

    void ChasePlayer()
    {
        _agent.SetDestination(_player.position);
    }

    void AttackPlayer()
    {
        _agent.SetDestination(transform.position);

        transform.LookAt(_player);
        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent < Rigidbody > ();
            rb.AddForce(transform.forward * 25f, ForceMode.Impulse); //ileri
            rb.AddForce(transform.up * 7f, ForceMode.Impulse); //yukarı

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweensAttacks);

        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
