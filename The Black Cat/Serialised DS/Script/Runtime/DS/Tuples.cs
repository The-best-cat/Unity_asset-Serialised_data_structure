namespace TheBlackCat.SerialisedDS
{
    public struct SerializedTuple
    {
        public static SerializedTuple<A, B> Of<A, B>(A item1, B item2)
        {
            return new SerializedTuple<A, B>(item1, item2);
        }

        public static SerializedTuple<A, B, C> Of<A, B, C>(A item1, B item2, C item3)
        {
            return new SerializedTuple<A, B, C>(item1, item2, item3);
        }

        public static SerializedTuple<A, B, C, D> Of<A, B, C, D>(A item1, B item2, C item3, D item4)
        {
            return new SerializedTuple<A, B, C, D>(item1, item2, item3, item4);
        }

        public static SerializedTuple<A, B, C, D, E> Of<A, B, C, D, E>(A item1, B item2, C item3, D item4, E item5)
        {
            return new SerializedTuple<A, B, C, D, E>(item1, item2, item3, item4, item5);
        }

        public static SerializedTuple<A, B, C, D, E, F> Of<A, B, C, D, E, F>(A item1, B item2, C item3, D item4, E item5, F item6)
        {
            return new SerializedTuple<A, B, C, D, E, F>(item1, item2, item3, item4, item5, item6);
        }

        public static SerializedTuple<A, B, C, D, E, F, G> Of<A, B, C, D, E, F, G>(A item1, B item2, C item3, D item4, E item5, F item6, G item7)
        {
            return new SerializedTuple<A, B, C, D, E, F, G>(item1, item2, item3, item4, item5, item6, item7);
        }

        public static SerializedTuple<A, B, C, D, E, F, G, H> Of<A, B, C, D, E, F, G, H>(A item1, B item2, C item3, D item4, E item5, F item6, G item7, H item8)
        {
            return new SerializedTuple<A, B, C, D, E, F, G, H>(item1, item2, item3, item4, item5, item6, item7, item8);
        }
    }

    [System.Serializable]
    public struct SerializedTuple<A, B>
    {
        public A Item1;
        public B Item2;

        internal SerializedTuple(A item1, B item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public static implicit operator (A, B)(SerializedTuple<A, B> tuple)
        {
            return (tuple.Item1, tuple.Item2);
        }

        public static implicit operator SerializedTuple<A, B>((A, B) tuple)
        {
            return new SerializedTuple<A, B>(tuple.Item1, tuple.Item2);
        }
    }

    [System.Serializable]
    public struct SerializedTuple<A, B, C>
    {
        public A Item1;
        public B Item2;
        public C Item3;

        internal SerializedTuple(A item1, B item2, C item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        public static implicit operator (A, B, C)(SerializedTuple<A, B, C> tuple)
        {
            return (tuple.Item1, tuple.Item2, tuple.Item3);
        }

        public static implicit operator SerializedTuple<A, B, C>((A, B, C) tuple)
        {
            return new SerializedTuple<A, B, C>(tuple.Item1, tuple.Item2, tuple.Item3);
        }
    }

    [System.Serializable]
    public struct SerializedTuple<A, B, C, D>
    {
        public A Item1;
        public B Item2;
        public C Item3;
        public D Item4;

        internal SerializedTuple(A item1, B item2, C item3, D item4)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
        }

        public static implicit operator (A, B, C, D)(SerializedTuple<A, B, C, D> tuple)
        {
            return (tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        }

        public static implicit operator SerializedTuple<A, B, C, D>((A, B, C, D) tuple)
        {
            return new SerializedTuple<A, B, C, D>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        }
    }

    [System.Serializable]
    public struct SerializedTuple<A, B, C, D, E>
    {
        public A Item1;
        public B Item2;
        public C Item3;
        public D Item4;
        public E Item5;

        internal SerializedTuple(A item1, B item2, C item3, D item4, E item5)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
        }

        public static implicit operator (A, B, C, D, E)(SerializedTuple<A, B, C, D, E> tuple)
        {
            return (tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);
        }

        public static implicit operator SerializedTuple<A, B, C, D, E>((A, B, C, D, E) tuple)
        {
            return new SerializedTuple<A, B, C, D, E>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);
        }
    }

    [System.Serializable]
    public struct SerializedTuple<A, B, C, D, E, F>
    {
        public A Item1;
        public B Item2;
        public C Item3;
        public D Item4;
        public E Item5;
        public F Item6;

        internal SerializedTuple(A item1, B item2, C item3, D item4, E item5, F item6)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
        }

        public static implicit operator (A, B, C, D, E, F)(SerializedTuple<A, B, C, D, E, F> tuple)
        {
            return (tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6);
        }

        public static implicit operator SerializedTuple<A, B, C, D, E, F>((A, B, C, D, E, F) tuple)
        {
            return new SerializedTuple<A, B, C, D, E, F>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6);
        }
    }

    [System.Serializable]
    public struct SerializedTuple<A, B, C, D, E, F, G>
    {
        public A Item1;
        public B Item2;
        public C Item3;
        public D Item4;
        public E Item5;
        public F Item6;
        public G Item7;

        internal SerializedTuple(A item1, B item2, C item3, D item4, E item5, F item6, G item7)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
        }

        public static implicit operator (A, B, C, D, E, F, G)(SerializedTuple<A, B, C, D, E, F, G> tuple)
        {
            return (tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7);
        }

        public static implicit operator SerializedTuple<A, B, C, D, E, F, G>((A, B, C, D, E, F, G) tuple)
        {
            return new SerializedTuple<A, B, C, D, E, F, G>(tuple.Item1, tuple.Item2, tuple.Item3,
                tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7);
        }
    }

    [System.Serializable]
    public struct SerializedTuple<A, B, C, D, E, F, G, H>
    {
        public A Item1;
        public B Item2;
        public C Item3;
        public D Item4;
        public E Item5;
        public F Item6;
        public G Item7;
        public H Item8;

        internal SerializedTuple(A item1, B item2, C item3, D item4, E item5, F item6, G item7, H item8)
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

        public static implicit operator (A, B, C, D, E, F, G, H)(SerializedTuple<A, B, C, D, E, F, G, H> tuple)
        {
            return (tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7, tuple.Item8);
        }

        public static implicit operator SerializedTuple<A, B, C, D, E, F, G, H>((A, B, C, D, E, F, G, H) tuple)
        {
            return new SerializedTuple<A, B, C, D, E, F, G, H>(tuple.Item1, tuple.Item2, tuple.Item3,
                tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7, tuple.Item8);
        }
    }
}
