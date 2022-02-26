using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace TP.ATDD
{
    [TestClass]
    public class Ahorcado
    {
        [TestMethod]
        
        
            //Primer test - perder el juego

            //Given "I have selected easy as the difficulty" 

            public void GivenIHaveSelectedEasyAsTheDifficulty()
            {
                

                var txtPalabra = driver.FindElement(By.Id("_secretWord"));
                txtPalabra.SendKeys("puma");
         

                var btnInsertWord = driver.FindElement(By.Id("word"));
                btnInsertWord.SendKeys(Keys.Enter);

               
            }

        //When I enter X as the typedLetter five times"
        public void WhenIEnterXAsTheTypedLetterFiveTimes()
        {
            var letterTyped = driver.FindElement(By.Id("LetterTyped"));
            var btnInsertLetter = driver.FindElement(By.Id("btnInsertLetter"));
            char letterRisked = 's';
            for (int i = 0; i < 6; i++)
            {
                letterTyped.SendKeys(letterRisked.ToString());
                Thread.Sleep(1000);
                btnInsertLetter.SendKeys(Keys.Enter);
                letterRisked++;
            }
        }

        //Then I should be told that I have lost"
        public void ThenIShouldBeToldThatIHaveLost()
        {
            var chancesLeft = driver.FindElement(By.Id("ChancesLeft"));
            var loss = Convert.ToInt32(chancesLeft.GetAttribute("value")) == 0;
            Thread.Sleep(1000);
            Assert.IsTrue(loss);
            Thread.Sleep(1000);
        }

        //Segundo Test - Elegir dificultad no valida
        //Given I have selected extreme as the difficulty
        public void GivenIHaveSelectedExtremeAsTheDifficulty()
        {

        }

        //Then  "It should tell me that the difficulty is invalid"
        public void ThenIShouldBeToldThatTheDifficultyIsInvalid()
        {
            var mensaje = driver.FindElement(By.ClassName("ui-pnotify-text"));
            var invalid = "Dificultad invalida" == mensaje.Text;      
            Assert.IsTrue(invalid);
    
        }

        //Tercer Test - Arriesgar palabra correcta
        //Given I have selected Hard as the difficulty
        public void GivenIHaveSelectedHardAsTheDifficulty()
        {
    
            var txtPalabra = driver.FindElement(By.Id("WordToGuess"));
            txtPalabra.SendKeys("hipopotamo");      

            var btnInsertWord = driver.FindElement(By.Id("btnInsertWord"));
            btnInsertWord.SendKeys(Keys.Enter);

         
        }

        //When I enter Hipopotamo as the word
        public void WhenIEnterHipopotamoAsTheTypedLetter()
        {
            var letterTyped = driver.FindElement(By.Id("LetterTyped"));
            var btnRiskWord = driver.FindElement(By.Id("btnRiskWord"));
            letterTyped.SendKeys("hipopotamo");
            btnRiskWord.SendKeys(Keys.Enter);
        }

        //Then I should be told that I win"
        public void ThenIShouldBeToldThatIWin()
        {
            var txtPalabra = driver.FindElement(By.Id("WordToGuess"));
            var guessingWord = driver.FindElement(By.Id("GuessingWord"));
            var mensaje = driver.FindElement(By.ClassName("ui-pnotify-text"));
            var win = guessingWord.GetAttribute("value").Replace(" ", String.Empty) == txtPalabra.GetAttribute("value");
            var correctMesagge = "Ganaste" == mensaje.Text;         
            Assert.IsTrue(win && correctMesagge);
         
        }

        //Cuarto test - Insertar un numero
        //Given I have selected easy as the difficulty
        public void GivenIHaveSelectedEasyAsTheDificulty()
        {
 

            var txtPalabra = driver.FindElement(By.Id("WordToGuess"));
            txtPalabra.SendKeys("puma");

            var btnInsertWord = driver.FindElement(By.Id("btnInsertWord"));
            btnInsertWord.SendKeys(Keys.Enter);
        }

        //When I enter 2 as the typedLetter one time"
        public void WhenIEnter4AsTheTypedLetterOneTime()
        {
            var letterTyped = driver.FindElement(By.Id("LetterTyped"));
            var btnInsertLetter = driver.FindElement(By.Id("btnInsertLetter"));
            letterTyped.SendKeys("2");
       
            btnInsertLetter.SendKeys(Keys.Enter);
        }

        //Then It should tell me that the letter is invalid
        public void ThenIShouldBeToldThatTheLetterIsInvalid()
        {
            var mensaje = driver.FindElement(By.ClassName("ui-pnotify-text"));
            var invalid = "La letra es invalida" == mensaje.Text;   
            Assert.IsTrue(invalid);
  
        }



    }
}
