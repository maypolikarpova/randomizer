using System;
using System.Collections.Generic;
using System.Threading;

namespace Randomizer.Models
{
    [Serializable]
    public class Request
    {
        #region Fields
        private Guid _guid;
        private DateTime _date;
        private int _startNumber;
        private int _endNumber;
        private int _generatedAmount;
        private IList<int> _sequence;
        #endregion

        #region Properties
        public Guid Guid
        {
            get { return _guid; }
            private set { _guid = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public int StartNumber
        {
            get { return _startNumber; }
            set { _startNumber = value; }
        }
        public int EndNumber
        {
            get { return _endNumber; }
            set { _endNumber = value; }
        }
        public int GeneratedAmount
        {
            get { return _generatedAmount; }
            set { _generatedAmount = value; }
        }
        public IList<int> Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }

        #endregion

        #region Constructor
        public Request(int startNumber, int endNumber, User user) : this()
        {
            _guid = Guid.NewGuid();
            _date = DateTime.Now;
            _startNumber = startNumber;
            _endNumber = endNumber;
            _generatedAmount = _endNumber - _startNumber + 1;
            _sequence = GenerateSequence();
            user.Requests.Add(this);
        }

        private Request()
        {}
        #endregion

        public override string ToString()
        {
            return String.Format("Start Number: {0}, End Number: {1}, Request Date: {2}, Generated Amount: {3}", 
                StartNumber, EndNumber, Date, GeneratedAmount); 
        }

        private IList<int> GenerateSequence()
        {
            IList<int> sequence = new List<int>();
            IList<int> initial = new List<int>();
            Random random = new Random();

            new Thread(() =>
            {
               Thread.CurrentThread.IsBackground = true;

                for (int i = _startNumber; i < _endNumber; i++)
                {
                    initial.Add(i);
                }

                for (int i = _startNumber; i < _endNumber; i++)
                {
                    int position = random.Next(0, _generatedAmount - 1);
                    if (sequence.Contains(initial[position]))
                    { 
                        while (sequence.Contains(initial[position]))
                        {
                            position = random.Next(0, _generatedAmount - 1);
                        }
                    }
                    sequence.Add(initial[position]);
                }
            }).Start();
            return sequence;
        }

        public bool IsValidRequest()
        {
            return _startNumber >= 0
                && _endNumber > 0 
                &&_startNumber < _endNumber;
        }
    }   
}
