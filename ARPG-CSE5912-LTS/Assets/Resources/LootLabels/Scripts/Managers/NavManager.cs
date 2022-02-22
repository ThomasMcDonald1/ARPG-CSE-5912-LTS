using UnityEngine;
using UnityEngine.AI;

namespace LootLabels {
    /// <summary>
    /// Manager takes care of player navigation and running interaction delegates
    /// </summary>
    public class NavManager : MonoBehaviour {

        public bool EnableRangeChecker = false;
        public float InteractRange = 3;     //the minimum distance the player needs to be near the target to run the queue delegate, must be higher then navagent stopping distance

        bool CheckForDestination = false;   //we need a bool to start checking if our agent has reached the interactable object, only set true if the path is reachable
        float stoppingDistance = 2;     //cached value to reset back if needed

        NavMeshAgent playerNavAgent; //assigned through playercontroller on start

        Camera cam; //cached cam
        bool cameraSet = false;

        public EventHandler.Events QueuedEvent;  //delegate used when player reaches the target

        [HideInInspector]
        public Transform PlayerTransform;   //assigned through playercontroller on start
        [HideInInspector]
        public Transform TargetTransform;   //assigned when clicking on a target 

        public NavMeshAgent PlayerNavAgent
        {
            get { return playerNavAgent; }

            set
            {
                playerNavAgent = value;

                stoppingDistance = playerNavAgent.stoppingDistance;

                if (playerNavAgent.stoppingDistance > InteractRange) {
                    InteractRange = playerNavAgent.stoppingDistance;
                    Debug.Log("interact range must be higher then stopping distance, otherwise queued events can't be called");
                }
            }
        }

        public enum Targets {
            Terrain,
            InteractableObject
        }

        Targets selectedTarget;

        public static NavManager singleton = null;

        void Awake() {
            //Check if instance already exists
            if (singleton == null) {
                //if not, set instance to this
                singleton = this;
            }

            //If instance already exists and it's not this:
            else if (singleton != this) {
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);
            }

            SetCamera();
        }

        public void SetCamera() {
            cam = Camera.main;
            cameraSet = true;
        }

        // Update is called once per frame
        void Update() {
            //debugPathing();

            ResetStoppingDistance();

            switch (selectedTarget) {
                case Targets.Terrain:
                    ChangeStoppingDistance();
                    FollowMouse();
                    break;
                case Targets.InteractableObject:
                    if (CheckForDestination) {
                        if (AgentInRange()) {
                            RunDelegate();
                            CheckForDestination = false;
                        }
                    }

                    break;
                default:
                    break;
            }

            
        }

        void debugPathing() {
            //Debug.Log(PlayerNavAgent.isPathStale);
            Debug.Log(PlayerNavAgent.pathStatus);
            //Debug.Log(PlayerNavAgent.remainingDistance);
            //Debug.Log(PlayerNavAgent.pathPending);
            //Debug.Log(PlayerNavAgent.destination);

        }

        /// <summary>
        /// When clicking on terrain, the terrain state gets activated catching mouse down events for walking
        /// </summary>
        public void SetTerrainState() {
            QueuedEvent = null;
            selectedTarget = Targets.Terrain;
            
            if (PlayerNavAgent == null) {
                Debug.Log("Drag the playercontroller script on your player object");
            }
            else {
                ChangeStoppingDistance();
                PlayerNavAgent.SetDestination(GetMousePosition());
            }
        }

        /// <summary>
        /// When following the mouse we set the stopping distance to 0 so the player keeps walking no matter how close it is to the player
        /// </summary>
        void ChangeStoppingDistance() {
            if (Input.GetMouseButtonDown(0)) {
                PlayerNavAgent.stoppingDistance = 0;
            }
        }
        
        /// <summary>
        /// set the stopping distance back to it's original value
        /// </summary>
        void ResetStoppingDistance() {
            if (Input.GetMouseButtonUp(0)) {
                PlayerNavAgent.stoppingDistance = stoppingDistance;
            }
        }

        //Set destination of nav agent to mouse position
        public void FollowMouse() {
            if (Input.GetMouseButton(0)) {
                if (PlayerNavAgent == null) {
                    Debug.Log("Drag the playercontroller script on your player object");
                }
                else {
                    PlayerNavAgent.SetDestination(GetMousePosition());
                }
            }
        }

        /// <summary>
        /// Call the queued delegate if it's not null
        /// </summary>
        void RunDelegate() {
            if (QueuedEvent != null) {
                QueuedEvent();
            }

            QueuedEvent = null;
        }

        /// <summary>
        /// Assign the target and events, change the state and calculate the path to the target
        /// </summary>
        /// <param name="queuedEvent"></param>
        /// <param name="targetTransform"></param>
        public void QueueInteraction(EventHandler.Events queuedEvent, Transform targetTransform) {
            QueuedEvent = queuedEvent;
            TargetTransform = targetTransform;
            CheckForDestination = false;
            selectedTarget = Targets.InteractableObject;

            CalculatePath(targetTransform.position);
        }

        /// <summary>
        /// When looting an item out of range, a path gets calculated and assigned if possible
        /// </summary>
        /// <param name="destination"></param>
        void CalculatePath(Vector3 destination) {
            if (PlayerNavAgent == null) {
                Debug.Log("Drag the playercontroller script on your player object");
                return;
            }

            NavMeshPath path = new NavMeshPath();
            PlayerNavAgent.CalculatePath(destination, path);

            switch (path.status) {
                case NavMeshPathStatus.PathComplete:
                    //The object is reachable without any problems
                    //Enable checking for distance so the interaction gets called when the target is reached
                    PlayerNavAgent.SetPath(path);

                    if (AgentInRange()) {
                        RunDelegate();
                    }
                    else {
                        CheckForDestination = true;
                    }

                    break;
                case NavMeshPathStatus.PathPartial:
                    //Path partial means on a navmesh, but not reachable
                    //We do set a path, but don't call the delegate when the object is reached
                    //f.e. player needs to climb up to get the object
                    PlayerNavAgent.SetPath(path);
                    break;
                case NavMeshPathStatus.PathInvalid:
                    //Invalid means not on the navmesh
                    //So we don't set paths or call delegates
                    Debug.Log("Target not reachable");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Checks if the agent's remaining distance is lower then or equal then the interaction range
        /// </summary>
        /// <returns></returns>
        bool AgentInRange() {
            if (!PlayerNavAgent.pathPending) {
                //Debug.Log("no path pending");
                if (PlayerNavAgent.hasPath) {
                    //Debug.Log("has path");
                    if (PlayerNavAgent.remainingDistance <= InteractRange) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }


        RaycastHit hit;
        Ray ray;
        Vector3 mousePosition;

        /// <summary>
        /// Gets mouse position and converts it to world space
        /// </summary>
        public Vector3 GetMousePosition() {
            if (!cameraSet) {
                SetCamera();
            }

            ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 200)) {
                mousePosition = hit.point;
                return mousePosition;
            }

            return PlayerTransform.position;
        }
    }
}