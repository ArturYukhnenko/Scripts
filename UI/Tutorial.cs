using System.Text;
using TMPro;
using UnityEngine;

namespace UI {
    public class Tutorial : MonoBehaviour {

        [SerializeField] private GameObject tutorialPopup;
        private int _tutorialStage;
        private bool _isTutorialActive;

        public GameObject TutorialPopup => tutorialPopup;

        public int TutorialStage => _tutorialStage;

        public bool IsTutorialActive => _isTutorialActive;

        public void StartTutorial() {
            _tutorialStage = 0;
            _isTutorialActive = true;
            GameObject tutorial = Instantiate(tutorialPopup, this.gameObject.transform);
            tutorial.GetComponentInChildren<TextMeshPro>().text = "Hello!Let me greet you in your cafe!";
        }

        public void NextStep(int stepNum) {
            _tutorialStage = stepNum;
            GameObject tutorial = Instantiate(tutorialPopup);
            switch (stepNum) {
                case 1:
                    string s1 = "First of all, lets take a look on your cafe!\n" +
                               "Check your menu.";
                    tutorial.GetComponentInChildren<TextMeshPro>().text = s1;
                    break;
                case 2:
                    string s2 = "Now it is empty, because you don't have any equipment bought. Let's change it!\n" +
                               "Open shop and enter to the furniture catalog, choose coffee machine and place it!";
                    tutorial.GetComponentInChildren<TextMeshPro>().text = s2;
                    break;
                case 3:
                    string s3 = "Now open menu one more time and activate espresso.";
                    tutorial.GetComponentInChildren<TextMeshPro>().text = s3;
                    break;
                case 4:
                    string s4 = "So, let's move further.\n" +
                                "Open shop again, but now let's buy some ingredients to cook.\n" +
                                "Buy 10 coffee and 5 milk";
                    tutorial.GetComponentInChildren<TextMeshPro>().text = s4;
                    break;
                case 5:
                    string s5 = "And now you can start your first working day!\n" +
                                "Press on the start day button!";
                    tutorial.GetComponentInChildren<TextMeshPro>().text = s5;
                    break;
                case 6:
                    string s6 = "Here, it is your first customer!\n" +
                                "Wait till he place order and cook for him!\n" +
                                "Open coffee machine and click on espresso icon to prepare it!";
                    tutorial.GetComponentInChildren<TextMeshPro>().text = s6;
                    break;
                case 7:
                    string s7 = "Now you are ready to work!\n" +
                                "Good luck and make your cafe the best!";
                    tutorial.GetComponentInChildren<TextMeshPro>().text = s7;
                    _isTutorialActive = false;
                    break;
            }
        }
    }
}
