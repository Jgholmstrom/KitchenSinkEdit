using Starcounter;

namespace KitchenSink
{
    [Database]
    public class Person
    {
        public string Name;
        public int Position;
        public bool TopDisabled;
        public bool BottomDisabled;
    }

    partial class SortableListPage : Page
    {
        protected override void OnData()
        {
            base.OnData();

            if (SortableListTestData.Exists())
            {
                SortableListTestData.DeleteAll();
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
                CheckTopAndBottom(Data);
                CheckTopAndBottom(PersonAbove);
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
                CheckTopAndBottom(Data);
                CheckTopAndBottom(PersonBelow);
            }
            SortableListPage sortableListPage = (SortableListPage)Parent.Parent;
            sortableListPage.RefreshData();
        }

        void CheckTopAndBottom (Person p)
        {
            Db.Transact(() =>
            {
                p.TopDisabled = (p.Position == 1) ? true : false;
                p.BottomDisabled = (p.Position == SortableListTestData.LastPosition) ? true : false;
            });
        }
    }
}
