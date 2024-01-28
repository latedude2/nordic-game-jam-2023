using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProcAnim : MonoBehaviour
{
    [SerializeField] LayerMask terrainLayer;
    [SerializeField] Transform body;
    [SerializeField] Transform core;
    [SerializeField] float stepDistance;
    [SerializeField] float speed;
    [SerializeField] float stepHeight;
    [SerializeField] float maxSpread;
    Vector3 currentPosition, prevPosition, spreadVector;
    float lerp, stepDistVar;

    void Start()
    {
        currentPosition = transform.position;
        spreadVector = new Vector3(0, 0, 0);
        stepDistVar = Random.Range(stepDistance*.85f, stepDistance*1.15f);
    }

    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, currentPosition, Time.deltaTime*speed);
        float velocity = Vector3.Distance(core.position, prevPosition) / Time.deltaTime;
        prevPosition = core.position;

        Debug.Log(velocity);

        RaycastHit hit;
        
        if (Physics.Raycast(body.position, body.TransformDirection(Vector3.forward) + spreadVector, out hit, 10, terrainLayer))
        {
            Debug.DrawRay(body.position, body.TransformDirection(Vector3.forward) + spreadVector * hit.distance, Color.yellow);
            //Debug.Log(Vector3.Distance(currentPosition, hit.point));
            if (Vector3.Distance(currentPosition, hit.point) > stepDistVar){
                currentPosition = hit.point;
                lerp = 0;
            }
        }

        if (lerp < 1){
            Vector3 tempPosition = Vector3.Lerp(transform.position, currentPosition, lerp);
            tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;
        
            transform.position = tempPosition;
            lerp += Time.deltaTime * speed * (velocity+1);
        } else {
            transform.position = currentPosition;
            float xSpread = Random.Range(-maxSpread, maxSpread);
            float ySpread = Random.Range(-maxSpread, maxSpread);
            spreadVector = new Vector3(xSpread, ySpread, 0);
            stepDistVar = Random.Range(stepDistance*.85f, stepDistance*1.15f);
        }
    }
}
