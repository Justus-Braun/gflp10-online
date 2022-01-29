RegisterNetEvent("framework:client:firstJoin", function()
    print("firstjoin")
end)

RegisterCommand('token', function()
    print(exports.framework:GetToken())
end, false)

RegisterCommand('send', function()
    TriggerServerEvent("framework:server:requestToken")
end, false)