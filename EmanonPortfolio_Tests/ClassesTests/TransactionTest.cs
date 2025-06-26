//   ---------------------------------------------------------------------------------------------------------
//   Bestand       : TransactionTest.cs
//   Doel          : Testen van de transactie klasse en de bijbehorende methodes en eigenschappen.
//   Copyright (c) : Emanon BV, 2025
//   Aanpassingen  :
//   2025-06-26/001: Nieuw. (Sofie)
//   ---------------------------------------------------------------------------------------------------------

using FluentAssertions;
using Emanon_Portfolio;
using Emanon_Portfolio.Classes;

namespace Emanon_Portfolio_Tests.ClassesTests
{
    public class TransactionTest
    {
        #region Tests
        // ---------------------------------------------------------------------------------------------------
        // Methode: Transaction_Constructor_ShouldInitializeProperties
        // Doel   : Testen of de properties van het transactie object correct worden gevuld in de constructor.
        // ---------------------------------------------------------------------------------------------------
        [Fact]
        public void Transaction_Constructor_ShouldInitializeProperties()
        {
            // Arrange
            int numberShares = 10;
            decimal sharesRate = 150.50m;
            DateTime transactionDate = new DateTime(2023, 10, 1);
            Constants.ShareType shareType = Constants.ShareType.AAPL;

            // Act
            Transaction transaction = new Transaction(pNumberShares: numberShares
                                                    , pSharesRate: sharesRate
                                                    , pTransactionDate: transactionDate
                                                    , pShareType: shareType);

            // Assert
            transaction.NumberShares.Should().Be(numberShares);
            transaction.SharesRate.Should().Be(sharesRate);
            transaction.TransactionDate.Should().Be(transactionDate);
            transaction.ShareType.Should().Be(shareType);
        }


        // ---------------------------------------------------------------------------------------------------
        // Methode: Transaction_Amount_isCorrect
        // Doel   : Testen of de berekening van het transactiebedrag correct is.
        // ---------------------------------------------------------------------------------------------------
        [Theory]
        [InlineData(10, 15)]
        [InlineData(15, 201.56)]
        [InlineData(25, 12.623156)]
        public void Transaction_Amount_isCorrect(int pNumberShares
                                               , decimal pSharesRate)
        {
            // Arrange
            Transaction transaction = new Transaction(pNumberShares: pNumberShares
                                                    , pSharesRate: pSharesRate
                                                    , pTransactionDate: DateTime.Now
                                                    , pShareType: Constants.ShareType.AAPL);
            int? tmpNumberDecimals = new Random().Next(0, 6);
            if (tmpNumberDecimals == 0) tmpNumberDecimals = null;

            // Act
            var tmpExpectedAmount = tmpNumberDecimals.HasValue
                                  ? Math.Round(d: pNumberShares * pSharesRate
                                             , decimals: tmpNumberDecimals.Value)
                                  : pNumberShares * pSharesRate;
            decimal amount = transaction.Amount(pNumberDecimals: tmpNumberDecimals);

            // Assert
            amount.Should().Be(tmpExpectedAmount);
        }
        #endregion
    }
}
