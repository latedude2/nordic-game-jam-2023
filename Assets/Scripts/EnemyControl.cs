using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private GameObject target;
    public float lookatRotSpeed = 1f;
    public float moveSpeed = 1f;

    public int triggerDistToTarget;

    void Start()
    {
        target = GameObject.FindWithTag("Player");

        Reposition();
    }

    void Reposition()
    {
        float distance = Random.Range(10,30);
        float offset = Random.Range(-10,10);
        transform.position = target.transform.position - (target.transform.forward * distance) + (target.transform.right * offset);
        GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
    }

    void Lookat(){
        Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, lookatRotSpeed*Time.deltaTime);
    }
    void MoveForward(){

        GetComponent<Rigidbody>().AddForce(transform.forward*moveSpeed);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > triggerDistToTarget){
            Reposition();
        }
        Lookat();
        MoveForward();
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Player"){
            Reposition();
        }
    }
}
