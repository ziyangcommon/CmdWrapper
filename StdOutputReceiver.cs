namespace CmdWrapper
{
    public static class StdOutputReceiver
    {
        public delegate void StdOutputReceiverDelegate(Option option,string output);
        public static event StdOutputReceiverDelegate StdOutputReceived;

        public static void SendStdOutput(Option option,string output)
        {
            if (StdOutputReceived != null)
            {
                StdOutputReceived(option,output);
            }
        }
    }
}