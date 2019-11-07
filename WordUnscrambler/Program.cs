using System;
using System.Collections.Generic;
using System.Linq;
using WordUnscrambler.Data;
using WordUnscrambler.Workers;

namespace WordUnscrambler
{
    class Program
    {
        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();

        static void Main(string[] args)
        {

            try
            {
                //do-while bool value that is being compared
                bool continueWordUnscrambled = true;
                do
                {
                    //Option for file or manual
                    Console.WriteLine(Constants.OptionsOnHowToEnterScrambledWords);
                    var option = Console.ReadLine() ?? string.Empty;
                    switch (option.ToUpper())
                    {
                        //Case for FILE
                        case Constants.File:
                            //Asking for file path
                            Console.Write(Constants.EnterScrambledWordsViaFile);
                            ExecuteScrambledWordsInFileScenario();
                            break;
                        //Case for MANUAL
                        case Constants.Manual:
                            //Asking for scrambled words manually
                            Console.Write(Constants.EnterScrambledWordsManually);
                            ExecuteScrambledWordsInManualScenario();
                            break;
                        default:
                            Console.Write(Constants.EnterScrambledWordsOptionNotRecognized);
                            break;
                    }
                    var continueDecision = string.Empty;
                    do
                    {
                        Console.WriteLine(Constants.OptionsOnContinuingTheProgram);
                        continueDecision = (Console.ReadLine() ?? string.Empty);
                    } while (!continueDecision.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase) &&
                             !continueDecision.Equals(Constants.No, StringComparison.OrdinalIgnoreCase));
                    //Will make continueWordUnscrambled FALSE if user types N
                    continueWordUnscrambled = continueDecision.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase);

                } while (continueWordUnscrambled);
            }
            catch(Exception ex)
            {
                Console.WriteLine(Constants.ErrorProgramWillBeTerminated + ex.Message);
            }
            Console.ReadLine();

            

        }
        private static void ExecuteScrambledWordsInFileScenario()
        {
            try
            {
                var fileName = Console.ReadLine() ?? string.Empty;
                string[] scrambledWords = _fileReader.Read(fileName);
                DisplayMatchedUnscrambledWords(scrambledWords);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.ErrorScrambledWordsCannotBeLoaded + ex.Message);

            }
        }
        private static void ExecuteScrambledWordsInManualScenario()
        {
            var manualInput = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = manualInput.Split(",");
            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private static void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            string[] wordList = _fileReader.Read(Constants.WordListFileName);

            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordList);

            if (matchedWords.Any())
            {
                foreach(var matchedWord in matchedWords)
                {
                    Console.WriteLine(Constants.MatchFound, matchedWord.ScrambledWord, matchedWord.Word);
                }
            }
            else
            {
                Console.WriteLine(Constants.MatchNotFound);
            }
        }
    }

    }
