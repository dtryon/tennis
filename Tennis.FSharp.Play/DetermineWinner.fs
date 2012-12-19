namespace Tennis.FSharp.Play
    
    open Tennis.FSharp.Logic

    type DetermineWinner(sideOne: ISide, sideTwo: ISide) =
        
        let sideOne = sideOne
        let sideTwo = sideTwo

        interface IDetermineWinner with
            member this.ForPoint() =
                if sideOne.Strength > sideTwo.Strength then
                    Side.One
                else
                    Side.Two
        

