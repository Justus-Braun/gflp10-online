AddEventHandler("playerConnecting", function (name, setKickReason, deferrals)
    local player = source
    local fivemIdentifier
    local identifiers = GetPlayerIdentifiers(player)
    deferrals.defer()

    -- mandatory wait!
    Wait(0)

    deferrals.update(string.format("Hello %s. Your Account is being checked.", name))

    for _, v in pairs(identifiers) do
        if string.find(v, "license") then
            fivemIdentifier = string.sub(v, 1, string.len("license:"))
            break
        end
    end

    -- mandatory wait!
    Wait(0)

    if not fivemIdentifier then
        deferrals.done("You got an Error in your FiveM please Contact the Support..")
    else
        deferrals.done()
    end
end)

AddEventHandler('playerJoining', function(tempId)
    local _source = source
    TriggerEvent('framework:server:log', GetCurrentResourceName(), "Player with ID: " .. _source .. " joined.") 



    TriggerEvent('framework:server:loadPlayer', test)
end)