namespace Tennis.FSharp.Play
    
    open Tennis.FSharp.Logic

    type IPlayGame =
       abstract member Play : unit -> Side
       abstract member GetPointScores : unit -> seq<string>

