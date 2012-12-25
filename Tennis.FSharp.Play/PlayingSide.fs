namespace Tennis.FSharp.Play
  
    type PlayingSide() =
        let mutable teamName = ""
        let mutable strength = 0
        let mutable isServing = false
        let mutable servingStrength = 0
        let mutable returningStrength = 0

        interface ISide with
            member this.TeamName
                with get () = teamName
                and set (value) = teamName <- value
            member this.Strength
                with get () = strength
                and set (value) = strength <- value
            member this.IsServing
                with get () = isServing
                and set (value) = isServing <- value
            member this.ServingStrength
                with get () = servingStrength
                and set (value) = servingStrength <- value
            member this.ReturningStrength
                with get () = returningStrength
                and set (value) = returningStrength <- value

