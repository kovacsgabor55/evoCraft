namespace EvoCraft.Core
{
    public class Profile
    {
        protected int id;
        protected string name;

        public int ID
        {
            get { return id; }
            protected internal set { id = value; }
        }

        public string Name
        {
            get { return name; }
            protected internal set { name = value; }
        }

        protected internal Profile()
        {
        }

        protected internal Profile(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
