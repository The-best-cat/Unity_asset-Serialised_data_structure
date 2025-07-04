namespace TheBlackCat.SerialisedDS
{
    [System.Serializable]
    public struct SerializedTuple<A, B>
    {
        public A Item1;
        public B Item2;

        private SerializedTuple(A item1, B item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public static SerializedTuple<A, B> Of(A item1, B item2)
        {
            return new SerializedTuple<A, B>(item1, item2);
        }

        public void Deconstruct(out A item1, out B item2)
        {
            item1 = Item1;
            item2 = Item2;
        }
    }

    [System.Serializable]
    public struct SerializedTuple<A, B, C>
    {
        public A Item1;
        public B Item2;
        public C Item3;

        private SerializedTuple(A item1, B item2, C item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        public static SerializedTuple<A, B, C> Of(A item1, B item2, C item3)
        {
            return new SerializedTuple<A, B, C>(item1, item2, item3);
        }

        public void Deconstruct(out A item1, out B item2, out C item3)
        {
            item1 = Item1;
            item2 = Item2;
            item3 = Item3;
        }
    }

    [System.Serializable]
    public struct SerializedTuple<A, B, C, D>
    {
        public A Item1;
        public B Item2;
        public C Item3;
        public D Item4;

        private SerializedTuple(A item1, B item2, C item3, D item4)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
        }

        public static SerializedTuple<A, B, C, D> Of(A item1, B item2, C item3, D item4)
        {
            return new SerializedTuple<A, B, C, D>(item1, item2, item3, item4);
        }

        public void Deconstruct(out A item1, out B item2, out C item3, out D item4)
        {
            item1 = Item1;
            item2 = Item2;
            item3 = Item3;
            item4 = Item4;
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

        private SerializedTuple(A item1, B item2, C item3, D item4, E item5)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
        }

        public static SerializedTuple<A, B, C, D, E> Of(A item1, B item2, C item3, D item4, E item5)
        {
            return new SerializedTuple<A, B, C, D, E>(item1, item2, item3, item4, item5);
        }

        public void Deconstruct(out A item1, out B item2, out C item3, out D item4, out E item5)
        {
            item1 = Item1;
            item2 = Item2;
            item3 = Item3;
            item4 = Item4;
            item5 = Item5;
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

        private SerializedTuple(A item1, B item2, C item3, D item4, E item5, F item6)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
        }

        public static SerializedTuple<A, B, C, D, E, F> Of(A item1, B item2, C item3, D item4, E item5, F item6)
        {
            return new SerializedTuple<A, B, C, D, E, F>(item1, item2, item3, item4, item5, item6);
        }

        public void Deconstruct(out A item1, out B item2, out C item3, out D item4, out E item5, out F item6)
        {
            item1 = Item1;
            item2 = Item2;
            item3 = Item3;
            item4 = Item4;
            item5 = Item5;
            item6 = Item6;
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

        private SerializedTuple(A item1, B item2, C item3, D item4, E item5, F item6, G item7)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
        }

        public static SerializedTuple<A, B, C, D, E, F, G> Of(A item1, B item2, C item3, D item4, E item5, F item6, G item7)
        {
            return new SerializedTuple<A, B, C, D, E, F, G>(item1, item2, item3, item4, item5, item6, item7);
        }

        public void Deconstruct(out A item1, out B item2, out C item3, out D item4, out E item5, out F item6, out G item7)
        {
            item1 = Item1;
            item2 = Item2;
            item3 = Item3;
            item4 = Item4;
            item5 = Item5;
            item6 = Item6;
            item7 = Item7;
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

        private SerializedTuple(A item1, B item2, C item3, D item4, E item5, F item6, G item7, H item8)
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

        public static SerializedTuple<A, B, C, D, E, F, G, H> Of(A item1, B item2, C item3, D item4, E item5, F item6, G item7, H item8)
        {
            return new SerializedTuple<A, B, C, D, E, F, G, H>(item1, item2, item3, item4, item5, item6, item7, item8);
        }

        public void Deconstruct(out A item1, out B item2, out C item3, out D item4, out E item5, out F item6, out G item7, out H item8)
        {
            item1 = Item1;
            item2 = Item2;
            item3 = Item3;
            item4 = Item4;
            item5 = Item5;
            item6 = Item6;
            item7 = Item7;
            item8 = Item8;
        }
    }
}
