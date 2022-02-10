using UnityEngine;

namespace LootLabels {
    /// <summary>
    /// Highlights the mesh, used for child renderers
    /// </summary>
    public class MeshHighlight : MonoBehaviour {

        public Shader outline;      //assign your outline shader    
        public Color mouseoverColor = new Color32(255, 209, 90, 255);

        Shader standardShader;     //standard shader gets collected on awake


        // Use this for initialization
        void Awake() {
            if (GetComponent<Renderer>()) {
                standardShader = GetComponent<Renderer>().material.shader;

                if (outline == null) {
                    Debug.Log("Assign highlight shader to MeshHighlight script");
                }
            }
            else {
                Debug.Log("Can't highlight object, no renderer found");
            }
        }

        public void Highlight() {
            if (GetComponent<Renderer>()) {
                GetComponent<Renderer>().material.shader = outline;
                GetComponent<Renderer>().material.SetColor("_OutlineColor", mouseoverColor);
            }
        }

        public void StopHighlight() {
            if (GetComponent<Renderer>()) {
                GetComponent<Renderer>().material.shader = standardShader;
            }
        }
    }
}