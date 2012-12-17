namespace Tennis.FSharp.Logic

    type GameState =
       | None = 0
       | PriorToDeuce = 1
       | Deuce = 2
       | GameWonBySideOne = 3
       | GameWonBySideTwo = 4
