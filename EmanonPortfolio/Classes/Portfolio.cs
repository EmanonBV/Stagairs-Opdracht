//   ---------------------------------------------------------------------------------------------------------
//   Bestand       : Portfolio.cs
//   Doel          : Definiëren het porfolio object met de bijbehorende methodes en eigenschappen.
//   Copyright (c) : Emanon BV, 2025
//   Aanpassingen  :
//   2025-06-26/001: Nieuw. (Sofie)
//   ---------------------------------------------------------------------------------------------------------

using System.Transactions;

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
                
                int totalNumber = 0;
                
                foreach (Transaction transaction in Transactions)
                {
                    totalNumber += transaction.NumberShares;
                }
                return totalNumber;
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
            decimal totalValue = 0;

            foreach (Transaction transaction in Transactions)
            {
                totalValue += transaction.Amount(pNumberDecimals);
            }

            return totalValue;
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
            int totalNumber = 0;

            foreach (Transaction transaction in Transactions)
            {
                if (transaction.TransactionDate.CompareTo(pSelectionDate) <= 0)
                {
                    if (pShareTypes == null)
                    {
                        totalNumber += transaction.NumberShares;
                        continue;
                    }
                    else if (pShareTypes.Contains(transaction.ShareType))
                    {
                        totalNumber += transaction.NumberShares;
                    }
                }
            }

            return totalNumber;
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
            decimal totalValueOnDate = 0;

            foreach (Transaction transaction in Transactions)
            {
                if (transaction.TransactionDate.CompareTo(pSelectionDate) <= 0)
                {
                    if (pShareTypes == null)
                    {
                        totalValueOnDate += transaction.Amount(pNumberDecimals);
                        continue;
                    }
                    else if (pShareTypes.Contains(transaction.ShareType))
                    {
                        totalValueOnDate += transaction.Amount(pNumberDecimals);
                    }
                }
            }

            return totalValueOnDate;
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
