using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoCraft.Common
{
    static class ProfileManager
    {
        static List<Profile> profiles;

        internal static void AddProfile(int id, string name)
        {
            Profile tempProfile = new Profile(id, name);
            profiles.Add(tempProfile);
        }

        internal static void DeleteProfile(int id)
        {
            var index = profiles.FindIndex(c => c.ID == id);
            profiles.RemoveAt(index);
        }

        internal static void EditProfile(int id, string name)
        {
            profiles.FirstOrDefault(profile => profile.Name == name);
        }

        internal static Profile GetProfileById(int id)
        {
            return profiles.Find(c => c.ID == id);
        }

        internal static System.Collections.ObjectModel.ReadOnlyCollection<Profile> GetAllProfiles
        {
            get { return profiles.AsReadOnly(); }
        }

        static ProfileManager()
        {
            profiles = null;
        }
    }
}
