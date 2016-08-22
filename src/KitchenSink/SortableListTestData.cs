using Starcounter;

namespace KitchenSink
{
    static class SortableListTestData
    {
        public static int LastPosition = 0;
        private static Person LastPerson;
        public static bool Exists()
        {
            var exists = Db.SQL<Person>("SELECT i FROM Person i FETCH ?", 1).First;
            if (exists == null)
            {
                return false;
            }
            return true;
        }

        public static void DeleteAll()
        {
            Db.Transact(() =>
            {
                Db.SlowSQL("DELETE FROM Person");
            });
            LastPosition = 0;
            LastPerson = null;
        }
        public static void Create()
        {
                CreatePerson("Billy");
                CreatePerson("Bob");
                CreatePerson("John");
                CreatePerson("Jane");
                CreatePerson("Peter");
                CreatePerson("Paul");
                CreatePerson("Mary");
        }

        //Creates a person at the bottom of the list and disables the relevant buttons
        public static void CreatePerson(string name)
        {
            Db.Transact(() =>
            {
                LastPosition++;
                var p = new Person()
                {
                    Name = name,
                    Position = LastPosition,
                    BottomDisabled = true
                };
                if (LastPerson != null)
                {
                    LastPerson.BottomDisabled = false;
                }
                else
                {
                    p.TopDisabled = true;
                }
                LastPerson = p;
            });
        }
    }
}
