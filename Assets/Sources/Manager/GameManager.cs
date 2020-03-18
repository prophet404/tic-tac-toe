﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TicTakToe.Utils;
using TicTakToe.Player;
using TicTakToe.Ui;

namespace TicTakToe.Manager
{
    public class GameManager : MonoBehaviour
    {
        
        [SerializeField]
        private GameObject startScreen; 

        [SerializeField]
        private GameObject mainMenu;

        [SerializeField]
        private GameObject gameBoard;

        [SerializeField]
        private GameObject gameControllerGo;

        [SerializeField]
        private GameObject facebookManagerGo;

        [SerializeField]
        private GameObject playerInfo;

        [SerializeField]
        private Text playerxName;

        [SerializeField]
        private Text playeroName;

        [SerializeField]
        private Image playerXImage;

        [SerializeField]
        private Image playerOImage;

        [SerializeField]
        private GameObject AiGO;

        [SerializeField]
        private GameObject homeBtn;

        [SerializeField]
        private GameObject resetBtn;

        [SerializeField]
        private GameObject soundBtn;

        [SerializeField]
        private GameObject scoreBtn;

        [SerializeField]
        private GameObject scoreBoard;

        public Button FbBtn;   

        public GameController GameController;

        public static GameManager Instance;

        public GamePlayer player_x;

        public GamePlayer player_o;

        private PlayerAi player_Ai;

        private GameType gameType;

        public GameType GameType
        {
            get {return gameType;}
        }

        public GameObject FacebookManagerGO
        {
            get {return facebookManagerGo;}
        }

        private GridCell lastSelectedCell = null;


        private void Start()
        {
            Instance = this;
            GameController = gameControllerGo.GetComponent<GameController>();
        }

        public void ShowStartScreen ()
        {

        }

        public void ShowMenuScreen ()
        {
            startScreen.SetActive (false);
            mainMenu.SetActive (true);
            playerInfo.SetActive (false);
            homeBtn.SetActive (false);
            resetBtn.SetActive (false);
            gameBoard.SetActive (false);
            GameController.gameObject.SetActive (false);
            scoreBtn.SetActive (false);
        }

        public void ShowResultScreen ()
        {

        }

        public void SinglePlayerSelected ()
        {
            gameType = GameType.SINGLE_PLAYER;
            AiGO.SetActive (true);
            player_x = new User ();
            player_o = AiGO.GetComponent<PlayerAi>();

            player_x.SetPlayerData (FacebookManagerGO.GetComponent<FacebookManager>().UserName, 
                                    FacebookManagerGO.GetComponent<FacebookManager>().ProfilePic);

            player_x.SetPlayerData ("AI", 
                                    null);
            startGame ();
        }

        public void MultiPlayerSelected ()
        {
            gameType = GameType.MULTI_PLAYER;
            player_x = new User ();
            player_o = new User ();

            player_x.SetPlayerData (FacebookManagerGO.GetComponent<FacebookManager>().UserName, 
                                    FacebookManagerGO.GetComponent<FacebookManager>().ProfilePic);

            player_x.SetPlayerData ("Player_2", 
                                    null);
            startGame ();
        }

        private void startGame ()
        {
            GameManager.Instance.SaveDataForKey (GameManager.Instance.player_x.Name + "_Score", (int)GameManager.Instance.player_x.WinType);
            GameManager.Instance.SaveDataForKey (GameManager.Instance.player_o.Name + "_Score", (int)GameManager.Instance.player_o.WinType);

            mainMenu.SetActive (false);
            gameBoard.SetActive (true);
            playerInfo.SetActive (true);
            homeBtn.SetActive (true);
            resetBtn.SetActive (true);
            GameController.gameObject.SetActive (true);
            scoreBtn.SetActive (true);
        }

        public void OnCellClicked (GridCell cell)
        {
            if (GameController.CurrentTurn == PlayerSymbol.X)
            {
                player_x.onPlayerClick (cell);
            }
            else
            {
                player_o.onPlayerClick (cell);
            }
            
            lastSelectedCell = cell;
        }

        public void EnableFacebookManagerGameobject ()
        {
            if (!facebookManagerGo.activeInHierarchy)
            {
                facebookManagerGo.SetActive(true);
            }
        }

        private void ResetPlayerData ()
        {
            player_o.CheckedValue = 0;
            player_x.CheckedValue = 0;
        }

        public void ResetGame ()
        {
            GameController.ResetGameBoard ();
            ResetPlayerData ();
        }

        public void OnHomeButtonClicked ()
        {
            ResetGame ();
            ShowMenuScreen ();
        }

        public void SaveDataForKey (string key, int value)
        {
            PlayerPrefs.SetInt (key , value);
        }

        public void OnScoreBoradClicked ()
        {
            scoreBoard.SetActive (true);
        }

        public void OnCloseButtonClicked ()
        {
            Application.Quit ();
        }
    }
}