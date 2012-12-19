namespace Tennis.FSharp.Play
    
    open Tennis.FSharp.Logic

    type IPlayMatch =
       abstract member Play : unit -> ISide
       abstract member GetSetScores : unit -> seq<SetScore>
       abstract member SideOne : ISide
           with get
       abstract member SideTwo : ISide
           with get