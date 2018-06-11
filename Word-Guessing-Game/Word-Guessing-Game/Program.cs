using System;
using System.IO;
using System.Text;
using System.Linq;

namespace Word_Guessing_Game
{
    public class Program
    {
        public static string path = "../../../MyFile.txt";

        public static void Main(string[] args)
        {
            try
            {
                //Initial words that enter the file
                string[] initialWords = new string[] { "Pizza", "Icecream", "Chocolate", "Apple", "Salad" };
                //Creates a file with the initial words
                CreateFile(initialWords);

                MainMenu();
                Console.WriteLine("Thanks for Playing!");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Creates a file with a given array of strings. Prints each item in the array on a new line
        /// </summary>
        /// <param name="words"> takes in a string of arrays </param>
        public static string CreateFile(string[] words)
        {
            try
            {
                if (!File.Exists((path)))
                {
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        foreach (string word in words)
                        {
                            sw.WriteLine(word);
                        }
                    }
                }
                return "File has been made";

            }
            catch (Exception)
            {
                return "error";
            }
        }

        public static string DeleteFile()
        {
            File.Delete(path);
            return "file deleted";
        }

        /// <summary>
        /// The main menu wrapped in a while loop until user inputs 3
        /// </summary>
        public static void MainMenu()
        {
            try
            {
                bool flag = true;
                while (flag)
                {
                    Console.WriteLine("Welcome to the Guessing Game");
                    Console.WriteLine("Please Choose an option");
                    Console.WriteLine("1) Play Game");
                    Console.WriteLine("2) Admin");
                    Console.WriteLine("3) Exit");
                    Int32.TryParse(Console.ReadLine(), out int userInput);
                    switch (userInput)
                    {
                        case 1:
                            PlayGame();
                            break;
                        case 2:
                            AdminView();
                            break;
                        default:
                            flag = false;
                            break;
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// The main logic for the game. 
        /// </summary>
        public static void PlayGame()
        {
            //From the random class instantiate an object and give it the reference type of random
            Random randomObj = new Random();

            //Gather all the words in the text and store it in a string array
            string[] myText = File.ReadAllLines(path);

            //using the method next in the random object, inherited from Random class, store the random integer that is returned as number
            int randomNum = randomObj.Next(0, myText.Length);

            //Using the random number we just got find the word within the string array
            string mysteryWord = myText[randomNum];

            //Create a new string array the size of the length of the mystery word
            string[] displayWord = new string[mysteryWord.Length];

            //Empty String to hold user guesses
            string guessedLetter = "";

            for (int i = 0; i < mysteryWord.Length; i++)
            {
                displayWord[i] = " _ ";
            }

            DisplayCurrentWord(displayWord);

            bool correct = false;
            while (!correct)
            {
                Console.WriteLine("Guess a Letter");
                string letter = Console.ReadLine();

                if (letter != null && (mysteryWord.ToLower().Contains(letter.ToLower())) && !guessedLetter.Contains(letter.ToLower()))
                {
                    for (int i = 0; i < mysteryWord.Length; i++)
                    {
                        if(mysteryWord[i].ToString().ToLower() == letter)
                        {
                            displayWord[i] = letter;
                        }
                    }
                }
                guessedLetter += letter.ToLower();

                DisplayCurrentWord(displayWord);

                Console.WriteLine($"The guessed letters are: {guessedLetter}");
                
                if (!displayWord.Contains(" _ "))
                {
                    correct = true;
                    Console.WriteLine("Congrats! You did it!");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// To display in console the current status of the mystery word
        /// </summary>
        /// <param name="word"> Takes in an array of string (each string is a letter or _)</param>
        public static void DisplayCurrentWord(string[] word)
        {
            foreach (string item in word)
            {
                Console.Write(item);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// The same logic behind our game. Created this method to test if the logic is sound.
        /// </summary>
        /// <param name="word">The string we will be checking against</param>
        /// <param name="letter">The letter we will be checking the string for</param>
        /// <param name="alreadyGuessed">letters already guessed</param>
        /// <returns></returns>
        public static bool CheckIfLetterIsInWord(string word, string letter, string alreadyGuessed)
        {
            if (letter != null && (word.ToLower().Contains(letter.ToLower())) && !alreadyGuessed.Contains(letter.ToLower()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// AdminView wrapped in a while loop until chosen 4 or wrong input 
        /// </summary>
        public static void AdminView()
        {
            try
            {
                bool flag = true;
                while (flag)
                {
                    Console.WriteLine("Admin View: Please Select an Option");
                    Console.WriteLine("1) View List of Words");
                    Console.WriteLine("2) Add a Word");
                    Console.WriteLine("3) Delete a Word");
                    Console.WriteLine("4) Back to Main Menu");
                    Int32.TryParse(Console.ReadLine(), out int userInput);
                    switch (userInput)
                    {
                        case 1:
                            ViewList();
                            break;
                        case 2:
                            Console.WriteLine("What word would you like to add?");
                            string insertWordIs = Console.ReadLine();
                            string addedWord = AddWord(insertWordIs);
                            Console.WriteLine(addedWord);
                            Console.ReadLine();
                            break;
                        case 3:
                            Console.WriteLine("What word would you like to delete?");
                            string deleteWordIs = Console.ReadLine();
                            string rtnWord = DeleteWord(deleteWordIs);
                            Console.WriteLine(rtnWord);
                            Console.ReadLine();
                            break;
                        default:
                            flag = false;
                            Console.Clear();
                            break;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// To go read through all the possible words in the text and display on console
        /// </summary>
        /// <returns> returns the read lines as one word </returns>
        public static string ViewList()
        {
            string combined = "";

            string[] myText = File.ReadAllLines(path);

            foreach (string word in myText)
            {
                Console.WriteLine(word);
                combined += word;
            }
            Console.WriteLine();

            return combined;
        }

        /// <summary>
        /// Adds a user inputted word to the list of possible words used in the game. 
        /// </summary>
        /// <param name="word"> takes in a string that the user inputted </param>
        /// <returns> A string to confirm addition </returns>
        public static string AddWord(string word)
        {
            try
            {
                string[] myText = File.ReadAllLines(path);

                foreach (string item in myText)
                {
                    // if the word they enter and any of the words already existing in the file matches
                    // It will return nothing, If it doesnt match it will run the code to write the entered word
                    if (string.Equals(item, word, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return $"{word} not found";
                    }
                }

                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(word);
                }
                return $"{word} was inserted";

            }
            catch (Exception)
            {
                return "error";
            }
        }

        /// <summary>
        /// Deletes the user inputted from the list of possible word to be used in the game. 
        /// It actaully makes a new file without the user inputted word and replaces the old file with the new one
        /// </summary>
        /// <param name="word"> the user inputted string to be deleted from the list </param>
        /// <returns> a string to confirm it has finished </returns>
        public static string DeleteWord(string word)
        {
            try
            {

                string[] myText = File.ReadAllLines(path);

                string newPath = "../../../newFile.txt";

                string backupPath = "../../../bpFile.txt";

                using (StreamWriter sw = new StreamWriter(newPath))
                {
                    foreach (string item in myText)
                    {
                        if (!(string.Equals(item, word, StringComparison.CurrentCultureIgnoreCase)))
                        {
                            sw.WriteLine(item);
                        }
                    }
                }

                File.Replace(newPath, path, backupPath);
                File.Delete(backupPath);

                return $"{word} has been deleted";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                return "Error";
            }
        }
    }
}
