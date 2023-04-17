using System;
using System.Collections;
using LootLocker.Requests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.Games.Core
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private Grader _grader;
        private float _finalScore;
        public float FinalScore
        {
            get => _finalScore;
            set
            {
                _finalScore = value;
                _finalScoreField.text = _finalScore.ToString();
                //var grade = _grader.CalculateGrade((int)_finalScore);
                //_gradeField.text = grade.Text;
                //_gradeDescriptionField.text = grade.Description;
                //_gradeField.color = grade.Color;
                _successSubmit.gameObject.SetActive(false);
                _failSubmit.gameObject.SetActive(false);
                _submitButton.enabled = true;
            } 
        }
        private float _timeBonus;
        public float TimeBonus
        {
            get => _timeBonus;
            set
            {
                _timeBonus = value;
                _timeBonusField.text = _timeBonus.ToString();
            } 
        }
        private float _minigameScore;
        public float MinigameScore
        {
            get => _minigameScore;
            set
            {
                _minigameScore = value;
                _minigameScoreField.text = _minigameScore.ToString();
            } 
        }
        private float _bestFinalTime;
        public float BestFinalTime
        {
            get => _bestFinalTime;
            set
            {
                _bestFinalTime = value;
                _bestTimeField.text = $"{(int)_bestFinalTime / 60:00}:{(int)_bestFinalTime % 60:00}";
            } 
        }
        [SerializeField] private TextMeshProUGUI _minigameScoreField;
        [SerializeField] private TextMeshProUGUI _timeBonusField;
        [SerializeField] private TextMeshProUGUI _finalScoreField;
        [SerializeField] private TextMeshProUGUI _bestTimeField;
        [SerializeField] private TextMeshProUGUI _gradeField;
        [SerializeField] private TextMeshProUGUI _gradeDescriptionField;

        [SerializeField] private TMP_InputField _nameField;
        [SerializeField] private RectTransform _successSubmit;
        [SerializeField] private RectTransform _failSubmit;
        [SerializeField] private Button _submitButton;

        /*
        public void SubmitScore()
        {
            StartCoroutine(SubmitScoreSequence());
        }
        private IEnumerator SubmitScoreSequence()
        {
            bool done = false;
            LootLockerSDKManager.SetPlayerName(_nameField.text, (response) =>
            {
                done = true;
            });
            yield return new WaitWhile(() => done == false);
            string playerID = PlayerPrefs.GetString("PlayerID"); 
            LootLockerSDKManager.SubmitScore(playerID, (int)(_finalTime*100), "time", (r) =>
            {
                if (r.statusCode == 200)
                {
                    _successSubmit.gameObject.SetActive(true);
                }
                else
                {
                    _failSubmit.gameObject.SetActive(true);
                }

                _submitButton.enabled = false;
            });
        }
        */
    }
}