using ChestCube;
using System;
using System.Collections.Generic;
using TestProj;

namespace ChestCubeApp
{
    class Program
    {
        static void Main(string[] args)
        {


            Trade t = new TestProj.Trade
            {
                IsOtc = true,
                Qty = 11.1f,
                TradeDate = DateTime.Now,
                TradeType = 1,
                TradeGuid = Guid.NewGuid(),
                Volume = 6666.6m,
                instrument = new TestProj.Instrument() { Symbol = "KRX", FaceValue = 1000 },
                SidesDict = new Dictionary<int, TestProj.Counterparty>()
                { { 1, new TestProj.Counterparty() { Name = "VTBC" } },
                    { 2, new TestProj.Counterparty() { Name = "SBERP" } } },

                Sides = new List<TestProj.Counterparty>()
                { new TestProj.Counterparty() { Name = "ABD" },
                new TestProj.Counterparty(){Name="CBOM" } },
                AngryProp= 42,
                 Nightmare = new List<Dictionary<string, TestProj.Counterparty>>()
                 {
                     new Dictionary<string, TestProj.Counterparty>(){
                         { "1", new TestProj.Counterparty(){ Name="GAZP" } },
                         { "2", new TestProj.Counterparty(){ Name="GAZR" } }}

                     ,
                     new Dictionary<string, TestProj.Counterparty>(){
                      { "3", new TestProj.Counterparty(){ Name="SGNX" } },
                         { "4", new TestProj.Counterparty(){ Name="SGNP" } }
                     }

                 }

            };

            Repo r = new ChestCube.Repo("Server=localhost;Database=DB;Trusted_Connection=True;");
           var id= r.StoreObject(t);

            var rez = r.GetObject<Trade>(id);

            //
            //
            //

            Console.WriteLine("Hello World!");
        }
    }
}
