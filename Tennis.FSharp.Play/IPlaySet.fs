namespace Tennis.FSharp.Play
    
    open Tennis.FSharp.Logic

    type IPlaySet =
       abstract member Play : unit -> Side
       abstract member GetGameScores : unit -> seq<GameScore>
