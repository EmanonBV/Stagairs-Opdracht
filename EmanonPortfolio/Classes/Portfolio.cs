//   ---------------------------------------------------------------------------------------------------------
//   Bestand       : Portfolio.cs
//   Doel          : Definiëren het porfolio object met de bijbehorende methodes en eigenschappen.
//   Copyright (c) : Emanon BV, 2025
//   Aanpassingen  :
//   2025-06-26/001: Nieuw. (Sofie)
//   ---------------------------------------------------------------------------------------------------------

namespace Emanon_Portfolio.Classes
{
    public class Portfolio
    {
        #region Objectkenmerken
        // ---------------------------------------------------------------------------------------------------
        // Kenmerk: Transactions
        // Doel   : De lijst met transacties binnen het portfolio.
        // ---------------------------------------------------------------------------------------------------
        public List<Transaction> Transactions { get; private set; }
        #endregion


        #region Afgeleide kenmerken
        // ---------------------------------------------------------------------------------------------------
        // Kenmerk: TotalNumber
        // Doel   : Opleveren van het totaal aantal aandelen in het portfolio op basis van de bovenstaande
        //          lijst met transacties, onafhankelijk van transactiedatum.
        // ---------------------------------------------------------------------------------------------------
        public int TotalNumber
        {
            get
            {
                #warning Implementeer dit kenmerk
                return 0;
            }
        }


        // ---------------------------------------------------------------------------------------------------
        // Methode: TotalValue
        // Doel   : Opleveren van de totale waarde van alle aandelen in het portfolio op basis van de
        //          bovenstaande lijst met transacties, onafhankelijk van transactiedatum.
        // TODO   : Dit kenmerk kent een optionele parameter pNumberDecimals die bepaalt op hoeveel decimalen
        //          de waarde wordt afgerond, indien null wordt de waarde niet afgerond.
        // ---------------------------------------------------------------------------------------------------
        public decimal TotalValue(int? pNumberDecimals = 2)
        {
            #warning Implementeer deze methode
            return 0m;
        }


        // ---------------------------------------------------------------------------------------------------
        // Methode: NumberOnDate
        // Doel   : Opleveren van het aantal aandelen in het portfolio op de gegeven datum.
        // TODO   : Dit kenmerk kent een optionele parameter pShareTypes, indien deze parameter gevuld is
        //          dienen alleen transacties met een ShareType in de lijst meegenomen te worden in
        //          de berekening van het aantal aandelen.
        // ---------------------------------------------------------------------------------------------------
        public int NumberOnDate(DateTime pSelectionDate
                              , List<Constants.ShareType>? pShareTypes = null)
        {
            #warning Implementeer deze methode
            return 0;
        }


        // ---------------------------------------------------------------------------------------------------
        // Methode: ValueOnDate
        // Doel   : Opleveren van de waarde van de aandelen in het portfolio op de gegeven datum.
        // TODO   : Dit kenmerk kent een optionele parameter pNumberDecimals die bepaalt op hoeveel decimalen
        //          de waarde wordt afgerond, indien null wordt de waarde niet afgerond.
        //          Ook kent het kenmerk een optionele parameter pShareTypes, indien deze parameter gevuld is
        //          dienen alleen transacties met een ShareType in de lijst meegenomen te worden
        //          in de berekening van de waarde van de aandelen.
        // ---------------------------------------------------------------------------------------------------
        public decimal ValueOnDate(DateTime pSelectionDate
                                 , int? pNumberDecimals = 2
                                 , List<Constants.ShareType>? pShareTypes = null)
        {
            #warning Implementeer deze methode
            return 0m;
        }
        #endregion


        #region Constructor
        // ---------------------------------------------------------------------------------------------------
        // Methode   : Portfolio
        // Doel      : Het initieren van een nieuw Portfolio object met de meegegeven transacties.
        // ---------------------------------------------------------------------------------------------------
        public Portfolio(List<Transaction> pTransactions)
        {
            Transactions = pTransactions;
        }
        #endregion
    }
}
