using System;
using System.Collections.Generic;
using System.Threading;

namespace SimpleReport.Template.Infrastructure.ParallelTask
{
    public class ThreadManager<T>
    {
        private Action<T> action { get; set; }

        private Semaphore semaphore { get; set; }

        public IList<Thread> listThread { get; set; }

        private void InternalProcessInParallel(int maxThread, IEnumerable<T> listOfObjects, Action<T> action, TimeSpan timeOut, bool waitAll)
        {
            semaphore = new Semaphore(maxThread, maxThread);
            this.action = action;
            listThread = new List<Thread>();

            foreach (var ObjectAction in listOfObjects)
            {
                var paramStart = new ParameterizedThreadStart(ActionParam);
                var thread = new Thread(paramStart);
                semaphore.WaitOne(timeOut);
                thread.Start(ObjectAction);
                listThread.Add(thread);
            }

            if (waitAll)
            {
                WaitAll(timeOut);
            }
        }

        private void WaitAll(TimeSpan timeOut)
        {
            foreach (var thread in listThread)
            {
                var dtStartValidation = DateTime.Now;

                while (thread.IsAlive)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));

                    if (DateTime.Now.Subtract(dtStartValidation) > timeOut)
                    {
                        throw new Exception("Time out execution thread");
                    }
                }
            }
        }

        private void ActionParam(object objectAction)
        {
            try
            {
                action((T)objectAction);
            }
            finally
            {
                semaphore.Release();
            }
        }

        public void ProcessInParallel(int maxThread, IEnumerable<T> listOfObjects, Action<T> action)
        {
            ProcessInParallel(maxThread, listOfObjects, action, TimeSpan.FromMinutes(10), true);
        }

        public void ProcessInParallel(int maxThread, IEnumerable<T> listOfObjects, Action<T> action, TimeSpan timeOut)
        {
            ProcessInParallel(maxThread, listOfObjects, action, timeOut, true);
        }

        public void ProcessInParallel(int maxThread, IEnumerable<T> listOfObjects, Action<T> action, TimeSpan timeOut, bool waitAll)
        {
            var threadManager = new ThreadManager<T>();
            threadManager.InternalProcessInParallel(maxThread, listOfObjects, action, timeOut, waitAll);
        }
    }
}
