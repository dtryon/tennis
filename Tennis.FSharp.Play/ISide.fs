namespace Tennis.FSharp.Play

    type ISide =
        abstract member TeamName : string
            with get, set
        abstract member Strength : int
            with get, set
        abstract member IsServing : bool
            with get, set
        abstract member ServingStrength : int
            with get, set
        abstract member ReturningStrength : int
            with get, set
