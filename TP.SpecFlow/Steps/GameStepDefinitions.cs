using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using TechTalk.SpecFlow;
using Tp.Models;

namespace TP.SpecFlow.Steps
{
    [Binding]
    public sealed class GameStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;
        private string _baseUrl;

        [BeforeScenario]
        public void TestInitialize()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Drivers";
            _driver = new ChromeDriver(path);
            _baseUrl = "https://tpagiles2021-puma.azurewebsites.net/";
        }

        [AfterScenario]
        public void TestCleanUp()
        {
            _driver.Quit();
        }

        [Given("I have started a new game on (.*) difficulty")]
        public void GivenIHaveStartedANewGame(string difficulty)
        {
            Difficulty formattedDifficulty;
            if (difficulty == Difficulty.Easy.ToString())
                formattedDifficulty = Difficulty.Easy;
            else if (difficulty == Difficulty.Medium.ToString())
                formattedDifficulty = Difficulty.Medium;
            else
                formattedDifficulty = Difficulty.Hard;

            _driver.Navigate().GoToUrl(_baseUrl);
            Thread.Sleep(5000);

            var txtNombre = _driver.FindElement(By.Id("player-name-textbox"));
            txtNombre.SendKeys("Manuel");
            Thread.Sleep(1000);

            var difficultyDropdown = new SelectElement(_driver.FindElement(By.Id("difficulty-dropdown")));
            difficultyDropdown.SelectByValue(((int)formattedDifficulty).ToString());
            Thread.Sleep(1000);

            var btnStartGame = _driver.FindElement(By.Id("btn-start-game"));
            btnStartGame.Click();
            Thread.Sleep(1000);
        }

        [When("I try (.*) as the secret word")]
        public void WhenITrySecretWord(string word)
        {
            var tryWordText = _driver.FindElement(By.Id("try-word-textbox"));
            tryWordText.SendKeys(word);
            Thread.Sleep(1000);

            var tryWordBtn = _driver.FindElement(By.Id("try-word-button"));
            tryWordBtn.Click();
            Thread.Sleep(1000);
        }

        [When("I try (.*) as a letter of the secret word (.*) times")]
        public void WhenITryALetterOfTheSecretWord(char letter, int numberOfTrials)
        {
            var tryLetterText = _driver.FindElement(By.Id("try-letter-textbox"));
            var tryLetterBtn = _driver.FindElement(By.Id("try-letter-button"));

            for (int i = 0; i < numberOfTrials; i++)
            {
                tryLetterText.SendKeys(letter.ToString());
                Thread.Sleep(1000);

                tryLetterBtn.Click();
                Thread.Sleep(1000);
            }
        }

        [Then("I should be told that I (.*)")]
        public void ThenIShouldBeToldThatGameIsOver(string result)
        {
            var gameOverText = _driver.FindElement(By.Id("game-over-text"));
            Assert.IsTrue(gameOverText.Text == (result == "win" ? "You won!" : "You lost!"));
        }

        [Then("I should be told that the (.*) is invalid")]
        public void ThenIShouldBeToldThatTheInputIsInvalid(string input)
        {
            var errorMessage = input == "word" ? "Error: Invalid Word." : "Error: Invalid Letter. It must be an alphabetic character.";

            var errorText = _driver.FindElement(By.Id("error-text"));
            Assert.AreEqual(errorText.Text, errorMessage);
        }

        [Then("I should see (.*) in the result")]
        public void ThenIShouldSeeLetterInTheResult(char letter)
        {
            var resultText = _driver.FindElement(By.Id("result"));
            Assert.IsTrue(resultText.Text.Contains(letter, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
