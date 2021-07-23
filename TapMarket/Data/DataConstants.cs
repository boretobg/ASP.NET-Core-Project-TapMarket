﻿namespace TapMarket.Data
{
    public class DataConstants
    {
        public class Listing
        {
            public const int TitleMinLength = 10;
            public const int TitleMaxLength = 25;
            public const int DescriptionMinLength = 80;
            public const int DescriptionMaxLength = 10000;
        }

        public class Category
        {
            public const int MaxLength = 25;
        }

        public class Customer
        {
            public const int UsernameMaxLength = 20;
            public const int UsernameMinLength = 5;

            public const int AddressMaxLength = 30;
            public const int AddressMinLength = 5;

            public const int CityMaxLength = 20;
            public const int CityMinLength = 3;
        }
    }
}
