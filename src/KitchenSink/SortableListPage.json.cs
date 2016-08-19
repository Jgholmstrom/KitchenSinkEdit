using Starcounter;

namespace KitchenSink
{
    [Database]
    public class Person
    {
        public string Name;
        public int Position;
    }

    partial class SortableListPage : Page
    {
        protected override void OnData()
        {
            base.OnData();
            
            if (SortableListTestData.Exists())
            {
                Db.Transact(() =>
                {
                    SortableListTestData.DeleteAll();
                });
            }
            SortableListTestData.Create();
            People = Db.SQL("SELECT p FROM Person p ORDER BY p.Position");
        }
        public void RefreshData()
        {
            People = Db.SQL("SELECT p FROM Person p ORDER BY p.Position");
        }
    }

    [SortableListPage_json.People]
    partial class SortableListPagePeopleElement : Json, IBound<Person>
    {
        void Handle(Input.Up action)
        {
            Person PersonAbove = Db.SQL<Person>("SELECT p FROM Person p WHERE p.Position = ?", Data.Position - 1).First;
            if (PersonAbove != null)
            {
                Db.Transact(() =>
                {
                    Data.Position--;
                    PersonAbove.Position++;
                });
            }
            SortableListPage sortableListPage = (SortableListPage)Parent.Parent;
            sortableListPage.RefreshData();
            
        }

        void Handle(Input.Down action)
        {
            Person PersonBelow = Db.SQL<Person>("SELECT p FROM Person p WHERE p.Position = ?", Data.Position + 1).First;
            if (PersonBelow != null)
            {
                Db.Transact(() =>
                {
                    Data.Position++;
                    PersonBelow.Position--;
                });
            }
            SortableListPage sortableListPage = (SortableListPage)Parent.Parent;
            sortableListPage.RefreshData();
        }
    }
}
