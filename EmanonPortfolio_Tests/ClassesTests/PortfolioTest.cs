//   ---------------------------------------------------------------------------------------------------------
//   Bestand       : PortfolioTest.cs
//   Doel          : Testen van de Portfolio klasse en de bijbehorende methodes en eigenschappen.
//   Copyright (c) : Emanon BV, 2025
//   Aanpassingen  :
//   2025-06-26/001: Nieuw. (Sofie)
//   ---------------------------------------------------------------------------------------------------------

using FluentAssertions;
using Emanon_Portfolio;
using Emanon_Portfolio.Classes;

namespace Emanon_Portfolio_Tests.ClassesTests
{
    public class PortfolioTest
    {
        #region Testgegevens
        // ---------------------------------------------------------------------------------------------------
        // Kenmerk: TestTransactionData
        // Doel   : Gegevens die gebruikt worden voor het opbouwen van transacties voor de tests.
        // ---------------------------------------------------------------------------------------------------
        private readonly struct TestTransactionData(int NumberShares
                                                  , decimal SharesRate
                                                  , DateTime TransactionDate
                                                  , Constants.ShareType ShareType)
        {
            public int NumberShares { get; } = NumberShares;
            public decimal SharesRate { get; } = SharesRate;
            public DateTime TransactionDate { get; } = TransactionDate;
            public Constants.ShareType ShareType { get; } = ShareType;
        }
        readonly List<TestTransactionData> _testTransactionData;


        // ---------------------------------------------------------------------------------------------------
        // Methode: PortfolioTest
        // Doel   : Intialiseert de transacties die gebruikt worden in de tests.
        // ---------------------------------------------------------------------------------------------------
        public PortfolioTest()
        {
             _testTransactionData = new() {
                 new(NumberShares: 10
                   , SharesRate: 150.50m
                   , TransactionDate: new DateTime(2023, 10, 1)
                   , ShareType: Constants.ShareType.AAPL)
               , new(NumberShares: 37
                   , SharesRate: 212.65m
                   , TransactionDate: new DateTime(2023, 10, 16)
                   , ShareType: Constants.ShareType.AMZN)
               , new(NumberShares: -5
                   , SharesRate: 155.00m
                   , TransactionDate: new DateTime(2023, 11, 2)
                   , ShareType: Constants.ShareType.AAPL)
               , new(NumberShares: 8
                   , SharesRate: 720.18m
                   , TransactionDate: new DateTime(2023, 11, 10)
                   , ShareType: Constants.ShareType.TSLA)
               , new(NumberShares: 12
                   , SharesRate: 330.00m
                   , TransactionDate: new DateTime(2023, 12, 5)
                   , ShareType: Constants.ShareType.MSFT)
               , new(NumberShares: -3
                   , SharesRate: 215.00m
                   , TransactionDate: new DateTime(2023, 12, 15)
                   , ShareType: Constants.ShareType.AMZN)
               , new(NumberShares: 4
                   , SharesRate: 410.29m
                   , TransactionDate: new DateTime(2024, 1, 3)
                   , ShareType: Constants.ShareType.NFLX)
               , new(NumberShares: 2
                   , SharesRate: 152.00m
                   , TransactionDate: new DateTime(2024, 1, 10)
                   , ShareType: Constants.ShareType.AAPL)
               , new(NumberShares: -12
                   , SharesRate: 282.27m
                   , TransactionDate: new DateTime(2024, 1, 18)
                   , ShareType: Constants.ShareType.MSFT)
             };
        }


        // ---------------------------------------------------------------------------------------------------
        // Methode: GetShareTypeTestData
        // Doel   : Levert testdata voor de (Total)Number(OnDate) en (Total)Value(OnDate) methodes van de
        //          portfolio klasse.
        // ---------------------------------------------------------------------------------------------------
        public static IEnumerable<object[]> GetShareTypeTestData()
        {
            yield return new object[] { null!, DateTime.MaxValue };
            yield return new object[] { null!, new DateTime(2023, 12, 16) };

            yield return new object[] { new Constants.ShareType[] {
                                            Constants.ShareType.AAPL
                                          , Constants.ShareType.AMZN}
                                      , DateTime.MaxValue};
            yield return new object[] { new Constants.ShareType[] {
                                            Constants.ShareType.AAPL
                                          , Constants.ShareType.AMZN }
                                      , new DateTime(2024, 1, 1)};

            yield return new object[] { new Constants.ShareType[] { Constants.ShareType.NFLX }
                                      , new DateTime(2023, 1, 1) };
            yield return new object[] { new Constants.ShareType[] { Constants.ShareType.NFLX }
                                      , new DateTime(2024, 10, 17) };
        }
        #endregion


        #region Tests
        // ---------------------------------------------------------------------------------------------------
        // Methode: Portfolio_Constructor_ShouldInitializeTransactionsList
        // Doel   : Testen of de properties van het portfolio object correct worden gevuld in de constructor.
        // ---------------------------------------------------------------------------------------------------
        [Fact]
        public void Portfolio_Constructor_ShouldInitializeTransactionsList()
        {
            // Arrange
            var tmpTestTransactions = _testTransactionData
               .Select(t => new Transaction(pNumberShares: t.NumberShares
                                          , pSharesRate: t.SharesRate
                                          , pTransactionDate: t.TransactionDate
                                          , pShareType: t.ShareType))
               .ToList();
            // Act
            Portfolio portfolio = new Portfolio(pTransactions: tmpTestTransactions);

            // Assert
            portfolio.Transactions.Should().BeEquivalentTo(tmpTestTransactions);
        }


        // ---------------------------------------------------------------------------------------------------
        // Methode: Portfolio_NumberOnDate_IsCorrect
        // Doel   : Testen of het aantal aandelen binnen de gegeven type stukken op de gegeven datum
        //          goed worden berekend.
        // ---------------------------------------------------------------------------------------------------
        [Theory]
        [MemberData(nameof(GetShareTypeTestData))]
        public void Portfolio_NumberOnDate_IsCorrect(Constants.ShareType[]? pShareTypes
                                                   , DateTime pSelectionDate)
        {
            // Arrange
            var tmpTestTransactions = _testTransactionData
               .Select(t => new Transaction(pNumberShares: t.NumberShares
                                          , pSharesRate: t.SharesRate
                                          , pTransactionDate: t.TransactionDate
                                          , pShareType: t.ShareType))
               .ToList();
            var tmpShareTypes = pShareTypes?.ToList();
            var tmpPortfolio = new Portfolio(pTransactions: tmpTestTransactions);

            // Act
            var tmpExpectedNumber = _testTransactionData.Where(t => (pShareTypes == null
                                                                  || pShareTypes.Contains(t.ShareType))
                                                                 && t.TransactionDate <= pSelectionDate)
                                   .Sum(t => t.NumberShares);
            var tmpNumber = tmpPortfolio.NumberOnDate(pSelectionDate: pSelectionDate
                                                    , pShareTypes: tmpShareTypes);

            // Assert
            tmpNumber.Should().Be(tmpExpectedNumber);
        }


        // ---------------------------------------------------------------------------------------------------
        // Methode: Portfolio_TotalNumber_IsCorrect
        // Doel   : Testen of het totaal aantal aandelen in het portfolio correct wordt berekend.
        // ---------------------------------------------------------------------------------------------------
        [Fact]
        public void Portfolio_TotalNumber_IsCorrect()
        {
            // Arrange
            var tmpTestTransactions = _testTransactionData
               .Select(t => new Transaction(pNumberShares: t.NumberShares
                                          , pSharesRate: t.SharesRate
                                          , pTransactionDate: t.TransactionDate
                                          , pShareType: t.ShareType))
               .ToList();
            var tmpPortfolio = new Portfolio(pTransactions: tmpTestTransactions);

            // Act
            var tmpExpectedNumber = _testTransactionData
                                   .Sum(t => t.NumberShares);
            var tmpNumber = tmpPortfolio.TotalNumber;

            // Assert
            tmpNumber.Should().Be(tmpExpectedNumber);
        }


        // ---------------------------------------------------------------------------------------------------
        // Methode: Portfolio_AmountOnDate_IsCorrect
        // Doel   : Testen of de waarde van de aandelen binnen de gegeven type stukken op de gegeven datum
        //          goed worden berekend.
        // ---------------------------------------------------------------------------------------------------
        [Theory]
        [MemberData(nameof(GetShareTypeTestData))]
        public void Portfolio_AmountOnDate_IsCorrect(Constants.ShareType[]? pShareTypes
                                                   , DateTime pSelectionDate)
        {
            // Arrange
            var tmpTestTransactions = _testTransactionData
               .Select(t => new Transaction(pNumberShares: t.NumberShares
                                          , pSharesRate: t.SharesRate
                                          , pTransactionDate: t.TransactionDate
                                          , pShareType: t.ShareType))
               .ToList();
            var tmpShareTypes = pShareTypes?.ToList();
            var tmpPortfolio = new Portfolio(pTransactions: tmpTestTransactions);
            var tmpNumberDecimals = new Random().Next(1, 6);

            // Act
            var tmpExpectedValue = _testTransactionData.Where(t => (pShareTypes == null
                                                                 || pShareTypes.Contains(t.ShareType))
                                                                && t.TransactionDate <= pSelectionDate)
                                  .Sum(t => t.NumberShares * t.SharesRate);
            var tmpExpectedValueRounded = Math.Round(d: tmpExpectedValue, decimals: tmpNumberDecimals);
            var tmpValue = tmpPortfolio.ValueOnDate(pSelectionDate: pSelectionDate
                                                  , pShareTypes: tmpShareTypes
                                                  , pNumberDecimals: null);
            var tmpValueRounded = tmpPortfolio.ValueOnDate(pSelectionDate: pSelectionDate
                                                         , pShareTypes: tmpShareTypes
                                                         , pNumberDecimals: tmpNumberDecimals);

            // Assert
            tmpValue.Should().Be(tmpExpectedValue);
            tmpValueRounded.Should().Be(tmpExpectedValueRounded);
        }


        // ---------------------------------------------------------------------------------------------------
        // Methode: Portfolio_TotalValue_IsCorrect
        // Doel   : Testen of de totale waarde van aandelen in het portfolio correct wordt berekend.
        // ---------------------------------------------------------------------------------------------------
        [Fact]
        public void Portfolio_TotalValue_IsCorrect()
        {
            // Arrange
            var tmpTestTransactions = _testTransactionData
               .Select(t => new Transaction(pNumberShares: t.NumberShares
                                          , pSharesRate: t.SharesRate
                                          , pTransactionDate: t.TransactionDate
                                          , pShareType: t.ShareType))
               .ToList();
            var tmpPortfolio = new Portfolio(pTransactions: tmpTestTransactions);
            var tmpNumberDecimals = new Random().Next(1, 6);

            // Act
            var tmpExpectedValue = _testTransactionData
                                  .Sum(t => t.NumberShares * t.SharesRate);
            var tmpExpectedValueRounded = Math.Round(d: tmpExpectedValue, decimals: tmpNumberDecimals);
            var tmpValue = tmpPortfolio.TotalValue(pNumberDecimals: null);
            var tmpValueRounded = tmpPortfolio.TotalValue(pNumberDecimals: tmpNumberDecimals);

            // Assert
            tmpValue.Should().Be(tmpExpectedValue);
            tmpValueRounded.Should().Be(tmpExpectedValueRounded);
        }
        #endregion
    }
}