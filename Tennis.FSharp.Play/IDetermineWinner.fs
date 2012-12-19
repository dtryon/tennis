namespace Tennis.FSharp.Play
    
    open Tennis.FSharp.Logic

    type IDetermineWinner =
        abstract member ForPoint : unit -> Side
