namespace Sail.NET
{
    public abstract class SailApiHandler
    {
        public abstract string CreateRequest(string input, SailModel model, int tokens = 0, double temperature = 0, int count = 0);
        public abstract string CreateResponse(string context);
        public virtual void ClearHistory()
        {
            throw new NotImplementedException();
        }
    }
}
