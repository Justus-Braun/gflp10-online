AddEventHandler('framework:server:log', function(scriptName, msg)
    print(scriptName .. " => " .. msg)
end)

RegisterNetEvent('framework:client:log', function(scriptName, player, msg)
    print(scriptName .. " => " .. player .. " => " .. msg)
end)