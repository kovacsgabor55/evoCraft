using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            ////string teamProjectCollectionUrl = "https://tfs.codeplex.com:443/tfs/TFS41";

            ////TfsTeamProjectCollection teamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(teamProjectCollectionUrl));
            ////VersionControlServer versionControlServer = teamProjectCollection.GetService<VersionControlServer>();

            //string teamProjectCollectionUrl = "https://tfs.codeplex.com:443/tfs/TFS41";
            //string filePath = @"d:\VisualStudio\EVOCraft.MenuGridFinal\MenuGridFinal\TutorialsPage.xaml.cs";

            //// Get the version control server
            //TfsTeamProjectCollection teamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(teamProjectCollectionUrl));
            //VersionControlServer versionControlServer = teamProjectCollection.GetService<VersionControlServer>();

            //// Get the latest Item for filePath
            //Item item = versionControlServer.GetItem(filePath, VersionSpec.Latest);

            //// Download and display content to console
            //string fileString = string.Empty;

            //using (Stream stream = item.DownloadFile())
            //{
            //    using (MemoryStream memoryStream = new MemoryStream())
            //    {
            //        stream.CopyTo(memoryStream);

            //        // Use StreamReader to read MemoryStream created from byte array
            //        using (StreamReader streamReader = new StreamReader(new MemoryStream(memoryStream.ToArray())))
            //        {
            //            fileString = streamReader.ReadToEnd();
            //        }
            //    }
            //}

            //Console.WriteLine(fileString);
            //Console.ReadLine();

            TeamFoundationServer tfs = TeamFoundationServerFactory.GetServer("https://tfs.codeplex.com:443/tfs/TFS41");
            VersionControlServer vcs = (VersionControlServer)tfs.GetService(typeof(VersionControlServer));

            string machineName = Environment.MachineName;
            string currentUserName = Environment.UserName;

            PendingSet[] sets = vcs.QueryPendingSets(new string[] { @"d:\VisualStudio\Campus\ConsoleApplication1" }, RecursionType.Full, null, null);
            //Workspace myWorkspace = vcs.GetWorkspace(machineName, currentUserName);
            Workspace myWorkspace = vcs.GetWorkspace(machineName, @"snd\sititomi_cp");
            PendingChange[] changes = myWorkspace.GetPendingChanges(@"d:\VisualStudio\Campus", RecursionType.Full, false);

            foreach (var item in changes)
            {
                Console.WriteLine("{0}: {1}", item.ChangeType, item.ServerItem);
            }

            Console.WriteLine("{0} Pending sets", sets.Length);
            for (int i = 0; i < sets.Length; ++i)
            {
                Console.WriteLine("Workspace {0};{1} on {2} has {3} pending changes", sets[i].Name, sets[i].OwnerName, sets[i].Computer, sets[i].PendingChanges.Length);
                foreach(PendingChange pc in sets[i].PendingChanges)
                {
                    Console.WriteLine("{0,-20} {1,-10} {2,-16} {3}", pc.FileName, pc.ChangeTypeName, pc.CreationDate, pc.LocalItem);
                }
            }
            Console.ReadKey();
        }

        //public static PendingChange[] GetPendingChangesInTheWorkspace(string workspaceName, string userName, string compName)
        //{
        //    var tfs = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(ConfigurationManager.AppSettings["TfsUri"]));
        //    var service = tfs.GetService<VersionControlServer>();

        //    Workspace workspace = service.QueryWorkspaces(string.IsNullOrEmpty(workspaceName) ? null : workspaceName,
        //                                                    string.IsNullOrEmpty(userName) ? null : userName,
        //                                                    string.IsNullOrEmpty(compName) ? null : compName).First();

        //    var pendingchanges = workspace.GetPendingChanges();

        //    return pendingchanges;
        //}
    }
}
