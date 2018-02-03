namespace EvoCraft.Core
{
    public class Profile
    {
        public int id;
        public string name;

        public int ID
        {
            get { return id; }
             set { id = value; }
        }

        public string Name
        {
            get { return name; }
             set { name = value; }
        }

         public Profile()
        {
        }

         public Profile(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
