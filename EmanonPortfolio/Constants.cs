//   ---------------------------------------------------------------------------------------------------------
//   Bestand       : Constants.cs
//   Doel          : Definiëren van programma breede constanten.
//   Copyright (c) : Emanon BV, 2025
//   Aanpassingen  :
//   2025-06-26/001: Nieuw. (Sofie)
//   ---------------------------------------------------------------------------------------------------------

namespace Emanon_Portfolio
{
    public static class Constants
    {
        // ---------------------------------------------------------------------------------------------------
        // Constante: ShareType
        // Doel     : De constante ShareType definieert de verschillende aandelen die in het portfolio kunnen
        //            worden opgenomen.
        // ---------------------------------------------------------------------------------------------------
        public enum ShareType
        {
            AAPL        // Apple Inc.
          , AMZN        // Amazon.com, Inc.
          , TSLA        // Tesla, Inc.
          , MSFT        // Microsoft Corporation
          , NFLX        // Netflix, Inc.
        }
    }
}
