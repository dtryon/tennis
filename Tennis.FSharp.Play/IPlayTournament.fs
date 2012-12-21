namespace Tennis.FSharp.Play
    
    open Tennis.FSharp.Logic

    type IPlayTournament =
       abstract member Play : unit -> ISide
       abstract member GetMatchScores : unit -> seq<MatchScore>