namespace ClockwiseMatrix
{
    public static class Constants
    {
        public const int TotalCountOfDirections = 8;
        private static int maxValueOfTheFillNumber;
        private static bool maxValueIsSet = false;

        public static int MaxValueOfTheFillNumber
        {
            get
            {
                return maxValueOfTheFillNumber;
            }
            set
            {
                if (!maxValueIsSet)
                {
                    maxValueOfTheFillNumber = value;
                    maxValueIsSet = true;
                }
            }
        }
    }
}
