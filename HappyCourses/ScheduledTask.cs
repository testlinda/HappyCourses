using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HappyCourses
{
    public class ScheduledTask
    {
        private OperationHandler _operation_delegate = null;
        private DateTime _starttime = DateTime.MinValue;

        private CancellationTokenSource _tokenSource;
        private bool isTaskContinued = true;

        public ScheduledTask(DateTime starttime, OperationHandler operation)
        {
            _starttime = starttime;
            _operation_delegate = operation;
            Initialization();
        }

        private void Initialization()
        {
            _tokenSource = new CancellationTokenSource();
        }

        public void ArrangeTask()
        {
            var dateNow = DateTime.Now;
            var date = _starttime;

            TimeSpan ts = date - dateNow;
            Task.Delay(ts, _tokenSource.Token).ContinueWith((isTaskContinued) => DoTask());
        }

        private void DoTask()
        {
            if (!isTaskContinued)
                return;

            if (_operation_delegate != null)
            {
                _operation_delegate();
            }
        }

        public void CancelTask()
        {
            isTaskContinued = false;
            _tokenSource.Cancel();
        }
    }
}
