using System;
using Xunit;
using Word_Guessing_Game;

namespace Word_Guess_Test
{
    public class UnitTest1
    {
        string[] test1 = new string[] { "hello", "hi", "goodbye" };

        [Fact]
        public void TestFileCreate()
        {
            Assert.Equal("File has been made", Program.CreateFile(test1));
            Program.DeleteFile();
        }

        [Fact]
        public void TestFileUpdateWord()
        {
            Program.CreateFile(test1);
            Assert.Equal("goodevening was inserted", Program.AddWord("goodevening"));
            Program.DeleteFile();
        }

        [Fact]
        public void TestFileDeleteWord()
        {
            Program.CreateFile(test1);
            Program.AddWord("goodevening");
            Assert.Equal("goodevening has been deleted", Program.DeleteWord("goodevening"));
            Program.DeleteFile();
        }

        [Fact]
        public void TestRetriveAllWords()
        {
            Program.CreateFile(test1);
            Assert.Equal("hellohigoodbye", Program.ViewList());
            Program.DeleteFile();
        }

        [Fact]
        public void TestFileDelete()
        {
            Program.CreateFile(test1);
            Assert.Equal("file deleted", Program.DeleteFile());
        }

        [Theory]
        [InlineData("abcdef", "B", "c", true)]
        [InlineData("abcdef", "A", "c", true)]
        [InlineData("abcdef", "c", "c", false)]
        [InlineData("abcdef", "bAd", "c", false)]
        [InlineData("Abcdef", "a", "c", true)]
        public void TestLogicInGame(string word, string letter, string guessed, bool expected)
        {
            Assert.Equal(expected, Program.CheckIfLetterIsInWord(word, letter, guessed));
        }


    }
}
