namespace TheBlackCat.SerialisedDS
{
    [System.Serializable]
    public struct Pair<A, B>
    {
        public A Item1;
        public B Item2;

        private Pair(A item1, B item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public static Pair<A, B> Of(A item1, B item2)
        {
            return new Pair<A, B>(item1, item2);
        }
    }

    [System.Serializable]
    public struct Triplet<A, B, C>
    {
        public A Item1;
        public B Item2;
        public C Item3;

        private Triplet(A item1, B item2, C item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        public static Triplet<A, B, C> Of(A item1, B item2, C item3)
        {
            return new Triplet<A, B, C>(item1, item2, item3);
        }
    }

    [System.Serializable]
    public struct Quartet<A, B, C, D>
    {
        public A Item1;
        public B Item2;
        public C Item3;
        public D Item4;

        private Quartet(A item1, B item2, C item3, D item4)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
        }

        public static Quartet<A, B, C, D> Of(A item1, B item2, C item3, D item4)
        {
            return new Quartet<A, B, C, D>(item1, item2, item3, item4);
        }
    }

    [System.Serializable]
    public struct Quintet<A, B, C, D, E>
    {
        public A Item1;
        public B Item2;
        public C Item3;
        public D Item4;
        public E Item5;

        private Quintet(A item1, B item2, C item3, D item4, E item5)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
        }

        public static Quintet<A, B, C, D, E> Of(A item1, B item2, C item3, D item4, E item5)
        {
            return new Quintet<A, B, C, D, E>(item1, item2, item3, item4, item5);
        }
    }
}
