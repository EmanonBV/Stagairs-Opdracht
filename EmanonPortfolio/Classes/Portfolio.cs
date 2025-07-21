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
                return Transactions.Sum(t => t.NumberShares);
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
            if (pNumberDecimals != null)
            {
                // We geven de pNumberDecimals als parameter niet mee in de Amount methode van Transaction,
                // om te voorkomen dat we bij elke interatie een Math.Round moeten uitvoeren.
                // In plaats daarvan ronden we de totale waarde 1 keer af.

                // Als we veel moeten afronden, kan het eindresultaat daardoor anders worden.
                return Math.Round(Transactions.Sum(t => t.Amount()), pNumberDecimals.Value);
            }
            return Transactions.Sum(t => t.Amount());
        }


        // ---------------------------------------------------------------------------------------------------
        // Methode: NumberOnDate
        // Doel   : Opleveren van het aantal aandelen in het portfolio tot en met de gegeven datum.
        // TODO   : Dit kenmerk kent een optionele parameter pShareTypes, indien deze parameter gevuld is
        //          dienen alleen transacties met een ShareType in de lijst meegenomen te worden in
        //          de berekening van het aantal aandelen.
        // ---------------------------------------------------------------------------------------------------
        public int NumberOnDate(DateTime pSelectionDate
                              , List<Constants.ShareType>? pShareTypes = null)
        {
            if (pShareTypes != null && pShareTypes.Count > 0)
            {
                return Transactions
                    .Where(t => t.TransactionDate <= pSelectionDate && pShareTypes.Contains(t.ShareType))
                    .Sum(t => t.NumberShares);
            }
            return Transactions
                    .Where(t => t.TransactionDate <= pSelectionDate)
                    .Sum(t => t.NumberShares);
        }


        // ---------------------------------------------------------------------------------------------------
        // Methode: ValueOnDate
        // Doel   : Opleveren van de waarde van de aandelen in het portfolio tot en met de gegeven datum.
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
            decimal valueOnDate = -1;

            valueOnDate = pShareTypes != null && pShareTypes.Count > 0 ?
                Transactions
                    .Where(t => t.TransactionDate <= pSelectionDate && pShareTypes.Contains(t.ShareType))
                    .Select(t => t.Amount())
                    .Sum() :
                Transactions
                    .Where(t => t.TransactionDate <= pSelectionDate)
                    .Select(t => t.Amount())
                    .Sum();

            return pNumberDecimals != null
                ? Math.Round(valueOnDate, pNumberDecimals.Value)
                : valueOnDate;
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
