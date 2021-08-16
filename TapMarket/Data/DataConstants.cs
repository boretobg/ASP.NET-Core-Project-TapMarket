namespace TapMarket.Data
{
    public class DataConstants
    {
        public class Listing
        {
            public const int TitleMinLength = 5;
            public const int TitleMaxLength = 50;
            public const int DescriptionMinLength = 30;
            public const int DescriptionMaxLength = 10000;
        }

        public class Category
        {
            public const int MaxLength = 25;
        }

        public class User
        {
            public const int NameMaxLength = 25;
            public const int NameMinLength = 3;

            public const int AddressMaxLength = 30;
            public const int AddressMinLength = 5;

            public const int CityMaxLength = 20;
            public const int CityMinLength = 3;
        }

        public class Condition
        {
            public const int MaxLength = 10;
        }

        public class HomePage
        {
            public const int SearchInputMaxLength = 50;
            public const int CityInputMaxLength = 20;
        }

        public class HelpPage
        {
            public const int TitleMaxLength = 15;

            public const int DescriptionMaxLength = 500;
            public const int DescriptionMinLength = 5;
        }

        public class Message
        {
            public const int TextMaxLength = 250;
            public const int TextMinLength = 2;

            public const int UserNameMaxLength = 20;
        }
    }
}
