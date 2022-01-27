
local spawned = false

RegisterNetEvent("framework:client:firstJoin", function()
    while not spawned do
        Wait(1)
    end

    TriggerEvent('skinchanger:loadDefaultModel', true, function()
        local config = {
            ped = true,
            headBlend = true,
            faceFeatures = true,
            headOverlays = true,
            components = true,
            props = true
        }
    
        exports['fivem-appearance']:startPlayerCustomization(function(appearance)
            if (appearance) then
                print('Saved')
            else
                print('Canceled')
            end
        end, config)
    end)    
end)

AddEventHandler('playerSpawned', function()
    spawned = true
end)
