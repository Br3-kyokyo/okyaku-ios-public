using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {

        public static StartScene instance { get; private set; }
        public SimpleSQL.SimpleSQLManager dynamicDBManager;

        public GameObject notesModal;
        public AudioSource audioSource;

        void Awake () {
                instance = this;
                notesModal.SetActive (false);
        }
}
