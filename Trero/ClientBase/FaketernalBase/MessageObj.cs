namespace Trero.ClientBase.FaketernalBase
{
    internal class MessageObj
    {
        public ulong addr;

        public MessageObj(ulong addr)
        {
            this.addr = addr;
        }

        public string message // message offset is 0x0
        {
            get => MCM.readString(MCM.readInt64(addr + 0), 256);
            set => MCM.writeString(MCM.readInt64(addr + 0), value);
        }
    }
}