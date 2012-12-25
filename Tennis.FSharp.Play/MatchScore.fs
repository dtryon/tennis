namespace Tennis.FSharp.Play
    
    open Tennis.FSharp.Logic

    type MatchScore() =
        [<DefaultValue>] val mutable setScores : seq<SetScore>
        [<DefaultValue>] val mutable sideOne : ISide 
        [<DefaultValue>] val mutable sideTwo : ISide

        member this.SetScores
            with get () = this.setScores
            and set (value) = this.setScores <- value
        member this.SideOne
            with get () = this.sideOne
            and set (value) = this.sideOne <- value
        member this.SideTwo
            with get () = this.sideTwo
            and set (value) = this.sideTwo <- value
