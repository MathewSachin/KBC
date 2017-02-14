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
            new Question("Which of these sounds would you associate with the heart?", "Tring Tring", "Tap Tap", "Click Click", "Dhak Dhak", 4),
            new Question("Who is the 'Bharat Ka Veer Putra' according to the title of 2013 TV Series?", "Tipu Sultan", "Chandragupta Maurya", "Maharana Pratap", "Ashok", 3),
            new Question("In 2013, where did the natural calamity known as Himalayan tsunami occur?", "Uttrakhand", "Arunachal Pradesh", "Jammu and Kashmir", "Sikkim", 1),
            new Question("In the Ramayana, Which demon impersonated Rama's voice, screaming, 'Lakshman! Help Me!'?", "Surpanakha", "Khara", "Maricha", "Dushana", 3),
            new Question("Who is the only leader to be elected Prime Minister of Pakistan three times", "Syed Yousaf Raza Gillani", "Benazir Bhutto", "Liaqat Ali Khan", "Nawaz Sharif", 4),
            new Question("The black widow, which eats the male counterpart after mating, is a female species of which animal?", "Sloth", "Ant", "Spider", "Termite", 3),
            new Question("Douglas Engelbert, who passed away in 2013, is credited as the inventor of which of these products?", "Mobile Phone", "Computer Mouse", "Bluetooth Mouse", "Digital Camera", 2),
            new Question("In 1850, the first experimental electric telegraph line in India was set up between Calcutta and which place?", "Diamond Harbour", "Darjeeling", "Murshidabad", "Dhaka", 1),
            new Question("Which of the following person has not walked on the moon?", "Charles Duke", "James A Lovell", "Alan Bean", "Pete Conrad", 2),
            new Question("Which team retained the ashes Trophy in 2013?", "Australia", "South Africa", "West Indies", "England", 4),
            new Question("With which of these cards would you associate the phrase 'Aam Aadmi ka Adhikaar'?", "PAN Card", "Voter ID Card", "AADHAR Card", "Ration Card", 3),
            new Question("With which of these states do Chhattisgarh, Jharkhand and Andhra Pradesh all share their borders?", "Madhya Pradesh", "Odisha", "Bihar", "Uttar Pradesh", 2),
            new Question("The first world championship of what sport is planned to be held in 2017, though the game has been played since 1877?", "Test Cricket", "Rugby Union", "Kabaddi", "Carrom", 1),
            new Question("Which is the largest living species of tortoise in the world, which may weigh about 400 kg?", "Sulcata Tortoise", "Grenada Tortoise", "Golden Greek Tortoise", "Galapagos Tortoise", 4),
            new Question("According to legend, which of these Rishis regained his youth by a celestial favour?", "Agastya", "Durvasa", "Chyavana", "Charaka", 3),
            new Question("Which of these is a term for a place where people gather for shayari and ghazals?", "Rukhsar", "Mushaira", "Shikara", "Mahabara", 2),
            new Question("Which of these is not a recommended mineral in human diet?", "Strontium", "Potassium", "Iron", "Calcium", 1),
            new Question("Who was the president of India at the turn of the century, as the 20th century became the 21st century?", "K R Narayan", "A P J Abdul Kalam", "R Venkataraman", "Shankar Dayal Sharma", 1),
            new Question("In the Mughal era, which of these harbours was also known as 'Babul Mecca' and 'Meccaidwar'?", "Bharuch", "Surat", "Porbandar", "Khambat", 2),
            new Question("After whom is the annual award, given by the Govt of India to an outstanding handloom weaver, named?", "Acharya Vinoba Bhave", "Sant Kabir", "Mahatma Gandhi", "Gul Ahmed", 2),
            new Question("Damascus is the capital of which country?", "Tunisia", "Jordan", "Syria", "Lebanon", 3),
            new Question("To respect whose word did the five Pandavas marry Draupadi?", "Krishna", "Indra", "Kunti", "Madri", 3),
            new Question("Who is the only Indian to have won a medal in Women's Singles at the World Badminton Championships?", "Jwala Gupta", "P V Sindhu", "Saina Nehwal", "Ashwini Ponnappa", 2),
            new Question("From which country did India buy an aircraft carrier which was renamed as INS Vikramaditya?", "France", "Russia", "England", "Germany", 2),
            new Question("Which of these freedom fighters was the English language secretary to Mahatma Gandhi for 16 years?", "Hansa Mehta", "Rajkumari Amrit Kaur", "Sarojini Naidu", "Sushila Nayyar", 2)
        };
    }
}