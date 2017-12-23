using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class GeoguessVRGame : MonoBehaviour
    {
        public static int _score = 0, _round = 0;
        public static bool _gameStarted = false;
        static bool tutorialVisible = false;

        public static GameObject
            _gameLogo,
            _confirmLocationText,
            _confirmLocationImage,
            _gameScore,
            _gameScoreText,
            _gameScoreTextBg,
            _gameScoreBg,
            _scoreBackground,
            _textbox_youwerein,
            _bgtextbox_youwerein,
            _textbox_location,
            _bgtextbox_location,
            _textbox_accuracy,
            _bgtextbox_accuracy,
            _textbox_accuracydistance,
            _bgtextbox_accuracydistance,
            _textbox_youearned,
            _bgtextbox_youearned,
            _textbox_pointsearned,
            _bgtextbox_pointsearned,
            _textbox_stepInfoPress,
            _textbox_stepInfoA,
            _textbox_stepInfoToContinue,
            _textbox_startInfoPress,
            _textbox_startInfoA,
            _textbox_startInfoToStart,
            _controllerTutorial,
            _textbox_introText,
            _textbox_roundText,
            _textboxbg_roundText,
            _textbox_round,
            _textboxbg_roundnotext,
            _textbox_finalround;


        public static GMAPS.Location currentLocation = null;
        public static GMAPS.Location nextLocation = null;

        [SerializeField]
        Transform _globeTransform;

        void Start()
        {
            initializeGameObjects();
            changeScoreVisibility(false);
            changeRoundVisibility(false);
            changeConfirmVisibility(false);
            changeIntroSplashVisibility(true);
            changeFinalRoundVisibility(false);

            currentLocation = GMAPS.GetNewStreetViewCoordinates();
            TextureHelper.cacheTextureSkybox(currentLocation);

        }

        public static void madeGuess(double distanceTo)
        {
            

            int formattedDistance = (int)distanceTo;
            _textbox_location.GetComponent<Text>().text = currentLocation.Name.ToString();
            _textbox_accuracydistance.GetComponent<Text>().text = formattedDistance.ToString() + " KM";

            int pointsEarned = scoreFromDistance(distanceTo);
            _score += pointsEarned;
            _gameScore.GetComponent<Text>().text = _score.ToString();
            _textbox_pointsearned.GetComponent<Text>().text = pointsEarned.ToString() + "PTS";

            _round += 1;

            _textbox_roundText.GetComponent<Text>().text = _round.ToString() + "/3";
            if (_round == 3)
            {
                changeFinalRoundVisibility(true);
                _textbox_finalround.GetComponent<Text>().text = "YOUR FINAL SCORE: " + _score.ToString();
                _gameScore.GetComponent<Text>().text = 0.ToString();
                _score = 0;
                _round = 0;
            }


            changeConfirmVisibility(false);
            changeRoundVisibility(true);

            //toon totale score nieuwe player

            



        }



        public static int scoreFromDistance(double distance)
        {
            return (int)(5000 / distance);
        }

        void initializeGameObjects()
        {
            // Initializing game objects by using Find on their name/id
            _gameScore = GameObject.Find("game_score");
            _gameScoreText = GameObject.Find("game_score_text");
            _gameScoreTextBg = GameObject.Find("scorebox_text_background");
            _gameScoreBg = GameObject.Find("scorebox_background");
            _gameLogo = GameObject.Find("game_logo");
            _confirmLocationText = GameObject.Find("confirm_location_text");
            _scoreBackground = GameObject.Find("score_background");
            _confirmLocationImage = GameObject.Find("confirm_location_image");

            _textbox_youwerein = GameObject.Find("textbox_youwerein");
            _bgtextbox_youwerein = GameObject.Find("bgtextbox_youwerein");
            _textbox_location = GameObject.Find("textbox_location");
            _bgtextbox_location = GameObject.Find("bgtextbox_location");
            _textbox_accuracy = GameObject.Find("textbox_accuracy");
            _bgtextbox_accuracy = GameObject.Find("bgtextbox_accuracy");
            _textbox_accuracydistance = GameObject.Find("textbox_accuracydistance");
            _bgtextbox_accuracydistance = GameObject.Find("bgtextbox_accuracydistance");
            _textbox_youearned = GameObject.Find("textbox_youearned");
            _bgtextbox_youearned = GameObject.Find("bgtextbox_youearned");
            _textbox_pointsearned = GameObject.Find("textbox_pointsearned");
            _bgtextbox_pointsearned = GameObject.Find("bgtextbox_pointsearned");

            _textbox_stepInfoPress = GameObject.Find("textbox_stepInfoPress");
            _textbox_stepInfoA = GameObject.Find("textbox_stepInfoA");
            _textbox_stepInfoToContinue = GameObject.Find("textbox_stepInfoToContinue");

            _textbox_startInfoPress = GameObject.Find("textbox_startInfoPress");
            _textbox_startInfoA = GameObject.Find("textbox_startInfoA");
            _textbox_startInfoToStart = GameObject.Find("textbox_startInfoToStart");

            _controllerTutorial = GameObject.Find("controllerTutorial");
            _textbox_introText = GameObject.Find("textbox_introText");

            _textbox_roundText  =GameObject.Find("textbox_roundtext");
            _textboxbg_roundText    =GameObject.Find("textboxbg_roundText");
            _textbox_round       =   GameObject.Find("textbox_round");
            _textboxbg_roundnotext  =  GameObject.Find("textboxbg_roundnotext");
            _textbox_finalround   = GameObject.Find("textbox_finalround");

        }


        public static void changeFinalRoundVisibility(bool isVisible)
        {
            _textbox_finalround.SetActive(isVisible);
        }

        public static void changeConfirmVisibility(bool isVisible)
        {
            _confirmLocationText.SetActive(isVisible);
            _scoreBackground.SetActive(isVisible);
            _confirmLocationImage.SetActive(isVisible);
        }

        public static void changeScoreVisibility(bool isVisible)
        {
            _gameScore.SetActive(isVisible);
            _gameScoreText.SetActive(isVisible);
            _gameScoreTextBg.SetActive(isVisible);
            _gameScoreBg.SetActive(isVisible);
        }

        public static void changeIntroSplashVisibility(bool isVisible)
        {
            _scoreBackground.SetActive(isVisible);
            _gameLogo.SetActive(isVisible);
            _textbox_startInfoPress.SetActive(isVisible);
            _textbox_startInfoA.SetActive(isVisible);
            _textbox_startInfoToStart.SetActive(isVisible);
            _textbox_introText.SetActive(isVisible);
            _controllerTutorial.SetActive(isVisible);
        }



        public static void changeRoundVisibility(bool isVisible, int points = 0, float accuracy = 0.0f)
        {
            _scoreBackground.SetActive(isVisible);
            _textbox_youwerein.SetActive(isVisible);
            _bgtextbox_youwerein.SetActive(isVisible);
            _textbox_location.SetActive(isVisible);
            _bgtextbox_location.SetActive(isVisible);
            _textbox_accuracy.SetActive(isVisible);
            _bgtextbox_accuracy.SetActive(isVisible);
            _textbox_accuracydistance.SetActive(isVisible);
            _bgtextbox_accuracydistance.SetActive(isVisible);
            _textbox_youearned.SetActive(isVisible);
            _bgtextbox_youearned.SetActive(isVisible);
            _textbox_pointsearned.SetActive(isVisible);
            _bgtextbox_pointsearned.SetActive(isVisible);

            _textbox_roundText.SetActive(isVisible);
            _textboxbg_roundText.SetActive(isVisible);
            _textbox_round.SetActive(isVisible);
            _textboxbg_roundnotext.SetActive(isVisible);


            _textbox_stepInfoPress.SetActive(isVisible);
            _textbox_stepInfoA.SetActive(isVisible);
            _textbox_stepInfoToContinue.SetActive(isVisible);

        }


        void hideSplash()
        {
            if (_gameStarted) { return; }
            changeIntroSplashVisibility(false);
            changeScoreVisibility(true);
            _gameStarted = true;
        }

        void hideScoreSplash()
        {
            changeFinalRoundVisibility(false);
            RayPointer.hasGuessed = false;
            changeRoundVisibility(false);
            
        }


        // Update is called once per frame
        void Update()
        {
            OVRInput.Update(); // Call before checking the input            
            if (OVRInput.Get(OVRInput.Button.One) && (RayPointer.hasGuessed || !_gameStarted))
            {
                hideSplash();

                if (RayPointer.hasGuessed)
                    hideScoreSplash();

                if (nextLocation != null)
                    currentLocation = nextLocation;

                nextLocation = GMAPS.GetNewStreetViewCoordinates();
                TextureHelper.UpdateSkybox();
                TextureHelper.cacheTextureSkybox(nextLocation);

                _globeTransform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
            }

            if (OVRInput.Get(OVRInput.Button.Three) && _gameStarted && !tutorialVisible)
            {
                tutorialVisible = true;
                _controllerTutorial.SetActive(true);

            }
            if (!OVRInput.Get(OVRInput.Button.Three) && _gameStarted && tutorialVisible)
            {
                tutorialVisible = false;
                _controllerTutorial.SetActive(false);
            }
        }
    }
}