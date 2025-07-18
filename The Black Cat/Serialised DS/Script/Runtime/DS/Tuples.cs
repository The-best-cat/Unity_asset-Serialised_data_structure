namespace TheBlackCat.SerialisedDS
{
    public struct Pair
    {
        public static Pair<A, B> Of<A, B>(A item1, B item2)
        {
            return new Pair<A, B>(item1, item2);
        }
    }

    [System.Serializable]
    public struct Pair<A, B>
    {
        public A Item1;
        public B Item2;

        internal Pair(A item1, B item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public static implicit operator (A, B)(Pair<A, B> tuple)
        {
            return (tuple.Item1, tuple.Item2);
        }

        public static implicit operator Pair<A, B>((A, B) tuple)
        {
            return new Pair<A, B>(tuple.Item1, tuple.Item2);
        }
    }

    public struct Triplet
    {
        public static Triplet<A, B, C> Of<A, B, C>(A item1, B item2, C item3)
        {
            return new Triplet<A, B, C>(item1, item2, item3);
        }

    }

    [System.Serializable]
    public struct Triplet<A, B, C>
    {
        public A Item1;
        public B Item2;
        public C Item3;

        internal Triplet(A item1, B item2, C item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        public static implicit operator (A, B, C)(Triplet<A, B, C> tuple)
        {
            return (tuple.Item1, tuple.Item2, tuple.Item3);
        }

        public static implicit operator Triplet<A, B, C>((A, B, C) tuple)
        {
            return new Triplet<A, B, C>(tuple.Item1, tuple.Item2, tuple.Item3);
        }
    }

    public struct Quartet
    {
        public static Quartet<A, B, C, D> Of<A, B, C, D>(A item1, B item2, C item3, D item4)
        {
            return new Quartet<A, B, C, D>(item1, item2, item3, item4);
        }
    }

    [System.Serializable]
    public struct Quartet<A, B, C, D>
    {
        public A Item1;
        public B Item2;
        public C Item3;
        public D Item4;

        internal Quartet(A item1, B item2, C item3, D item4)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
        }

        public static implicit operator (A, B, C, D)(Quartet<A, B, C, D> tuple)
        {
            return (tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        }

        public static implicit operator Quartet<A, B, C, D>((A, B, C, D) tuple)
        {
            return new Quartet<A, B, C, D>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        }
    }

    public struct Quintet
    {
        public static Quintet<A, B, C, D, E> Of<A, B, C, D, E>(A item1, B item2, C item3, D item4, E item5)
        {
            return new Quintet<A, B, C, D, E>(item1, item2, item3, item4, item5);
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

        internal Quintet(A item1, B item2, C item3, D item4, E item5)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
        }

        public static implicit operator (A, B, C, D, E)(Quintet<A, B, C, D, E> tuple)
        {
            return (tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);
        }

        public static implicit operator Quintet<A, B, C, D, E>((A, B, C, D, E) tuple)
        {
            return new Quintet<A, B, C, D, E>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);
        }
    }

    public struct Sextet
    {
        public static Sextet<A, B, C, D, E, F> Of<A, B, C, D, E, F>(A item1, B item2, C item3, D item4, E item5, F item6)
        {
            return new Sextet<A, B, C, D, E, F>(item1, item2, item3, item4, item5, item6);
        }
    }

    [System.Serializable]
    public struct Sextet<A, B, C, D, E, F>
    {
        public A Item1;
        public B Item2;
        public C Item3;
        public D Item4;
        public E Item5;
        public F Item6;

        internal Sextet(A item1, B item2, C item3, D item4, E item5, F item6)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
        }

        public static implicit operator (A, B, C, D, E, F)(Sextet<A, B, C, D, E, F> tuple)
        {
            return (tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6);
        }

        public static implicit operator Sextet<A, B, C, D, E, F>((A, B, C, D, E, F) tuple)
        {
            return new Sextet<A, B, C, D, E, F>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6);
        }
    }

    public struct Septet
    {
        public static Septet<A, B, C, D, E, F, G> Of<A, B, C, D, E, F, G>(A item1, B item2, C item3, D item4, E item5, F item6, G item7)
        {
            return new Septet<A, B, C, D, E, F, G>(item1, item2, item3, item4, item5, item6, item7);
        }
    }

    [System.Serializable]
    public struct Septet<A, B, C, D, E, F, G>
    {
        public A Item1;
        public B Item2;
        public C Item3;
        public D Item4;
        public E Item5;
        public F Item6;
        public G Item7;

        internal Septet(A item1, B item2, C item3, D item4, E item5, F item6, G item7)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
        }

        public static implicit operator (A, B, C, D, E, F, G)(Septet<A, B, C, D, E, F, G> tuple)
        {
            return (tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7);
        }

        public static implicit operator Septet<A, B, C, D, E, F, G>((A, B, C, D, E, F, G) tuple)
        {
            return new Septet<A, B, C, D, E, F, G>(tuple.Item1, tuple.Item2, tuple.Item3,
                tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7);
        }
    }

    public struct Octet
    {
        public static Octet<A, B, C, D, E, F, G, H> Of<A, B, C, D, E, F, G, H>(A item1, B item2, C item3, D item4, E item5, F item6, G item7, H item8)
        {
            return new Octet<A, B, C, D, E, F, G, H>(item1, item2, item3, item4, item5, item6, item7, item8);
        }
    }

    [System.Serializable]
    public struct Octet<A, B, C, D, E, F, G, H>
    {
        public A Item1;
        public B Item2;
        public C Item3;
        public D Item4;
        public E Item5;
        public F Item6;
        public G Item7;
        public H Item8;

        internal Octet(A item1, B item2, C item3, D item4, E item5, F item6, G item7, H item8)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
            Item8 = item8;
        }

        public static implicit operator (A, B, C, D, E, F, G, H)(Octet<A, B, C, D, E, F, G, H> tuple)
        {
            return (tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7, tuple.Item8);
        }

        public static implicit operator Octet<A, B, C, D, E, F, G, H>((A, B, C, D, E, F, G, H) tuple)
        {
            return new Octet<A, B, C, D, E, F, G, H>(tuple.Item1, tuple.Item2, tuple.Item3,
                tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7, tuple.Item8);
        }
    }
}
