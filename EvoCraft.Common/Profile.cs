namespace EvoCraft.Core
{
    public class Profile
    {
        protected int id;
        protected string name;

        public int ID
        {
            get { return id; }
            protected public set { id = value; }
        }

        public string Name
        {
            get { return name; }
            protected public set { name = value; }
        }

        protected public Profile()
        {
        }

        protected public Profile(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
