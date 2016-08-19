using Starcounter;

namespace KitchenSink
{
    static class SortableListTestData
    {
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
            Db.SlowSQL("DELETE FROM Person");
        }
        public static void Create()
        {
            Db.Transact(() =>
            {
                var John = new Person()
                {
                    Name = "John",
                    Position = 1
                };

                var Jane = new Person()
                {
                    Name = "Jane",
                    Position = 2
                };

                var Billy = new Person()
                {
                    Name = "Billy",
                    Position = 3
                };

                var Bob = new Person()
                {
                    Name = "Bob",
                    Position = 4
                };
            });
            }
    }
}
