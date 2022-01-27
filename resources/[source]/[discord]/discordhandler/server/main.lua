RegisterNetEvent('playerConnecting', function(name, setKickReason, deferrals)
    _source = source
    exports.discordroles:isOnDiscord(tonumber(_source), function(response)
        if type(response) ~= 'table' then
            TriggerClientEvent('discordhandler:showwatermark', _source)
        end
    end)
end)