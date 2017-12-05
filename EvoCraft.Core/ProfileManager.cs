using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoCraft.Core
{
    static class ProfileManager
    {
        static List<Profile> profiles;

        public static void AddProfile(int id, string name)
        {
            Profile tempProfile = new Profile(id, name);
            profiles.Add(tempProfile);
        }

        public static void DeleteProfile(int id)
        {
            var index = profiles.FindIndex(c => c.ID == id);
            profiles.RemoveAt(index);
        }

        public static void EditProfile(int id, string name)
        {
            profiles.FirstOrDefault(profile => profile.Name == name);
        }

        public static Profile GetProfileById(int id)
        {
            return profiles.Find(c => c.ID == id);
        }

        public static System.Collections.ObjectModel.ReadOnlyCollection<Profile> GetAllProfiles
        {
            get { return profiles.AsReadOnly(); }
        }

        static ProfileManager()
        {
            profiles = null;
        }
    }
}
