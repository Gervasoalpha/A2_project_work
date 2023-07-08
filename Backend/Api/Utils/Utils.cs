namespace Utils
{
    public static class Utils
    {
        public static int FromBoolToBit(bool expression)
        {
            if (expression)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}