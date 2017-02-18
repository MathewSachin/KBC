namespace KBC
{
    class Question
    {
        public Question(string Statement, string OptionA, string OptionB, string OptionC, string OptionD, int CorrectOption)
        {
            this.Statement = Statement;

            Options = new[] { OptionA, OptionB, OptionC, OptionD };
            
            this.CorrectOption = CorrectOption;
        }

        public string Statement { get; }

        public string[] Options { get; }

        public int CorrectOption { get; }
        
        public static string[] Amounts { get; } =
        {
            "5,000", "10,000", "20,000", "40,000", "80,000", "1,60,000", "3,20,000", "6,40,000", "12,50,000", "25,00,000", "50,00,000", "1 Crore", "3 Crore", "5 Crore", "7 Crore"
        };

        public static int[] SafeLevels { get; } = { 3, 7, 14 };        
    }
}