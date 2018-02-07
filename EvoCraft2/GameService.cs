using EvoCraft2.Common;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System;

namespace EvoCraft2.Core
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class GameService : GameDescription, IClientService, IAdminService
    {
        //public event EventHandler<List<Unit>> MapReceived;

        public GameService()
        {
            //Engine.OnUpdateFinished += GameService_MapReceived;
            Core.Engine.OnUpdateFinished += Engine_OnUpdateFinished;
        }

        private void Engine_OnUpdateFinished(object sender, List<Unit> map)
        {
            foreach (IServiceCallback callback in lofasz.Values)
            {
                callback.SendMap(map);
            }
        }

        //private void GameService_MapReceived(object sender, List<Unit> e)
        //{
        //    //foreach (IServiceCallback callback in lofasz.Values)
        //    //{
        //    //    callback.SendMap(e);
        //    //}
        //}

        internal GameDescription GameDescription {get; set;}

        Dictionary<string, IServiceCallback> lofasz = new Dictionary<string,IServiceCallback>();

        public string JoinGame(string person)
        {
            if (!lofasz.ContainsKey(person))
            {
                lofasz[person] = OperationContext.Current.GetCallbackChannel<IServiceCallback>();
            }
            //if (lofasz.ContainsKey(person) && lofasz[person].Count <= 1)
            //{
            //    lofasz[person].Add(OperationContext.Current.GetCallbackChannel<IServiceCallback>());
            //}

            if (lofasz.Count == 2)
            {
                foreach (IServiceCallback item in lofasz.Values)
                {
                    item.StartGame();
                }
            }

            return string.Format(CultureInfo.CurrentUICulture, "Szia {0}!", person);
        }

        public void SendMessage(string message)
        {
            foreach (IServiceCallback callback in lofasz.Values)
            {
                //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle,
                //new Action( () =>
                //{
                //foreach (var item in list)
                //{
                callback.Join(message);
                //}
                //}
                //));

            }
            //ServiceCallback asd = new ServiceCallback();
            //asd.Join();
            //Console.WriteLine(message);
            //return message;
        }

        public void SendMap(List<Unit> map)
        {
            foreach (IServiceCallback callback in lofasz.Values)
            {
                callback.SendMap(map);
            }
        }

        //public void SetGameDescription(GameDescription gameDesrciption)
        //{
        //    GameDescription = gameDesrciption;
        //}

        public GameDescription GetGameDescribtion()
        {
            return GameDescription;
        }

        public GameDescription GetServerDetails()
        {
            return GameDescription;
        }

        public void SetServerDetails(GameDescription gameDesc)
        {
            GameDescription = gameDesc;
        }

        public void AddCommand(Command command)
        {
            Core.Engine.AddCommand(command);
        }

        public void GetMap(List<Unit> map)
        {
            throw new NotImplementedException();
        }
    }
}
