﻿namespace utils
{
    // type référence lorsqu'on peut pas utiliser les références.
    public class Ref<T>
    {
        public T Value { get; set; }
        public Ref(T reference)
        {
            this.Value = reference;
        }

        public Ref(ref T reference)
        {
            this.Value = reference;
        }
    }
}

