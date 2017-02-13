namespace KBC
{
    class Question
    {
        public Question(string Statement, string OptionA, string OptionB, string OptionC, string OptionD, int CorrectOption)
        {
            this.Statement = Statement;

            this.OptionA = OptionA;
            this.OptionB = OptionB;
            this.OptionC = OptionC;
            this.OptionD = OptionD;

            this.CorrectOption = CorrectOption;
        }

        public string Statement { get; }

        public string OptionA { get; }
        public string OptionB { get; }
        public string OptionC { get; }
        public string OptionD { get; }

        public int CorrectOption { get; }

        public bool Asked { get; set; }

        public static string[] Amounts { get; } =
        {
            "5,000", "10,000", "20,000", "40,000", "80,000", "1,60,000", "3,20,000", "6,40,000", "12,50,000", "25,00,000", "50,00,000", "1 Crore", "3 Crore", "5 Crore", "7 Crore"
        };

        public static Question[] Questions { get; } =
        {
            new Question("What is the final evolved form of Chimchar", "Monferno", "Infernape", "Charizard", "Blaziken", 2),
            new Question("Which of the following is NOT a legendary Pokemon", "Mew", "Ho-oh", "Palkia", "Darkrai", 4),
            new Question("Squirtle eventually evolves into which Pokemon", "Blastoise", "Swampert", "Vaporeon", "Piplup", 1),
            new Question("Which Pokemon has the ability - Static", "Sylveon", "Garados", "Pikachu", "Bayleaf", 3)
        };
    }
}