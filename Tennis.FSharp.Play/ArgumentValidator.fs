module ArgumentValidator

    let checkForNull obj message =
        if obj = null then
            raise (System.ArgumentException(message))
        else
            ()
