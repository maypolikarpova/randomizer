using System;
using System.Collections.Generic;

namespace Randomizer.Models
{
    public class Request
    {
        #region Fields
        private Guid _guid;
        private DateTime _date;
        private int _startNumber;
        private int _endNumber;
        private int _generatedAmount;
        private ISet<int> _sequence;
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
        public ISet<int> Sequence
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
        {
        }
        #endregion
        public override string ToString()
        {
            return String.Format("Start Number: {0}, End Number: {1}, Request Date: {2}, Generated Amount: {3}", 
                StartNumber, EndNumber, Date, GeneratedAmount); 
        }

        private ISet<int> GenerateSequence()
        {
           ISet<int> sequence = new HashSet<int>();
           while(sequence.Count < _generatedAmount)
            {
                Random random = new Random();
                int generated = random.Next(_startNumber, _endNumber + 1);
                if (!sequence.Contains(generated))
                {
                    sequence.Add(generated);
                }
            }
            return sequence;
        }

        public bool IsValidRequest()
        {
            return _startNumber < _endNumber 
                && _startNumber >= 0
                && _endNumber > 0;
        }
    }   
}
