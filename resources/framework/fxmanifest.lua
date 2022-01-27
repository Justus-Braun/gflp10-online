fx_version 'cerulean'
game 'gta5'

name "framework"
description "The framework of GFLP10-Online"
author "GFLP10#7754"
version "1.0.0"

shared_scripts {
	'shared/*.lua'
}

client_scripts {
	'client/*.lua'
}

server_scripts {
	'/server/bin/Debug/server.net.dll'
}
