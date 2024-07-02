using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using Unity.VisualScripting;

namespace Lightbug.GrabIt
{

    [System.Serializable]
    public class GrabObjectProperties
    {
        public bool m_useGravity = false;
        public float m_mass = 0.1f;
        public float m_drag = 10;
        public float m_angularDrag = 10;
        public RigidbodyConstraints m_constraints = RigidbodyConstraints.FreezeRotation;

    }

    public class GrabIt : NetworkBehaviour
    {
        [Header("Grab properties")]

        [SerializeField]
        [Range(4, 50)]
        float m_grabSpeed = 7;

        [SerializeField]
        [Range(0.1f, 5)]
        float m_grabMinDistance = 1;

        [SerializeField]
        [Range(2, 25)]
        float m_grabMaxDistance = 10;

        [SerializeField]
        float m_impulseMagnitude = 1;

        [SerializeField]
        bool canPush = true;

        public delegate void GrabObjectHandler(Rigidbody rb);
        public static event GrabObjectHandler Grabbed;
        public static event GrabObjectHandler Released;


        [Header("Affected Rigidbody Properties")]
        [SerializeField] GrabObjectProperties m_grabProperties = new GrabObjectProperties();

        GrabObjectProperties m_defaultProperties = new GrabObjectProperties();



        public Rigidbody m_targetRB = null;        
        Transform m_transform;

        Vector3 m_targetPos;
        float m_targetDistance;

        private bool grabbedThisFrame = false;
        private bool releasedThisFrame = false;
        public bool m_holding = false;
        public bool m_pushing = false;
        bool m_applyImpulse = false;
        bool m_isHingeJoint = false;
        bool m_isConfigurableJoint = false;

        //Debug
        LineRenderer m_lineRenderer;

        public override void OnNetworkSpawn()
        {
            if(!IsOwner)
            {
                return;
            }
            m_transform = transform;
            m_lineRenderer = GetComponent<LineRenderer>();
            Grabbed += OnGrabbed;
        }

        void GrabSound()
        {
            //TODO: play sound for grabbing
        }


        void OnGrabbed(Rigidbody rb)
        {
            if(rb == null)
            {
                return;
            }

            NetworkObjectReference target = rb.GetComponent<NetworkObject>();
            GrabbedChangeOwnershipRpc(target);
        }


        [Rpc(SendTo.Server)]
        public void GrabbedChangeOwnershipRpc(NetworkObjectReference target)
        {
            if (target.TryGet(out NetworkObject targetObject))
            {
                targetObject.ChangeOwnership(GetComponentInParent<NetworkObject>().OwnerClientId);
                Debug.Log("Ownership changed to " + GetComponentInParent<NetworkObject>().OwnerClientId);
            }
        }

        void Update()
        {
            if(!IsOwner)
            {
                return;
            }

            releasedThisFrame = false;
            if (m_holding)
            {
                grabbedThisFrame = false;
                m_targetDistance = Mathf.Clamp(m_targetDistance, m_grabMinDistance, m_grabMaxDistance);

                m_targetPos = m_transform.position + m_transform.forward * m_targetDistance;
                if(Input.GetMouseButtonDown(0))
                {
                    TryToRelease(); 
                }

                if (Input.GetMouseButtonDown(1))
                {
                    m_applyImpulse = true;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    TryToGrab();
                }
            }

        }

        void FixedUpdate()
        {
            if(!IsOwner)
            {
                return;
            }

            if (!m_holding)
                return;
            
            Hold();
            Throw();
        }

        private void TryToRelease()
        {
            if(grabbedThisFrame)
                return;

            if (m_holding)
            {
                Release();
                m_holding = false;
                releasedThisFrame = true;
            }

            if(m_pushing)
            {
                Release();
                m_pushing = false;
                releasedThisFrame = true;
            }
        }

        private void TryToGrab()
        {
            if(releasedThisFrame || m_holding || m_pushing)
                return;

            grabbedThisFrame = true;
            RaycastHit hitInfo = GetComponent<MouseInteraction>().hit;
            if (hitInfo.collider != null)
            {
                if(hitInfo.collider.GetComponent<Pushable>() != null)
                {
                    if(!canPush)
                    {
                        return;
                    }
                    m_pushing = true;
                    Push();
                    GrabSound();
                    return;
                }
                Rigidbody rb = hitInfo.collider.GetComponent<Rigidbody>();
                if(rb == null && hitInfo.collider.transform.parent != null)
                {
                    rb = hitInfo.collider.transform.parent.GetComponent<Rigidbody>();
                }
                if (rb != null)
                {
                   
                    if(rb.GetComponent<Pushable>() == null) {
                        SetHeldObject(rb, hitInfo.distance);
                        m_holding = true;
                        GrabSound();
                        Grabbed?.Invoke(rb);
                    }
                }
            }
        }

        void Push()
        {
            RaycastHit hitInfo = GetComponent<MouseInteraction>().hit;
            if (hitInfo.collider != null)
            {
                
                Rigidbody rb = hitInfo.collider.GetComponent<Pushable>().pushableRigidbody;
                if (rb != null)
                {
                    Vector3 forceVector = m_transform.forward * m_impulseMagnitude * rb.mass;
                    forceVector = forceVector * (1 - hitInfo.distance / m_grabMaxDistance);
                    rb.GetComponent<Pushable>().PushRpc(forceVector);
                    Invoke(nameof(PushEndDelay), 0.1f);
                    return;   
                }
            }
        }

        void SetHeldObject(Rigidbody target, float distance)
        {
            m_targetRB = target;
            m_isHingeJoint = target.GetComponent<HingeJoint>() != null;
            m_isConfigurableJoint = target.GetComponent<ConfigurableJoint>() != null;

            //Rigidbody default properties	
            m_defaultProperties.m_useGravity = m_targetRB.useGravity;
            m_defaultProperties.m_mass = m_targetRB.mass;
            m_defaultProperties.m_drag = m_targetRB.drag;
            m_defaultProperties.m_angularDrag = m_targetRB.angularDrag;
            m_defaultProperties.m_constraints = m_targetRB.constraints;

            //Grab Properties	
            m_targetRB.useGravity = m_grabProperties.m_useGravity;
            m_targetRB.drag = m_grabProperties.m_drag;
            m_targetRB.mass = m_grabProperties.m_mass;
            m_targetRB.angularDrag = m_grabProperties.m_angularDrag;
            if(m_isHingeJoint || m_isConfigurableJoint)
            {
                m_targetRB.constraints = RigidbodyConstraints.None;
            }
            else {
                m_targetRB.constraints = m_grabProperties.m_constraints;
            }

            m_targetDistance = distance;
            m_targetPos = m_transform.position + m_transform.forward * m_targetDistance;

            if(m_targetRB.GetComponent<Collider>())
            {
                Physics.IgnoreCollision(m_targetRB.GetComponent<Collider>(), transform.parent.GetComponent<CapsuleCollider>(), true);
            }           
        }

        void PushEndDelay()
        {
            m_pushing = false;
        }

        void Release()
        {
            if(m_targetRB != null)
            {
                if(m_targetRB.GetComponent<Collider>())
                {
                    Physics.IgnoreCollision(m_targetRB.GetComponent<Collider>(), transform.parent.GetComponent<CapsuleCollider>(), false);
                }
                m_targetRB.useGravity = m_defaultProperties.m_useGravity;
                m_targetRB.mass = m_defaultProperties.m_mass;
                m_targetRB.drag = m_defaultProperties.m_drag;
                m_targetRB.angularDrag = m_defaultProperties.m_angularDrag;
                m_targetRB.constraints = m_defaultProperties.m_constraints; 
                
                Released?.Invoke(m_targetRB);
            }
            

            m_targetRB = null;

            if (m_lineRenderer != null)
                m_lineRenderer.enabled = false;

            GrabSound();
        }

        void Hold()
        {
            if(m_targetRB == null)
            {
                Drop();
                return;
            }
            //If held object gets destroyed for some reason
            Vector3 hitPointPos = m_targetRB.position;
            Vector3 dif = m_targetPos - hitPointPos;

            if (m_isHingeJoint || m_isConfigurableJoint){
                //Handle hinge rotation
                m_targetRB.AddForceAtPosition(m_grabSpeed * dif * 100, hitPointPos, ForceMode.Force);
                
            }
            else
            {
                //Handle hingeless picked up objects
                m_targetRB.velocity = m_grabSpeed * dif;
            }

            if (Vector3.Distance(m_transform.position,m_targetRB.transform.position) > m_grabMaxDistance)
                Drop();

            if (m_lineRenderer != null)
            {
                m_lineRenderer.enabled = true;
                m_lineRenderer.SetPositions(new Vector3[] { m_targetPos, hitPointPos });
            }
        }

        public void Drop()
        {
            if(m_holding)
            {
                Release();
                m_holding = false;
                m_applyImpulse = false;  
            }          
        }

        private void Throw()
        {
            if (m_applyImpulse)
            {
                m_targetRB.velocity = m_transform.forward * m_impulseMagnitude * m_targetRB.mass * 50;
                Release();
                m_holding = false;
                m_applyImpulse = false;
            }
        }
    }
}
