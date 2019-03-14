using UnityEngine;

namespace Manager
{
    public class WebManager : MonoBehaviour
    {
    
        public void OpenSurvey()
        {
            Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSc0UTuUT4QjenjZlxhr3EHV2H74-Y_sYypfYSXq8r3BE_PjTA/viewform?usp=sf_link");
        }
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
