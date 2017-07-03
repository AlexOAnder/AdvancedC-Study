namespace FibbonaciClass
{
    public static class FibbonaciNumbers
    {
        public static ICacher Cacher { private get; set; }


        public static int Generate(int quantity)
        {
            if (Cacher == null)
            {
                Cacher = new MemmoryCache();
            }

            if (quantity <= 0)
            {
                return 0;
            }

            return quantity <= 2 ? 1 : Cacher.GetValue(quantity - 2) + Cacher.GetValue(quantity - 1);
        }
    }
    
}