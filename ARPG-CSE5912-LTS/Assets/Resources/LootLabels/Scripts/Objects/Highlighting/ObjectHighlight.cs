using UnityEngine;

namespace LootLabels {
    /// <summary>
    /// Highlights the object and any children with a ChildHighlight script
    /// </summary>
    public class ObjectHighlight : MonoBehaviour {

        MeshHighlight[] highLightArray = new MeshHighlight[0];

        public bool HighlightThis = false;     //if the parent object contains a renderer you want highlighted, check this
        public Shader outline;      //assign your outline shader    
        public Color mouseoverColor = new Color32(255, 209, 90, 255);    //assign your background image color when highlighted

        Shader standard;     //standard shader gets collected on awake

        // Use this for initialization
        void Awake() {
            if (HighlightThis) {
                if (GetComponent<Renderer>()) {
                    standard = GetComponent<Renderer>().material.shader;

                    if (outline == null) {
                        Debug.Log("Assign highlight shader in the objectHighLight script");
                    }
                }
                else {
                    HighlightThis = false;
                    Debug.Log("No renderer found, turning off HighlightThis");
                }
            }

            if (GetComponentInChildren<Collider>() == null) {
                Debug.Log("Add a collider to your object to detect mouse events");
            }

            highLightArray = GetComponentsInChildren<MeshHighlight>();

            if (!HighlightThis && highLightArray.Length == 0) {
                Debug.Log("Assign the meshHighlight script to child renderers or turn on highlightThis for "+gameObject.name);
            }
        }

        /// <summary>
        /// Highlight the parent if checked and any of it's child renderers
        /// </summary>
        public void HighlightObject() {
            if (HighlightThis) {
                Highlight();
            }

            HighlightMeshChildren();
        }

        /// <summary>
        /// Stop Highlighting the parent if checked and any of it's child renderers
        /// </summary>
        public void StopHighlightObject() {
            if (HighlightThis) {
                StopHighlight();
            }

            StopHighlightMeshChildren();
        }

        /// <summary>
        /// Highlight the mesh's children who have a MeshHighLight script attached to them
        /// </summary>
        public virtual void HighlightMeshChildren() {
            if (highLightArray.Length != 0) {
                foreach (var item in highLightArray) {
                    item.Highlight();
                }
            }
        }

        /// <summary>
        /// Stop highlighting the mesh's children who have a MeshHighLight script attached to them
        /// </summary>
        public virtual void StopHighlightMeshChildren() {
            if (highLightArray.Length != 0) {
                foreach (var item in highLightArray) {
                    item.StopHighlight();
                }
            }
        }

        /// <summary>
        /// Changes the current shader to the outline shader with the supplied color
        /// </summary>
        public void Highlight() {
            if (GetComponent<Renderer>()) {
                //GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                GetComponent<Renderer>().material.shader = outline;
                GetComponent<Renderer>().material.SetColor("_color_emission", mouseoverColor);
                GetComponent<Renderer>().material.SetColor("_color_texture", mouseoverColor);
            }
        }

        /// <summary>
        /// Changes the current shader back to the standard shader
        /// </summary>
        public void StopHighlight() {
            if (GetComponent<Renderer>()) {
                //GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                GetComponent<Renderer>().material.shader = standard;
            }
        }
    }
}