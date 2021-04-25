using System;
using System.Collections.Generic;

namespace TestProj
{
    public class Trade
    {
        public decimal Volume;
        public float Qty;
        public Guid TradeGuid;
        public int TradeType;
        public DateTime TradeDate;
        public bool IsOtc;
        public Instrument instrument;
        public List<Counterparty> Sides;
        public Dictionary<int, Counterparty> SidesDict;
        public List<Dictionary<string, Counterparty>> Nightmare;

        public int AngryProp { get; set; }
    }

    public class Instrument
    {
        public decimal FaceValue;
        public string Symbol;
    }


    public class Counterparty
    {
        public string Name;
    }



}
