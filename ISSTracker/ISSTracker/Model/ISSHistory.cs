using System.Collections.Generic;

namespace ISSTracker.Model
{
    public class ISSHistory
    {
        List<ISSPosition> history;

        public ISSHistory()
        {
            history = new List<ISSPosition>();
        }

        public bool IsMoreThanOneRecord()
        {
            return history.Count > 1;
        }

        public int Count()
        {
            return history.Count;
        }

        public ISSPosition this[int index]
        {
            get
            {
                return history[index];
            }

        }

        public void AddRecord (ISSPosition position)
        {
            if(history.Count > 500)
            {
                history.RemoveAt(0);
            }
            history.Add(position);
        }

        public void RemoveRecordAt(int index)
        {
            history.RemoveAt(index);
        }
    }
}
