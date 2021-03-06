using BlubLib.Serialization;

namespace Netsphere.Network.Message.Auth
{
    [BlubContract]
    public class CAuthInEUReqMessage : IAuthMessage
    {
        [BlubMember(0)]
        public string Username { get; set; }

        [BlubMember(1)]
        public string Password { get; set; }

        [BlubMember(2)]
        public string Unk1 { get; set; }

        [BlubMember(3)]
        public string Unk2 { get; set; }

        [BlubMember(4)]
        public int Unk3 { get; set; }

        [BlubMember(5)]
        public int Unk4 { get; set; }

        [BlubMember(6)]
        public int Unk5 { get; set; }

        [BlubMember(7)]
        public string Unk6 { get; set; }
    }

    [BlubContract]
    public class CServerListReqMessage : IAuthMessage
    {
    }
}
